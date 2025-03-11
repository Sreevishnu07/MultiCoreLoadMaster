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


