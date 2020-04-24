using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Queue<T> : ICloneable
    {
        T[] array;
        int head, tail;
        bool isLooped;

        bool isFull() => this.head == this.tail && this.isLooped;
        bool isEmpty() => this.head == this.tail && !this.isLooped;

        public Queue(int size)
        {
            this.array = new T[size];
            this.head = this.tail = 0;
            this.isLooped = false;
        }

        public void Push(T element)
        {
            if (this.isFull())
                throw new InvalidOperationException();
            this.array[this.tail] = element;
            //Сдвигаем хвост на 1 и, если он превосходит длину массива, помещаем в начало и ставим флаг зацикленности
            this.tail = (this.tail + 1) % this.array.Length; 
            if (this.tail == 0)
                this.isLooped = true;
        }

        public T Pop()
        {
            if (this.isEmpty())
                throw new InvalidOperationException();
            T element = this.array[this.head];
            //Сдвигаем голову на 1 и, если он превосходит длину массива, помещаем в начало и убираем флаг зацикленности
            this.head = (this.head + 1) % this.array.Length;
            if (this.head == 0)
                this.isLooped = false;
            return element;
        }

        public static Queue<T> Combine(Queue<T> first, Queue<T> second)
        {
            int size = first.array.Length + second.array.Length;
            Queue<T> queue = new Queue<T>(size);
            int pointer = first.head;
            bool isLooped = false;
            //Добавляем в новую очередь все элементы первой очереди с учетом цикла в массиве
            while (pointer != first.tail || isLooped != first.isLooped)
            {
                queue.Push(first.array[pointer]);
                pointer = (pointer + 1) % first.array.Length;
                if (pointer == 0)
                    isLooped = true;
            }
            pointer = second.head;
            isLooped = false;
            //Добавляем в новую очередь все элементы второй очереди с учетом цикла в массиве
            while (pointer != second.tail || isLooped != second.isLooped)
            {
                queue.Push(second.array[pointer]);
                pointer = (pointer + 1) % second.array.Length;
                if (pointer == 0)
                    isLooped = true;
            }
            return queue;
        }

        public object Clone()
        {
            Queue<T> queue = new Queue<T>(this.array.Length);
            queue.array = (T[])this.array.Clone();
            queue.head = this.head;
            queue.tail = this.tail;
            queue.isLooped = this.isLooped;
            return queue;
        }
    }
}
