//Implementing modular C# versions of the Analysed Algorithms.
//Each algorithm will be implemented in a separate method for easy testing and comparison.
//Initializes a LoadBalancer class with multiple algorithms: Round Robin, Weighted Round Robin, Least Connections, Least Load, and Threshold-Based.

using System;
using System.Collections.Generic;
using System.Linq;

class Processor
{
    public int Id { get; set; }
    public int Load { get; set; }  // Represents CPU usage or task count
}

class LoadBalancer
{
    private List<Processor> processors;
    private int roundRobinIndex = 0;

    public LoadBalancer(int processorCount)
    {
        processors = new List<Processor>();
        for (int i = 0; i < processorCount; i++)
            processors.Add(new Processor { Id = i, Load = 0 });
    }

    // Round Robin Algorithm
    public void RoundRobinAssign()
    {
        processors[roundRobinIndex].Load++;
        roundRobinIndex = (roundRobinIndex + 1) % processors.Count;
    }

    // Weighted Round Robin Algorithm
    public void WeightedRoundRobinAssign(List<int> weights)
    {
        int maxWeightIndex = weights.IndexOf(weights.Max());
        processors[maxWeightIndex].Load++;
    }

    // Least Connections Algorithm
    public void LeastConnectionsAssign()
    {
        var leastLoaded = processors.OrderBy(p => p.Load).First();
        leastLoaded.Load++;
    }

    // Least Load Algorithm
    public void LeastLoadAssign()
    {
        var leastLoad = processors.OrderBy(p => p.Load).First();
        leastLoad.Load++;
    }

    // Threshold-Based Algorithm
    public void ThresholdAssign(int threshold)
    {
        var overloaded = processors.Where(p => p.Load > threshold).ToList();
        if (overloaded.Any())
        {
            var leastLoaded = processors.OrderBy(p => p.Load).First();
            leastLoaded.Load++;
        }
        else
        {
            processors[0].Load++;
        }
    }

    // AI-Based Load Balancing Algorithm
    public void AIBasedAssign()
    {
        var predictedLeastLoaded = processors.OrderBy(p => PredictLoad(p)).First();
        predictedLeastLoaded.Load++;
    }

    private int PredictLoad(Processor p)
    {
        return p.Load + new Random().Next(0, 3); // Simulated AI prediction
    }

    // Display processor loads
    public void PrintLoads()
    {
        foreach (var p in processors)
            Console.WriteLine($"Processor {p.Id}: Load {p.Load}");
    }
}

class Program
{
    static void Main()
    {
        LoadBalancer lb = new LoadBalancer(3);
        for (int i = 0; i < 10; i++) lb.RoundRobinAssign();
        lb.PrintLoads();

        Console.WriteLine("\nAI-Based Load Balancing:");
        for (int i = 0; i < 10; i++) lb.AIBasedAssign();
        lb.PrintLoads();
    }
}



