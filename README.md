
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

