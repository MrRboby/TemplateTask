using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> queue = new Queue<int>(3);
            queue.Push(3);
            queue.Push(2);
            int k = queue.Pop();
            queue.Push(1);
            queue.Push(0);
            Queue<int> queue2 = new Queue<int>(5);
            queue2.Push(10);
            queue2.Push(9);
            queue2.Push(8);
            queue2.Push(7);
            queue2.Push(6);
            Queue<int> q3 = (Queue<int>)queue2.Clone();
            Console.WriteLine(q3.Pop());
            Queue<int> qc = Queue<int>.Combine(queue, q3);
            for(int i = 0; i < 7; i++)
                Console.Write("{0} ", qc.Pop());
            Console.ReadKey();
        }
    }
}
