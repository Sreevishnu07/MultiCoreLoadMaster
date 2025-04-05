using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading;

class Processor
{
    public int Id { get; set; }
    public int Load => TaskQueue.Count + (CurrentTask != null ? 1 : 0);
    public int Weight { get; set; } = 1;
    public Queue<int> LoadHistory { get; set; } = new Queue<int>();
    public Queue<Task> TaskQueue { get; set; } = new Queue<Task>();
    public Task CurrentTask { get; set; } = null;

    public double PredictedLoad()
    {
        double historicalAverage = LoadHistory.Count > 0 ? LoadHistory.Average() : Load;
        return 0.5 * Load + 0.5 * historicalAverage;
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
    public int Priority { get; set; } // 1 = High, 2 = Medium, 3 = Low
    public bool IsCompleted => (Environment.TickCount - StartTime) >= ExecutionTime;
}

class LoadBalancer
{
    private List<Processor> processors;
    private const int HistorySize = 5;
    private Random rand = new Random();

    public LoadBalancer(int processorCount)
    {
        processors = new List<Processor>();
        for (int i = 0; i < processorCount; i++)
            processors.Add(new Processor { Id = i, Weight = rand.Next(1, 5) });
    }

    public void AIBasedAssign(Task task)
    {
        var sortedProcessors = processors.OrderBy(p => p.PredictedLoad()).ThenBy(p => task.Priority).ToList();
        var bestProcessor = sortedProcessors.First();
        task.ExecutionTime = rand.Next(500, 2000); // Simulate realistic exec time
        bestProcessor.TaskQueue.Enqueue(task);
        UpdateLoadHistory(bestProcessor);
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

        while (processors.Any(p => p.CurrentTask != null || p.TaskQueue.Count > 0))
        {
            processors.ForEach(p => p.UpdateExecution());
            Thread.Sleep(10);
        }

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    public void PrintLoads()
    {
        foreach (var p in processors)
            Console.WriteLine($"Processor {p.Id}: Tasks in Queue {p.TaskQueue.Count}, Current Task: {(p.CurrentTask != null ? "Yes" : "No")}");
    }
}

class Program
{
    static void Main()
    {
        int processorsCount = 5;
        int iterations = 1000;
        LoadBalancer lb = new LoadBalancer(processorsCount);

        Console.WriteLine("Enhanced AI-Based Load Balancer:");
        Console.WriteLine($"Execution Time: {lb.MeasureExecutionTime(lb.AIBasedAssign, iterations)} ms");

        lb.PrintLoads();
    }
}
