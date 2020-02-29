#pragma once

template <class T> 
class AVLtree;

template <class T>
class AVLnode {
private:
	T data; 
	int height;
	AVLnode* left, * right;
	AVLnode(T data) {
		this->data = data;
		this->height = 1;
		this->left = nullptr;
		this->right = nullptr;
	}
	void makeNewHeight() {
		int leftHeight, rightHeight;
		if (this->left != nullptr)
			leftHeight = this->left->height;
		else
			leftHeight = 0;
		if (this->right != nullptr)
			rightHeight = this->right->height;
		else
			rightHeight = 0;
		this->height = leftHeight > rightHeight ? leftHeight + 1 : rightHeight + 1;
	}
	int getBalance() {
		int leftHeight, rightHeight;
		if (this->left != nullptr)
			leftHeight = this->left->height;
		else
			leftHeight = 0;
		if (this->right != nullptr)
			rightHeight = this->right->height;
		else
			rightHeight = 0;
		return rightHeight - leftHeight;
	}
public:
	friend class AVLtree<T>;
};


template <class T>
class AVLtree {
private:
	AVLnode<T>* root;
	bool hasChanges;
	int count;
	void _insert(AVLnode<T>*& pointer, T data) {
		if (pointer == nullptr) {
			this->hasChanges = true;
			pointer = new AVLnode<T>(data);
		}
		else {
			if (data < pointer->data) {
				this->_insert(pointer->left, data);
				if (this->hasChanges)
					this->_balance(pointer);
			}
			else {
				this->_insert(pointer->right, data);
				if (this->hasChanges)
					this->_balance(pointer);
			}
		}
	}
	void _del(AVLnode<T>*& pointer, T data) {
		AVLnode<T>* temp;
		if (pointer != nullptr) {
			if (data < pointer->data) {
				this->_del(pointer->left, data);
				this->_balance(pointer);
			}
			else if (data > pointer->data) {
				this->_del(pointer->right, data);
				this->_balance(pointer);
			}
			else {
				temp = pointer;
				if (pointer->right == nullptr)
					pointer = pointer->left;
				else if (pointer->left == nullptr)
					pointer = pointer->right;
				else
					this->_findToDelete(pointer->left, pointer, temp);
				delete temp;
			}
		}
	}
	void _findToDelete(AVLnode<T>*& replaceable, AVLnode<T>* pointer, AVLnode<T>*& temp) {
		if (replaceable->right != nullptr) {
			this->_findToDelete(replaceable->right, pointer, temp);
			this->_balance(replaceable);
		}
		else {
			temp = replaceable;
			pointer->data = replaceable->data;
			replaceable = replaceable->left;
		}
	}
	bool _contains(AVLnode<T>* pointer, T data) {
		if (pointer != nullptr) {
			if (data == pointer->data)
				return true;
			else if (data < pointer->data)
				return this->_contains(pointer->left, data);
			else if (data > pointer->data)
				return this->_contains(pointer->right, data);
		}
		return false;
	}
	void _balance(AVLnode<T>*& pointer) {
		int oldHeight = pointer->height;
		pointer->makeNewHeight();
		int balance = pointer->getBalance();
		if (balance > 1) {
			if (pointer->right->getBalance() < 0)
				this->_turnLeft(pointer->right);
			this->_turnRight(pointer);
			if (pointer->height == oldHeight)
				this->hasChanges = false;
		}
		else if (balance < -1) {
			if (pointer->left->getBalance() > 0)
				this->_turnRight(pointer->left);
			this->_turnLeft(pointer);
			if (pointer->height == oldHeight)
				this->hasChanges = false;
		}
	}
	void _turnLeft(AVLnode<T>*& pointer) {
		AVLnode<T>* temp;
		temp = pointer->left;
		pointer->left = temp->right;
		temp->right = pointer;
		pointer->makeNewHeight();
		temp->makeNewHeight();
		pointer = temp;
	}
	void _turnRight(AVLnode<T>*& pointer) {
		AVLnode<T>* temp;
		temp = pointer->right;
		pointer->right = temp->left;
		temp->left = pointer;
		pointer->makeNewHeight();
		temp->makeNewHeight();
		pointer = temp;
	}
	void _dispose(AVLnode<T>* pointer) {
		if (pointer != nullptr) {
			if (pointer->left != nullptr)
				this->_dispose(pointer->left);
			if (pointer->right != nullptr)
				this->_dispose(pointer->right);
			delete pointer;
		}
	}
	void _toArray(AVLnode<T>* treePointer, T* array, int& arrayPointer) {
		if (treePointer->left != nullptr)
			_toArray(treePointer->left, array, arrayPointer);
		array[arrayPointer] = treePointer->data;
		arrayPointer++;
		if (treePointer->right != nullptr)
			_toArray(treePointer->right, array, arrayPointer);
	}
public:
	AVLtree() {
		this->root = nullptr;
		this->hasChanges = false;
		this->count = 0;
	}
	AVLtree(T data) {
		this->root = new AVLnode<T>(data);
		this->hasChanges = false;
		this->count = 1;
	}
	~AVLtree() {
		this->_dispose(this->root);
	}
	void insert(T data) {
		this->_insert(this->root, data);
		this->count++;
	}
	void del(T data) {
		this->_del(this->root, data);
		this->count--;
	}
	bool contains(T data) {
		return this->_contains(this->root, data);
	}
	int getCount() {
		return this->count;
	}
	T* toArray() {
		T* array = new T[this->count];
		int arrayPointer = 0;
		_toArray(this->root, array, arrayPointer);
		return array;
	}
};