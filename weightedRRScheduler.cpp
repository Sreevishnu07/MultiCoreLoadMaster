#include <iostream>
#include <vector>
#include <string>

using namespace std;

struct Processor {
    string name;
    int weight;
    int current_weight;
    vector<string> task_queue;

    Processor(string n, int w) : name(n), weight(w), current_weight(0) {}

    void assign_task(string task) {
        task_queue.push_back(task);
        cout << "Task " << task << " assigned to " << name << endl;
    }
};

class WeightedRoundRobinScheduler {
private:
    vector<Processor*> processors;
    int total_weight;

public:
    WeightedRoundRobinScheduler(vector<Processor*> procs) : processors(procs) {
        total_weight = 0;
        for (auto p : processors) {
            total_weight += p->weight;
        }
    }

    void schedule(vector<string> tasks) {
        for (string task : tasks) {
            Processor* chosen = nullptr;
            int max_weight = -1;

            for (auto p : processors) {
                p->current_weight += p->weight;
                if (p->current_weight > max_weight) {
                    max_weight = p->current_weight;
                    chosen = p;
                }
            }
            chosen->assign_task(task);
            chosen->current_weight -= total_weight;
        }
    }
};

int main() {
    Processor p1("CPU1", 5);
    Processor p2("CPU2", 3);
    Processor p3("CPU3", 2);

    vector<Processor*> processors = {&p1, &p2, &p3};
    WeightedRoundRobinScheduler scheduler(processors);

    vector<string> tasks = {"T1", "T2", "T3", "T4", "T5", "T6", "T7", "T8", "T9", "T10"};
    scheduler.schedule(tasks);
}
