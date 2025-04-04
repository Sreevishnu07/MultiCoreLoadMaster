#include <iostream>
#include <vector>
#include <string>

using namespace std;

class SimpleScheduler {
private:
    vector<string> processors;
    vector<int> load; // Number of tasks for each processor

public:
    SimpleScheduler(vector<string> names) {
        processors = names;  //saving processor name
        load = vector<int>(processors.size(), 0); // set all task to 0
    }

    void assignTask(string task) {
        int leastBusy = 0;
        // Finding processor with least number of task
        for (int i = 1; i < load.size(); i++) {
            if (load[i] < load[leastBusy]) {
                leastBusy = i;
            }
        }
        //Assigned task to  least loaded processor and increase its task count by 1 
        cout << task << " assigned to " << processors[leastBusy] << endl;
        load[leastBusy]++;
    }

    void showLoad() {
        cout << "\nProcessor Load:\n";
        for (int i = 0; i < processors.size(); i++) {
            cout << processors[i] << ": " << load[i] << " task(s)\n";
        }
    }
};

int main() {
    vector<string> processors = {"P1", "P2", "P3"};
    SimpleScheduler scheduler(processors); //we created three process and assigned it to schedular

    vector<string> tasks = {"T1", "T2", "T3", "T4", "T5"};

    for (string task : tasks) {
        scheduler.assignTask(task);
    }

    scheduler.showLoad();

    return 0;
}
