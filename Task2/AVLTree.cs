using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    
    class AVLTree<T> where T : IComparable
    {
        class AVLNode
        {
            public T data;
            int height;
            public AVLNode left, right;
            public int Height => height;

            public AVLNode(T data)
            {
                this.data = data;
                this.height = 1;
                this.left = null;
                this.right = null;
            }

            public int Balance
            {
                get
                {
                    int leftHeight, rightHeight;
                    leftHeight = (this.left != null) ? this.left.height : 0;
                    rightHeight = (this.right != null) ? this.right.height : 0;
                    return rightHeight - leftHeight;
                }
            }

            public void MakeNewHeight()
            {
                int leftHeight, rightHeight;
                leftHeight = (this.left != null) ? this.left.height : 0;
                rightHeight = (this.right != null) ? this.right.height : 0;
                this.height = Math.Max(leftHeight, rightHeight) + 1;
            }
        }

        AVLNode root;
        bool hasChanges;
        int count;

        public AVLTree()
        {
            this.root = null;
            this.hasChanges = false;
            this.count = 0;
        }

        public AVLTree(T data)
        {
            this.root = new AVLNode(data);
            this.hasChanges = false;
            this.count = 1;
        }

        void balance(ref AVLNode pointer)
        {
            int oldHeight = pointer.Height;
            pointer.MakeNewHeight();
            int balance = pointer.Balance;
            if (balance > 1)
            {
                if (pointer.right.Balance < 0)
                    this.turnLeft(ref pointer.right);
                this.turnRight(ref pointer);
                if (pointer.Height == oldHeight)
                    this.hasChanges = false;
            }
            else if (balance < -1)
            {
                if (pointer.left.Balance < 0)
                    this.turnRight(ref pointer.left);
                this.turnLeft(ref pointer);
                if (pointer.Height == oldHeight)
                    this.hasChanges = false;
            }
        }

        void turnLeft(ref AVLNode pointer)
        {
            AVLNode temp = pointer.left;
            pointer.left = temp.right;
            temp.right = pointer;
            pointer.MakeNewHeight();
            temp.MakeNewHeight();
            pointer = temp;
        }

        void turnRight(ref AVLNode pointer)
        {
            AVLNode temp = pointer.right;
            pointer.right = temp.left;
            temp.left = pointer;
            pointer.MakeNewHeight();
            temp.MakeNewHeight();
            pointer = temp;
        }

        void _insert(ref AVLNode pointer, T data)
        {
            if (pointer == null)
            {
                this.hasChanges = true;
                pointer = new AVLNode(data);
            }
            else
            {
                if (data.CompareTo(pointer.data) == -1)
                {
                    this._insert(ref pointer.left, data);
                    if (this.hasChanges)
                        this.balance(ref pointer);
                }
                else
                {
                    this._insert(ref pointer.right, data);
                    if (this.hasChanges)
                        this.balance(ref pointer);
                }
            }
        }

        public void Insert(T data) //Вставка элемента
        {
            this._insert(ref this.root, data);
            this.count += 1;
        }

        void findToDelete(ref AVLNode replaceable, AVLNode pointer, ref AVLNode temp)
        {
            if (replaceable.right != null)
            {
                this.findToDelete(ref replaceable.right, pointer, ref temp);
                this.balance(ref replaceable);
            }
            else
            {
                temp = replaceable;
                pointer.data = replaceable.data;
                replaceable = replaceable.left;
            }
        }

        void _delete(ref AVLNode pointer, T data, out bool isFound)
        {
            isFound = false;
            if (pointer != null)
            {
                if (data.CompareTo(pointer.data) == -1)
                {
                    this._delete(ref pointer.left, data, out isFound);
                    this.balance(ref pointer);
                }
                else if (data.CompareTo(pointer.data) == 1)
                {
                    this._delete(ref pointer.right, data, out isFound);
                    this.balance(ref pointer);
                }
                else
                {
                    isFound = true;
                    AVLNode temp = pointer;
                    if (pointer.right == null)
                        pointer = pointer.left;
                    else if (pointer.left == null)
                        pointer = pointer.right;
                    else
                        this.findToDelete(ref pointer.left, pointer, ref temp);
                }
            }
        }

        public void Delete(T data) //Удаление элемента
        {
            this._delete(ref this.root, data, out bool isFound);
            if (!isFound)
                throw new ArgumentException();
            else 
                this.count -= 1;
        }

        bool _contains(AVLNode pointer, T data)
        {
            if (pointer != null)
            {
                if (data.CompareTo(pointer.data) == -1)
                    return this._contains(pointer.left, data);
                else if (data.CompareTo(pointer.data) == 1)
                    return this._contains(pointer.right, data);
                else
                    return true;
            }
            return false;
        }

        public bool Contains(T data) //Поиск по дереву
        {
            return this._contains(this.root, data);
        }

        public int Count => count; //Число элементов дерева

        void _toArray(AVLNode treePointer, T[] array, ref int arrayPointer)
        {
            if (treePointer.left != null)
                _toArray(treePointer.left, array, ref arrayPointer);
            array[arrayPointer] = treePointer.data;
            arrayPointer += 1;
            if (treePointer.right != null)
                _toArray(treePointer.right, array, ref arrayPointer);
        }

        public T[] Array //Список элементов дерева
        {
            get
            {
                T[] array = new T[this.count];
                int arrayPointer = 0;
                this._toArray(this.root, array, ref arrayPointer);
                return array;
            }
        }

        void _leaves(AVLNode pointer, ref List<T> list)
        {
            if (pointer.left != null)
                _leaves(pointer.left, ref list);
            if (pointer.right != null)
                _leaves(pointer.right, ref list);
            if (pointer.left == null && pointer.right == null)
                list.Add(pointer.data);
        }

        public List<T> Leaves //Список листьев
        {
            get
            {
                List<T> list = new List<T>();
                _leaves(this.root, ref list);
                return list;
            }
        }
    }
}
