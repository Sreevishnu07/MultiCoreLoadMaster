The points I felt were the pros of weighted RR is:
1)Fair distribution based on Power-In real-world scenario not all CPUs are powerful,so the powerful one(heavier weight in this case) naturally would get more tasks.
2)Simple and predictable
3)Load balancing for unequal systems,which is the crux of our project.
4)And prevents starvation-unlike greedy schedulers(pririty based for instance)the lower-weight ones eventually gets tasks,it isnt ignored forever.
5)Low overheads and high efficiency in real-time scenarios.
