#include <iostream>
#include <queue>
#include <vector>
#include <string>

using namespace std;

// Structure  to represent each processor
struct Processor {
    string name;
    int load;

    Processor(string n, int l) {
        name = n;
        load = l;
    }

    // Comparator: the processor with lower load should come first and so on(priority queue into min heap)
    bool operator>(const Processor& other) const {
        return load > other.load;
    }
};

int main() {
    // Initial processor names
    vector<string> processorNames = {"P1", "P2", "P3"};

    // Min-heap priority queue using custom comparator
    priority_queue<Processor, vector<Processor>, greater<Processor>> pq;

    // Initialize processors with 0 load
    for (const string& name : processorNames) {
        pq.push(Processor(name, 0));
    }

    // Incoming tasks
    vector<string> tasks = {"T1", "T2", "T3", "T4", "T5"};

    for (const string& task : tasks) {
        // Get processor with least load
        Processor leastBusy = pq.top();
        pq.pop();

        // Assign task
        cout << task << " assigned to " << leastBusy.name << endl;

        // Increase processor load and push back into heap
        leastBusy.load++;
        pq.push(leastBusy);
    }

    return 0;
}
