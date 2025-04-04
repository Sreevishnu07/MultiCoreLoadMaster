//Implementing modular C# versions of the Analysed Algorithms.
//Each algorithm will be implemented in a separate method for easy testing and comparison.
//Initializes a LoadBalancer class with multiple algorithms: Round Robin, Weighted Round Robin, Least Connections, Least Load, and Threshold-Based.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading;

class Processor
{
    public int Id { get; set; }
    public int Load { get; set; }  // Represents the number of assigned tasks
    public int Weight { get; set; } = 1;  // Used for Weighted Round Robin
    public Queue<int> LoadHistory { get; set; } = new Queue<int>();
    public Queue<Task> TaskQueue { get; set; } = new Queue<Task>();
    public Task CurrentTask { get; set; } = null;

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
    public bool IsCompleted => (Environment.TickCount - StartTime) >= ExecutionTime;
}

class LoadBalancer
{
    private List<Processor> processors;
    private Random rand = new Random();
    private int roundRobinIndex = 0;
    private int weightedRRIndex = 0;
    private int weightedCounter = 0;
    private const int HistorySize = 5;
    private const int Threshold = 5;  // For Threshold-Based Balancing

    public LoadBalancer(int processorCount)
    {
        processors = new List<Processor>();
        for (int i = 0; i < processorCount; i++)
            processors.Add(new Processor { Id = i, Weight = rand.Next(1, 5) });
    }

    private void AssignTask(Processor p)
    {
        p.TaskQueue.Enqueue(new Task { ExecutionTime = rand.Next(500, 5000) });
        p.Load = p.TaskQueue.Count + (p.CurrentTask != null ? 1 : 0);
        UpdateLoadHistory(p);
    }

    public void RoundRobinAssign()
    {
        AssignTask(processors[roundRobinIndex]);
        roundRobinIndex = (roundRobinIndex + 1) % processors.Count;
    }

    public void LeastLoadAssign()
    {
        var leastLoaded = processors.OrderBy(p => p.Load).First();
        AssignTask(leastLoaded);
    }

    public void WeightedRoundRobinAssign()
    {
        AssignTask(processors[weightedRRIndex]);
        weightedCounter++;

        if (weightedCounter >= processors[weightedRRIndex].Weight)
        {
            weightedCounter = 0;
            weightedRRIndex = (weightedRRIndex + 1) % processors.Count;
        }
    }

    public void ThresholdAssign()
    {
        var overloaded = processors.Where(p => p.Load > Threshold).ToList();
        if (overloaded.Any())
        {
            var leastLoaded = processors.OrderBy(p => p.Load).First();
            AssignTask(leastLoaded);
        }
        else
        {
            AssignTask(processors[0]);
        }
    }

    public void RandomizedAssign()
    {
        var randomProcessor = processors[rand.Next(processors.Count)];
        AssignTask(randomProcessor);
    }

    public void AIBasedAssign()
    {
        var bestProcessor = processors.OrderBy(p => PredictLoad(p)).First();
        AssignTask(bestProcessor);
    }

    private double PredictLoad(Processor p)
    {
        if (!p.LoadHistory.Any()) return p.Load;
        return p.LoadHistory.Average();
    }

    private void UpdateLoadHistory(Processor p)
    {
        if (p.LoadHistory.Count >= HistorySize)
            p.LoadHistory.Dequeue();
        p.LoadHistory.Enqueue(p.Load);
    }

    public void ReinforcementLearningAssign()
    {
        var bestProcessor = processors.OrderBy(p => PredictLoad(p)).First();
        if (rand.NextDouble() < 0.2)  // 20% chance to explore a different processor
        {
            bestProcessor = processors[rand.Next(processors.Count)];
        }
        AssignTask(bestProcessor);
    }

    public long MeasureCompletionTime(Action assignMethod, int iterations)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        for (int i = 0; i < iterations; i++)
        {
            assignMethod();
            Thread.Sleep(50);
        }

        while (processors.Any(p => p.CurrentTask != null || p.TaskQueue.Count > 0))
        {
            processors.ForEach(p => p.UpdateExecution());
            Thread.Sleep(50);
        }

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    public void PrintLoads(string method)
    {
        Console.WriteLine($"\nProcessor Load Distribution ({method}):");
        foreach (var p in processors)
            Console.WriteLine($"Processor {p.Id}: Load {p.Load}");
    }
}

class Program
{
    static void Main()
    {
        int processorsCount = 5;
        int iterations = 1000;
        LoadBalancer lb = new LoadBalancer(processorsCount);

        Console.WriteLine("Testing Round Robin:");
        Console.WriteLine($"Completion Time: {lb.MeasureCompletionTime(lb.RoundRobinAssign, iterations)} ms");
        lb.PrintLoads("Round Robin");

        Console.WriteLine("\nTesting Least Load:");
        Console.WriteLine($"Completion Time: {lb.MeasureCompletionTime(lb.LeastLoadAssign, iterations)} ms");
        lb.PrintLoads("Least Load");

        Console.WriteLine("\nTesting Weighted Round Robin:");
        Console.WriteLine($"Completion Time: {lb.MeasureCompletionTime(lb.WeightedRoundRobinAssign, iterations)} ms");
        lb.PrintLoads("Weighted Round Robin");

        Console.WriteLine("\nTesting Threshold-Based:");
        Console.WriteLine($"Completion Time: {lb.MeasureCompletionTime(lb.ThresholdAssign, iterations)} ms");
        lb.PrintLoads("Threshold-Based");

        Console.WriteLine("\nTesting Randomized:");
        Console.WriteLine($"Completion Time: {lb.MeasureCompletionTime(lb.RandomizedAssign, iterations)} ms");
        lb.PrintLoads("Randomized");

        Console.WriteLine("\nTesting AI-Based Load Balancer:");
        Console.WriteLine($"Completion Time: {lb.MeasureCompletionTime(lb.AIBasedAssign, iterations)} ms");
        lb.PrintLoads("AI-Based");

        Console.WriteLine("\nTesting Reinforcement Learning-Based:");
        Console.WriteLine($"Completion Time: {lb.MeasureCompletionTime(lb.ReinforcementLearningAssign, iterations)} ms");
        lb.PrintLoads("Reinforcement Learning-Based");
    }
}



