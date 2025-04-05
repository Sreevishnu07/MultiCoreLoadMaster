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
    public Queue<Task> TaskQueue { get; set; } = new Queue<Task>();
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
        if (CurrentTask == null || CurrentTask.IsCompleted)
        {
            if (TaskQueue.Count > 0)
            {
                CurrentTask = TaskQueue.Dequeue();
                CurrentTask.StartTime = Environment.TickCount;
            }
            else
            {
                CurrentTask = null;
            }
        }
    }
}

class Task
{
    public int ExecutionTime { get; set; }
    public int StartTime { get; set; }
    public int Priority { get; set; }
    public bool IsCompleted => (Environment.TickCount - StartTime) >= ExecutionTime;
}

class LoadBalancer
{
    private Dictionary<int, Processor> processors;
    private const int HistorySize = 10;
    private Random rand = new Random();
    private Dictionary<(int, int), double> Q = new Dictionary<(int, int), double>();
    private const double alpha = 0.1;
    private const double gamma = 0.9;
    private const double epsilon = 0.2;
    private int dataCenterCount = 2;
    private const double loadThreshold = 10.0;
    private int droppedTasks = 0;

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

        var availableProcessors = processors.Values
            .Where(p => p.PredictedLoad() < loadThreshold)
            .ToList();

        if (!availableProcessors.Any())
        {
            Console.WriteLine("Load shedding: Task dropped due to high load.");
            droppedTasks++;
            return;
        }

        int bestProcessorId;
        if (rand.NextDouble() < epsilon)
        {
            bestProcessorId = availableProcessors[rand.Next(availableProcessors.Count)].Id;
        }
        else
        {
            bestProcessorId = availableProcessors
                .OrderBy(p => GetQValue(p.Id, task.Priority))
                .First().Id;
        }

        var chosenProcessor = processors[bestProcessorId];
        chosenProcessor.TaskQueue.Enqueue(task);
        UpdateLoadHistory(chosenProcessor);

        double reward = -chosenProcessor.PredictedLoad();
        double maxFutureQ = availableProcessors.Max(p => GetQValue(p.Id, task.Priority));
        var key = (bestProcessorId, task.Priority);

        if (!Q.ContainsKey(key)) Q[key] = 0;
        Q[key] += alpha * (reward + gamma * maxFutureQ - Q[key]);

        AdjustWeights();
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

        while (processors.Values.Any(p => p.CurrentTask != null || p.TaskQueue.Count > 0))
        {
            foreach (var p in processors.Values)
                p.UpdateExecution();
            Thread.Sleep(10);
        }

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    public void PrintLoads()
    {
        foreach (var p in processors.Values)
        {
            Console.WriteLine($"Processor {p.Id} (DC{p.DataCenterId}): Tasks in Queue {p.TaskQueue.Count}, Weight: {p.Weight}, Current Task: {(p.CurrentTask != null ? "Yes" : "No")}");
            Console.WriteLine($"Load History: {string.Join(", ", p.LoadHistory)}");
        }
    }

    public void PrintSummary()
    {
        Console.WriteLine($"\nSummary:\n Total Tasks Dropped: {droppedTasks}");
    }
}

class Program
{
    static void Main()
    {
        int processorsCount = 10;
        int iterations = 20;
        LoadBalancer lb = new LoadBalancer(processorsCount);

        Console.WriteLine("Reinforcement Learning-Based Load Balancer with Advanced Features:");
        Console.WriteLine($"Execution Time: {lb.MeasureExecutionTime(lb.RLAssign, iterations)} ms");

        lb.PrintLoads();
        lb.PrintSummary();
    }
}
