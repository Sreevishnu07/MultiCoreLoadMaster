**Overview**

Implementation an AI-based load balancer that distributes tasks among multiple processors based on their predicted load and task priority.
The load balancer uses a simple moving average algorithm to predict the load of each processor and assigns tasks to the processor with the lowest predicted load and priority.
The code also includes features such as task execution monitoring, load history tracking, and execution time measurement.

**Features**

1. **Predicted Load Calculation**: The load balancer calculates the predicted load of each processor using a simple moving average algorithm, which takes into account the current load and the historical average load.
2. **Task Assignment**: The load balancer assigns tasks to processors based on their predicted load and task priority.
3. **Task Execution Monitoring**: The load balancer monitors the execution time of each task and updates the processor's load history accordingly.
4. **Load History Tracking**: The load balancer tracks the load history of each processor to calculate the predicted load.
5. **Execution Time Measurement**: The load balancer measures the execution time of the load balancer and prints the result to the console.
6. **Processor Selection**: The load balancer selects the processor with the lowest predicted load and priority to assign the task.
7. **Task Queue Management**: The load balancer manages the task queue by dequeuing tasks from the processor's task queue.

**Merits**

1. **Efficient Task Distribution**: The load balancer efficiently distributes tasks among processors based on their predicted load and task priority.
2. **Real-time Monitoring**: The load balancer monitors the execution time of each task in real-time, providing valuable insights into the system's performance.
3. **Load History Tracking**: The load balancer tracks the load history of each processor, allowing for more accurate predicted load calculations.
4. **Scalability**: The load balancer can handle a large number of processors and tasks, making it suitable for large-scale systems.
5. **Flexibility**: The load balancer can be easily modified to accommodate different task assignment strategies and processor selection algorithms.

**Demerits**

1. **Simplistic Predicted Load Calculation**: The load balancer uses a simple moving average algorithm to calculate the predicted load, which may not accurately reflect the actual load.
2. **Limited Task Priority Handling**: The load balancer assigns tasks to the processor with the lowest predicted load and priority, which may not always result in optimal task assignment.

**Potential Improvements**

1. **Improve Predicted Load Calculation**: Use more advanced algorithms, such as machine learning-based models, to improve the accuracy of predicted load calculations.
2. **Implement Dynamic Task Arrival Handling**: Introduce a mechanism to handle dynamic task arrival, such as a queue-based system, to ensure that tasks are assigned to available processors.
3. **Support Task Prioritization**: Implement a task prioritization mechanism, such as a priority queue, to ensure that tasks with higher priority are assigned to processors with higher priority.
4. **Optimize Processor Selection Algorithm**: Improve the processor selection algorithm to ensure that tasks are assigned to the most suitable processor based on the predicted load and task priority.
