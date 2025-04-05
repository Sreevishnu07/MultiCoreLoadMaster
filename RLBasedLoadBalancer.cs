using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading;

class Processor
{
    public int Id { get; set; }
    public int DataCenterId { get; set; }
    public int Load => TaskQueue.Count + (CurrentTask != null ? 1 : 0);
    public int Weight { get; set; } = 1;
    public Queue<double> LoadHistory { get; set; } = new Queue<double>();
    public PriorityQueue<Task, int> TaskQueue { get; set; } = new PriorityQueue<Task, int>();
    public Task CurrentTask { get; set; } = null;

    public double PredictedLoad()
    {
        double smoothingFactor = 0.7;
        double predicted = Load;
        foreach (var l in LoadHistory.Reverse())
            predicted = smoothingFactor * l + (1 - smoothingFactor) * predicted;
        return predicted / Weight;
    }

    public void UpdateExecution()
    {
        if (CurrentTask != null && CurrentTask.IsCompleted)
        {
            CurrentTask = null;
        }

        if (CurrentTask == null && TaskQueue.Count > 0)
        {
            TaskQueue.TryDequeue(out Task task, out _);
            CurrentTask = task;
            CurrentTask.StartTime = Environment.TickCount;
        }
    }
}

class Task
{
    public int ExecutionTime { get; set; }
    public int StartTime { get; set; } = -1;
    public int Priority { get; set; }
    public int ArrivalTime { get; set; }
    public bool IsCompleted => StartTime >= 0 && (Environment.TickCount - StartTime) >= ExecutionTime;
}

class LoadBalancer
{
    private Dictionary<int, Processor> processors;
    private Queue<Task> dynamicTaskQueue = new Queue<Task>();
    private const int HistorySize = 10;
    private Random rand = new Random();
    private Dictionary<(int, int), double> Q = new Dictionary<(int, int), double>();
    private const double alpha = 0.1;
    private const double gamma = 0.9;
    private const double epsilon = 0.2;
    private int dataCenterCount = 2;
    private const double loadThreshold = 10.0;
    private int droppedTasks = 0;
    private int totalTasksAssigned = 0;
    private int currentTick = 0;

    public LoadBalancer(int processorCount)
    {
        processors = new Dictionary<int, Processor>();
        for (int i = 0; i < processorCount; i++)
        {
            processors[i] = new Processor
            {
                Id = i,
                DataCenterId = i % dataCenterCount,
                Weight = rand.Next(1, 5)
            };
        }
    }

    public void RLAssign(Task task)
    {
        task.ExecutionTime = rand.Next(500, 2000);
        task.ArrivalTime = Environment.TickCount + rand.Next(0, 100);
        dynamicTaskQueue.Enqueue(task);
    }

    private void ProcessDynamicTasks()
    {
        while (dynamicTaskQueue.Count > 0 || processors.Values.Any(p => p.CurrentTask != null || p.TaskQueue.Count > 0))
        {
            currentTick = Environment.TickCount;

            while (dynamicTaskQueue.Count > 0 && dynamicTaskQueue.Peek().ArrivalTime <= currentTick)
            {
                var task = dynamicTaskQueue.Dequeue();
                var availableProcessors = processors.Values
                    .Where(p => p.PredictedLoad() < loadThreshold)
                    .ToList();

                if (!availableProcessors.Any())
                {
                    Console.WriteLine("Load shedding: Task dropped due to high load.");
                    droppedTasks++;
                    continue;
                }

                var bestProcessor = availableProcessors
                    .OrderBy(p => p.Load)
                    .ThenBy(p => p.PredictedLoad())
                    .First();

                bestProcessor.TaskQueue.Enqueue(task, -task.Priority);
                totalTasksAssigned++;
                UpdateLoadHistory(bestProcessor);

                double reward = -bestProcessor.PredictedLoad();
                double maxFutureQ = availableProcessors.Max(p => GetQValue(p.Id, task.Priority));
                var key = (bestProcessor.Id, task.Priority);

                if (!Q.ContainsKey(key)) Q[key] = 0;
                Q[key] += alpha * (reward + gamma * maxFutureQ - Q[key]);

                AdjustWeights();
            }

            foreach (var p in processors.Values)
                p.UpdateExecution();

            Thread.Sleep(10);
        }
    }

    private void AdjustWeights()
    {
        foreach (var p in processors.Values)
        {
            double avgLoad = p.LoadHistory.Any() ? p.LoadHistory.Average() : 0;
            if (avgLoad > loadThreshold)
                p.Weight = Math.Max(1, p.Weight - 1);
            else if (avgLoad < loadThreshold / 2)
                p.Weight += 1;
        }
    }

    private double GetQValue(int processorId, int priority)
    {
        var key = (processorId, priority);
        return Q.ContainsKey(key) ? Q[key] : 0;
    }

    private void UpdateLoadHistory(Processor p)
    {
        if (p.LoadHistory.Count >= HistorySize)
            p.LoadHistory.Dequeue();
        p.LoadHistory.Enqueue(p.Load);
    }

    public long MeasureExecutionTime(Action<Task> assignMethod, int iterations)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        for (int i = 0; i < iterations; i++)
            assignMethod(new Task { Priority = rand.Next(1, 4) });

        ProcessDynamicTasks();

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    public void PrintLoads()
    {
        foreach (var p in processors.Values)
        {
            Console.WriteLine($"Processor {p.Id} (DC{p.DataCenterId}): Tasks in Queue {p.TaskQueue.Count}, Weight: {p.Weight}, Current Task: {(p.CurrentTask != null ? "Yes" : "No")}");
            Console.WriteLine($"  ➔ Load History: {string.Join(", ", p.LoadHistory)}");
        }
    }

    public void PrintSummary()
    {
        Console.WriteLine($"\nSummary:");
        Console.WriteLine($"➔ Total Tasks Dropped: {droppedTasks}");
        Console.WriteLine($"➔ Total Tasks Assigned: {totalTasksAssigned}");
        Console.WriteLine("➔ Tasks Assigned per Processor:");
        foreach (var p in processors.Values)
        {
            int totalProcessed = p.LoadHistory.Count + (p.CurrentTask != null ? 1 : 0);
            Console.WriteLine($"  - Processor {p.Id}: {totalProcessed} tasks processed");
        }

        Console.WriteLine("\n➔ Q-Learning Table (ProcessorID, Priority) => Q-Value:");
        foreach (var entry in Q.OrderBy(e => e.Key.Item1).ThenBy(e => e.Key.Item2))
        {
            Console.WriteLine($"  - ({entry.Key.Item1}, Priority {entry.Key.Item2}) => {entry.Value:F3}");
        }
    }
}

class Program
{
    static void Main()
    {
        int processorsCount = 5;
        int iterations = 75;
        LoadBalancer lb = new LoadBalancer(processorsCount);

        Console.WriteLine("Reinforcement Learning-Based Load Balancer:");
        Console.WriteLine($"Execution Time: {lb.MeasureExecutionTime(lb.RLAssign, iterations)} ms");

        lb.PrintLoads();
        lb.PrintSummary();
    }
}
