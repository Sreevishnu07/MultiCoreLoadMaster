
# MultiCoreLoadMaster
A project on multiple load balancing in a multiprocessor system, focusing on dynamic load balancing strategies to optimize task allocation and improve system efficiency.
# Features
1. Efficient dynamic load distribution across multiple processors.
2. Real-time load adjustments to minimize processing delays.
3. Adaptive and scalable load-balancing techniques.
4. Fault tolerance and failure recovery mechanisms.   
# Round Robin
This is the first and the basic algorithm in load balancing which works on **cyclic assignment Principle** ensuring that each processor get roughly equal number og task over the time.
## How it works
1. **Task Arrival** - new task come in the system and it need to be assigned to the processor.
2. **Processor Selection** - task is assigned to the processors sequentially to available processor in cyclic manner.
3. **Task Execution** - each processor execute its assigned task independently.
4. **Loop back** - when the task is assigned to the last processor it again comeback to the first processor.


# Weighted Round Robin implementation for load balancing on MP Systems:
The points I felt were the pros of weighted RR is:
1. Fair distribution based on Power-In real-world scenario not all CPUs are powerful,so the powerful one(heavier weight in this case) naturally would get more tasks.
2. Simple and predictable
3. Load balancing for unequal systems,which is the crux of our project.
4. And prevents starvation-unlike greedy schedulers(pririty based for instance)the lower-weight ones eventually gets tasks,it isnt ignored forever.
5. Low overheads and high efficiency in real-time scenarios.

# The Pros of LWR(Least-Work-Remaining)that i felt were top-notch:
Dynamic Priority Assignment:
LWR dynamically assigns tasks to processors based on their current workload and computational power, 
always choosing the processor with the least effective load (i.e., workload adjusted by power).

Heterogeneous Processor Support:
LWR can handle heterogeneous processors by factoring in processor power, 
which ensures that faster processors take on more work compared to slower ones.

Min-Heap Based Scheduling:
It uses a priority queue (min-heap) to always select the processor with the minimum effective load,
making task allocation more efficient and balanced.

# Enhancements made in the algo:
Threshold-based Switching (LWR + RR Hybrid):
Introduced a threshold mechanism: if the two lightest-loaded processors have a similar effective load (difference ≤ threshold), 
the algorithm switches to Round Robin fallback for fairness, preventing any one processor from being slightly overused.

Round Robin Fallback for Load Uniformity:
By blending RR as a fallback when processors are similarly loaded, this enhancement avoids unnecessary 
"greediness" of LWR in marginal cases, distributing tasks more uniformly under mild load variations.

Adaptive Balancing Strategy:
This enhancement makes the scheduler adaptive: it behaves like pure LWR when there’s 
significant imbalance but gracefully shifts to RR when system load is already well-distributed, making it more robust in real-world scenarios.


# Least Connection Load Balancing in Multiprocessor System

In this algo, when a task arrives, it is assigned to the processor with the **least number of currently running tasks**, and so on with other processors.

## ⚙️ Approaches Implemented

### 1. Naive Approach (Without Heap)

- I used a simple vector to tracke the number of task per processor
- For each task, we **loop through all processors** to find the one with the minimum load.

#### ⏱ Time Complexity:
- **Per task assignment**: `O(P)`  
- **Total for N tasks**: `O(N × P)` → Roughly like `O(N^2)` 

---

### 2. Optimized Approach (Using Min-Heap / Priority Queue)

- I used **priority queue** configured as **min-heap** to correctly get the processor with the minimum load.
- After each assignment, the processor is pushed back into the heap with updated load.

#### ⏱ Time Complexity:
- **Per task assignment**: `O(log P)`  
- **Total for N tasks**: `O(N × log P)` ✅

## 🚀 Future Enhancements

- Add support for task completion.
- Simulate processor speeds.
- Visualize load over time.


