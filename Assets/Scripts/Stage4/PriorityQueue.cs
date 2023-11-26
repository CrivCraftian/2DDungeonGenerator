using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<T>
{
    private HashSet<T> _items = new HashSet<T>();
    private List<Tuple<T, float>> _queue = new List<Tuple<T, float>>();

    public int Count() { return _queue.Count; }

    public void Enqueue(T item, float priority)
    {
        _items.Add(item);
        _queue.Add(Tuple.Create(item, priority));
    }

    public T Dequeue()
    {
        int bestIndex = 0;

        for (int i = 1;  i < _queue.Count ; i++)
        {
            if (_queue[i].Item2 < _queue[bestIndex].Item2)
            {
                bestIndex = i;
            }
        }

        T bestItem = _queue[bestIndex].Item1;
        _items.Remove(bestItem);
        _queue.RemoveAt(bestIndex);

        return bestItem;
    }

    public bool Contains(T element)
    {
        return _items.Contains(element);
    }

    public float GetPriority(T item)
    {
        foreach (var tuple in _queue)
        {
            if (EqualityComparer<T>.Default.Equals(tuple.Item1, item))
            {
                return tuple.Item2;
            }
        }
        throw new ArgumentException("Item not found in the priority queue.");
    }

    public void SetPriority(T item, float priority)
    {
        for (int i = 0; i < _queue.Count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(_queue[i].Item1, item))
            {
                _queue[i] = Tuple.Create(item, priority);
                return;
            }
        }
        throw new ArgumentException("Item not found in the priority queue.");
    }
}
