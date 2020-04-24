using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            AVLTree<int> tree = new AVLTree<int>(10);
            tree.Insert(5);
            tree.Insert(15);
            tree.Insert(7);
            tree.Insert(6);
            tree.Insert(2);
            tree.Insert(12);
            tree.Delete(6);
            bool b = tree.Contains(4);
            int[] arr = tree.Array;
            foreach (int i in arr)
                Console.Write($"{i} ");
            Console.WriteLine();
            List<int> lst = tree.Leaves;
            foreach (int i in lst)
                Console.Write($"{i} ");
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
