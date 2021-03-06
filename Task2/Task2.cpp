﻿#include <iostream>
#include <list>
#include "AVLTree.h"

int main() {
    AVLtree<int> tree = AVLtree<int>(5);
    tree.insert(10);
    tree.insert(2);
    tree.insert(7);
    tree.insert(15);
    tree.insert(8);
    tree.insert(6);
    tree.insert(1);
    tree.del(8);
    int* arr = tree.toArray();
    for (int i = 0; i < tree.getCount(); i++)
        std::cout << arr[i] << " ";
    std::cout << "\n";
    delete[] arr;
    std::list<int> lst = tree.leavesList();
    for (int i : lst)
        std::cout << i << " ";
    return 0;
}