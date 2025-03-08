#include <iostream>
#include <queue>
using namespace std;
struct p{
   int id,bursttime,remainingtime;
};
void roundrobin(p processes[],int n,int timeQ){
queue<p>q;
for(int i=0;i<n;i++)
	q.push(processes[i]);
int time=0;
while(!q.empty()){
p current=q.front();
q.pop();
int etime=min(current.remainingtime,timeQ);
time+=etime;
current.remainingtime-=etime;
cout<<"Process "<<current.id<<" executed for"<<etime<<" unit. Time: "<<time<<endl;
if(current.remainingtime>0)
	q.push(current);
	}
}
int main(){
p processes[]={{1,10,10},{2,5,5},{3,8,8}};
int n=sizeof(processes)/sizeof(processes[0]);
int timeq=3;
cout<<"RR scheduling:\n";
roundrobin(processes,n,timeq);
return 0;
}
