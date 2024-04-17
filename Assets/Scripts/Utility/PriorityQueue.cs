using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

// temporarily stolen from chatgpt
// TODO
public class PriorityQueue<T>
{
    private List<Tuple<T, int>> heap;
    private Dictionary<T, int> itemToIndex;

    public PriorityQueue()
    {
        heap = new List<Tuple<T, int>>();
        itemToIndex = new Dictionary<T, int>();
    }

    public int Count
    {
        get { return heap.Count; }
    }

    public void Enqueue(T item, int priority)
    {
        heap.Add(Tuple.Create(item, priority));
        int index = heap.Count - 1;
        itemToIndex[item] = index;
        BubbleUp(index);
    }

    public T Dequeue()
    {
        if (heap.Count == 0)
            throw new InvalidOperationException("Priority queue is empty");

        Tuple<T, int> frontItem = heap[0];
        int lastIndex = heap.Count - 1;
        heap[0] = heap[lastIndex];
        heap.RemoveAt(lastIndex);
        itemToIndex.Remove(frontItem.Item1);
        if (heap.Count > 0)
            BubbleDown(0);
        return frontItem.Item1;
    }

    public void UpdatePriority(T item, int priority)
    {
        int index = itemToIndex[item];
        heap[index] = Tuple.Create(item, priority);
        BubbleUp(index);
        BubbleDown(index);
    }

    private void BubbleUp(int index)
    {
        while (index > 0)
        {
            int parentIndex = (index - 1) / 2;
            if (heap[index].Item2 >= heap[parentIndex].Item2)
                break;

            Swap(index, parentIndex);
            index = parentIndex;
        }
    }

    private void BubbleDown(int index)
    {
        while (true)
        {
            int leftChildIndex = 2 * index + 1;
            int rightChildIndex = 2 * index + 2;
            int smallestChildIndex = leftChildIndex;

            if (rightChildIndex < heap.Count && heap[rightChildIndex].Item2 < heap[leftChildIndex].Item2)
                smallestChildIndex = rightChildIndex;

            if (leftChildIndex >= heap.Count || heap[smallestChildIndex].Item2 >= heap[index].Item2)
                break;

            Swap(index, smallestChildIndex);
            index = smallestChildIndex;
        }
    }

    private void Swap(int i, int j)
    {
        Tuple<T, int> temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
        itemToIndex[heap[i].Item1] = i;
        itemToIndex[heap[j].Item1] = j;
    }
}
