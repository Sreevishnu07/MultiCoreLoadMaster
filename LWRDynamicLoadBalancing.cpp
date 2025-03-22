#include <iostream>
#include <vector>
#include <queue>
#include <cmath>
#include <limits>

struct Processor {
    int id;
    int power; // Computational power factor (for heterogeneity)
    int workload; // Current workload (e.g., sum of task weights)

    // Effective load = workload / power
    double effectiveLoad() const {
        return static_cast<double>(workload) / power;
    }

    bool operator<(const Processor& other) const {
        return effectiveLoad() > other.effectiveLoad();
    }
};

struct Task {
    int id;
    int weight; // Task weight (e.g., time or size)
};

int main() {
    int numProcessors = 4;
    int threshold = 2; // enhancement: max allowed difference between loads before triggering RR fallback

    // Example heterogeneous processors
    std::vector<Processor> processors = {
        {0, 2, 0}, // Processor 0: power = 2
        {1, 1, 0}, // Processor 1: power = 1
        {2, 3, 0}, // Processor 2: power = 3
        {3, 2, 0}  // Processor 3: power = 2
    };

    // Example task queue
    std::vector<Task> tasks = {
        {0, 5}, {1, 3}, {2, 7}, {3, 4}, {4, 2}, {5, 6}, {6, 3}
    };

    int rrPointer = 0; // For RR fallback

    // Priority queue (min-heap) for processors based on effective load
    auto cmp = [](Processor a, Processor b) { return a < b; };
    std::priority_queue<Processor, std::vector<Processor>, decltype(cmp)> pq(cmp);

    for (const auto& proc : processors) {
        pq.push(proc);
    }

    for (const auto& task : tasks) {
        // Peek top 2 processors to check threshold difference
        Processor first = pq.top(); pq.pop();
        Processor second = pq.top(); pq.pop();

        double diff = std::abs(first.effectiveLoad() - second.effectiveLoad());

        Processor selected;

        if (diff <= threshold) {
            // 🔵 ENHANCEMENT: Fallback to RR when loads are similar
            selected = processors[rrPointer];
            rrPointer = (rrPointer + 1) % numProcessors;
            std::cout << "Task " << task.id << " assigned to Processor " << selected.id << " using RR fallback.\n";
        } else {
            // 🔵 ENHANCEMENT: Threshold-based LWR selection
            selected = first;
            std::cout << "Task " << task.id << " assigned to Processor " << selected.id << " using LWR.\n";
        }

        // Update workload
        processors[selected.id].workload += task.weight;

        // Rebuild heap with updated processor states
        while (!pq.empty()) pq.pop();
        for (const auto& proc : processors) pq.push(proc);
    }

    // Print final workloads
    std::cout << "\nFinal processor workloads:\n";
    for (const auto& proc : processors) {
        std::cout << "Processor " << proc.id << " workload: " << proc.workload << " (power = " << proc.power << ")\n";
    }

    return 0;
}
