
# Development Walkthrough
1. Researching different load balancing algorithms (Round Robin, Least Load, etc).
2. Implementing each algorithm in C-Sharp to compare performance.
3. Selecting the most efficient approach and optimizing it.
4. Finalizing and merging the best implementation into the main branch.

# Load Balancing Algorithms to Explore

    A. Round Robin (RR): Assigns tasks cyclically to processors. Simple but doesn’t consider current loads.
    
    B. Weighted Round Robin (WRR): Assigns more tasks to powerful processors based on weight factors.
    
    C. Least Connections: Assigns tasks to the processor with the fewest active connections.
    
    D. Least Load: Assigns tasks to the processor with the lowest CPU utilization.
    
    E. Randomized Load Balancing: Assigns tasks randomly, useful for evenly distributed workloads.
    
    F. Threshold-Based Balancing: Uses predefined thresholds to migrate tasks from overloaded processors.
    
    G. AI-Based Load Balancing: Uses machine learning to predict and distribute workloads dynamically.

# Analysis of Load Balancing Algorithms
Analysis of different algorithms based on their ability to dynamically distribute workloads, optimize performance, and adapt to system states.

A. Round Robin (RR)
    
    Description:
    > Tasks are assigned cyclically to processors in a circular manner.
    > Each processor gets equal tasks, irrespective of its current load.
    
    Benefits:
    > Simple and easy to implement.
    > Ensures fair distribution of tasks.
    
    Drawbacks:
    > Doesn’t consider processor load, leading to overloaded CPUs if some tasks take longer.
    > Not adaptive to system changes.
    
    Suitability: 
    > Works well when all tasks have equal execution times, but inefficient for variable workloads.

B. Weighted Round Robin (WRR)

    Description:
    > Extends Round Robin by assigning a weight to each processor based on its computing power.
    > Processors with higher capacity receive more tasks.
    
    Benefits:
    > Improves performance over RR by considering processor capabilities.
    > Still relatively simple to implement.
    
    Drawbacks:
    > Static weights: Doesn’t dynamically adjust to system state changes.
    > If a processor gets overloaded, it can’t adjust tasks dynamically.
    
    Suitability:
    > Better than RR for heterogeneous processors but still lacks real-time adaptability.

C. Least Connections (LC)

    Description:
    > Assigns tasks to the processor with the least active connections (i.e., least busy processor).
    > Adjusts in real-time based on the number of active tasks per processor.
    
    Benefits:
    > Dynamic: Adjusts task assignment based on real-time processor load.
    > Prevents overloading any single processor.
    
    Drawbacks:
    > If some tasks are longer-running, this method may not always balance CPU usage efficiently.
    > Tracking active connections introduces additional overhead.
    
    Suitability:
    > Good for real-time adaptive balancing, but may need additional metrics for better efficiency.
