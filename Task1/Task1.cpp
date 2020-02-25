#include <iostream>
#include "Queue.h"

int main()
{
    Queue<int> que = Queue<int>(3);
    que.push(1);
    que.push(2);
    que.push(3);
    Queue<int> que2 = Queue<int>(2);
    que2.push(4);
    que2.push(5);
    Queue<int> combinedQue = combineQueues(que, que2);
    for (int i = 0; i < 5; i++)
        std::cout << combinedQue.pop() << std::endl;
    return 0;
}