#include <iostream>
#include <vector>

using namespace std;

class RoundRobinScheduler {
private:
    vector<string> processors;
    int currentIndex;

public:
    RoundRobinScheduler(vector<string> procList) {
        processors = procList;
        currentIndex = 0;
    }

    void assignTask(string task) {
        cout << "Task " << task << " assigned to Processor " << processors[currentIndex] << endl;
        currentIndex = (currentIndex + 1) % processors.size();
    }
};

int main() {
    vector<string> processors = {"P1", "P2", "P3"};
    RoundRobinScheduler scheduler(processors);
    vector<string> tasks = {"T1", "T2", "T3", "T4", "T5", "T6"};

    for (string task : tasks) {
        scheduler.assignTask(task);
    }

    return 0;
}
