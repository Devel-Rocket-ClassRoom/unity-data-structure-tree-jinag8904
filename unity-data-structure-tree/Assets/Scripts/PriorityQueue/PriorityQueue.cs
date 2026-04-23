using System;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<TElement, TPriority>
{
    private List<(TElement Element, TPriority Priority)> priorityQueue = new();

    public (TElement Element, TPriority Priority) this[int i]
    {
        get => priorityQueue[i];
        set => priorityQueue[i] = value;
    }

    public int Count
    {
        get { return priorityQueue.Count; }
    }

    public void Enqueue(TElement element, TPriority priority)
    {
        priorityQueue.Add((element, priority));

        var i = priorityQueue.Count - 1;

        while (i != 0)
        {
            var compare = priorityQueue[i].CompareTo(priorityQueue[(i - 1) / 2]);

            if (compare < 0)
            {
                var temp = priorityQueue[i];
                priorityQueue[i] = priorityQueue[(i - 1) / 2];
                priorityQueue[(i - 1) / 2] = temp;
                i = (i - 1) / 2;
            }

            else
            {
                break;
            }
        }
    }

    public TElement Dequeue()
    {
        var result = Peek();

        priorityQueue[0] = priorityQueue[Count - 1];
        priorityQueue.RemoveAt(Count - 1);

        var i = 0;

        while (2 * i + 1 < Count -1)
        {
            var compare = priorityQueue[i].CompareTo(priorityQueue[2 * i + 1]);

            if (compare > 0)
            {
                var temp = priorityQueue[i];
                priorityQueue[i] = priorityQueue[2 * i + 1];
                priorityQueue[2 * i + 1] = temp;
                i = 2 * i + 1;
            }

            else
            {
                break;
            }
        }

        while (2 * i + 2 < Count -1)
        {
            var compare = priorityQueue[i].CompareTo(priorityQueue[2 * i + 2]);

            if (compare > 0)
            {
                var temp = priorityQueue[i];
                priorityQueue[i] = priorityQueue[2 * i + 2];
                priorityQueue[2 * i + 2] = temp;
                i = 2 * i + 2;
            }

            else
            {
                break;
            }
        }

        return result;
    }

    public TElement Peek()
    {
        return priorityQueue[0].Element;
    }

    public void Clear()
    {
        priorityQueue.Clear();
    }
}