#pragma once
#include <algorithm>
#include <stdexcept>

template <class T>
class Queue;
template<class T>
Queue<T> combineQueues(const Queue<T>& first, const Queue<T>& second);

template <class T>
class Queue
{
private:
	int size;
	T* array;
	int head, tail;
	bool isLooped;
	bool isFull() {
		return this->head == this->tail && this->isLooped;
	}
	bool isEmpty() {
		return this->head == this->tail && !this->isLooped;
	}
public:
	Queue(int size) {
		this->size = size;
		this->array = new T[this->size];
		this->head = 0;
		this->tail = 0;
		this->isLooped = false;
	}
	Queue(const Queue& queue) {
		this->size = queue.size;
		this->array = new T[this->size];
		std::copy(queue.array, queue.array + queue.size, this->array);
		this->head = queue.head;
		this->tail = queue.tail;
		this->isLooped = queue.isLooped;
	}
	~Queue() {
		delete[] array;
	}
	void push(T element) {
		if (this->isFull())
			throw std::out_of_range("Queue is full");
		this->array[this->tail] = element;
		this->tail = (this->tail + 1) % this->size;
		if (this->tail == 0)
			this->isLooped = true;
	}
	T pop() {
		if (this->isEmpty())
			throw std::out_of_range("Queue is empty");
		T element = this->array[this->head];
		this->head = (this->head + 1) % this->size;
		if (this->head == 0)
			this->isLooped = false;
		return element;
	}
	friend Queue combineQueues<T>(const Queue<T>& first, const Queue<T>& second);
};

template<class T>
Queue<T> combineQueues(const Queue<T>& first, const Queue<T>& second) {
	int size = first.size + second.size;
	Queue<T> queue = Queue<T>(size);
	int pointer = first.head;
	bool isLooped = false;
	while (pointer != first.tail || pointer == first.tail && isLooped != first.isLooped) {
		queue.push(first.array[pointer]);
		pointer = (pointer + 1) % first.size;
		if (pointer == 0)
			isLooped = true;
	}
	pointer = second.head;
	isLooped = false;
	while (pointer != second.tail || pointer == second.tail && isLooped != second.isLooped) {
		queue.push(second.array[pointer]);
		pointer = (pointer + 1) % second.size;
		if (pointer == 0)
			isLooped = true;
	}
	return queue;
}