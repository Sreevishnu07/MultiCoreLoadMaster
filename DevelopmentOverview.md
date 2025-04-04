
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

D. Least Load (LL)

    Description:
    > Assigns tasks to the processor with the lowest CPU utilization at that moment.
    > Uses real-time CPU load monitoring to distribute workloads dynamically.
    
    Benefits:
    > Ensures efficient CPU usage and prevents overloading.
    > Highly adaptive to changing system states.
    
    Drawbacks:
    > Requires continuous monitoring of processor load, adding overhead.
    > May cause task migration overhead if loads fluctuate rapidly.
    
    Suitability:
    > One of the best adaptive algorithms but requires careful tuning to avoid excessive migrations.

E. Randomized Load Balancing

    Description:
    > Assigns tasks randomly to processors.
    > Works best when tasks are short-lived and evenly distributed.
    
    Benefits:
    > Very low overhead (no tracking or monitoring required).
    > Avoids bottlenecks caused by static rules.
    
    Drawbacks:
    > Can result in uneven workload distribution, leading to some processors being overloaded.
    > Not adaptive to system state changes.
    
    Suitability: 
    > Useful for systems with small, short-lived tasks, but not ideal for optimizing resource utilization.

F. Threshold-Based Load Balancing

    Description:
    > Sets predefined threshold values (e.g., CPU usage, queue length).
    > If a processor’s load exceeds the threshold, new tasks are moved to a less-loaded processor.
    
    Benefits:
    > More adaptive than static algorithms like RR & WRR.
    > Helps prevent overloads by redistributing tasks dynamically.
    
    Drawbacks:
    > Requires manual tuning of threshold values.
    > May introduce migration delays when tasks need to be moved.
    
    Suitability:
    > Good for dynamic adaptation, but may need fine-tuning to avoid frequent task migrations.

G. AI-Based Load Balancing

    Description:
    > Uses machine learning or predictive algorithms to forecast workload patterns and dynamically allocate tasks.
    > Can analyze historical data and predict which processor will be least loaded in the near future.
    
    Benefits:
    > Highly adaptive to varying workloads.
    > Can self-optimize over time using real-world data.
    
    Drawbacks:
    > Requires training data and can be complex to implement.
    > Overhead may be higher than traditional methods.
    
    Suitability:
    > Best for large-scale cloud or dynamic systems, but may be overkill for smaller systems.

Comparison & Best Fit for Our Project

    Algorithm        Adaptability        Performance Optimization        Overhead        Best Use Case
    
    Round Robin          No                       Limited	               Low	    Simple workloads
    Weighted RR	     No	                      Moderate	               Low	    Heterogeneous processors
    Least Connections    Yes	               Good	              Moderate	    Dynamic workloads
    Least Load	     Yes	               High	               High	    Real-time balancing
    Randomized	     No	                       Poor	              Very Low	    Short tasks, even load
    Threshold-Based	     Yes	               Good	              Moderate	    Preventing overloads
    AI-Based	     Yes	              Very High	               High	    Large-scale adaptive systems

Conclusion of Analysis

    For our dynamic load balancing project, we need an algorithm that:
    1. Adapts to workload changes dynamically
    2. Optimizes CPU usage efficiently
    3. Minimizes overhead while improving response times
    
    Best Choices:
    
    A. Least Load – Best real-time adaptation with efficient CPU usage.
    
    B. Threshold-Based – Simple yet effective dynamic balancing.
    
    C. AI-Based – Ultimate efficiency but complex to implement.
