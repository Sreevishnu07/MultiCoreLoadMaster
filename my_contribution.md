# 🔧 Module: Dynamic Load Balancing (My Contribution)

## 🧠 What My Part Covers

As part of our group project on **Load Balancing in Multiprocessor Systems**, my focus was on the **Dynamic Load Balancing** section.

Specifically, I worked on:
- ✅ Implementing **Round Robin** load balancing
- ✅ Implementing **Least Connection** load balancing (with and without optimization)
- ✅ Comparing time complexities
- ✅ Writing modular and clean C++ code

---

## 📌 1. Round Robin Algorithm

I started with the **Round Robin approach** to understand how basic task distribution works.

### 🔹 What it does:
- Assigns tasks to processors one by one in a circular order.
- Doesn’t check the current load on each processor.

### 🧠 Key Point:
> It's a simple algorithm but doesn’t consider whether a processor is already overloaded or not.

### ⏱ Time Complexity:
- `O(1)` per task
- Very fast but not load-aware

---

## 📌 2. Least Connection Algorithm

Then I implemented **Least Connection** which is a more dynamic and smart load balancing technique.

---

### 🔹 Approach 1: Without Priority Queue
- I tracked each processor's task count using arrays.
- Before assigning a task, I checked all processors to find the one with the **least current load**.

#### 🧠 Limitation:
> Works correctly but slower if the number of processors increases.

#### ⏱ Time Complexity:
- `O(P)` per task  
- For N tasks: `O(N × P)`

---

### 🔹 Approach 2: With Min-Heap (Priority Queue)
To optimize the above approach, I used a **priority queue with a custom comparator** to implement a **min-heap**.

#### 💡 What Changed:
- No need to manually search for the least-loaded processor.
- Heap automatically gives the one with the smallest load.

#### ⏱ Time Complexity:
- `O(log P)` per task  
- For N tasks: `O(N × log P)` ✅

---

## 🔍 Pros & Cons of the Algorithms I Implemented

| Algorithm                  | Pros                                 | Cons                                   |
|---------------------------|--------------------------------------|----------------------------------------|
| Round Robin               | Easy, Fast                           | Doesn’t consider processor load        |
| Least Connection (Array)  | Smarter load distribution            | Slower for more processors             |
| Least Connection (Heap)   | Fast and Smart                       | Slightly more complex code             |

---

## 🗂 Files I Worked On

| File Name                    | Description                                        |
|-----------------------------|----------------------------------------------------|
| `round_robin.cpp`           | Round Robin implementation                        |
| `least_connection_naive.cpp`| Least connection using arrays                     |
| `least_connection_heap.cpp` | Optimized least connection using min-heap         |
| `my_contribution.md` | My explanation and contribution         |

---

## 📌 Summary of My Contribution

✔️ Implemented core dynamic load balancing algorithms  
✔️ Focused on making code modular, readable, and optimized  
✔️ Compared time complexities for better understanding  
✔️ Wrote documentation and logic explanation

---

## 🧠 What I Learned

This module helped me clearly understand:
- How static and dynamic load balancing differ
- How min-heaps can reduce time complexity
- How to think in terms of real-world task assignment

I also learned how important it is to **choose the right algorithm** for better performance as the system scales.

