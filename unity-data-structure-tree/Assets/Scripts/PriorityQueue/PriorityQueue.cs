using System;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<TElement, TPriority>
{
    private List<(TElement Element, TPriority Priority)> heap = new();

    public (TElement Element, TPriority Priority) this[int i]
    {
        get => heap[i];
        set => heap[i] = value;
    }

    public int Count
    {
        get { return heap.Count; }
    }

    public void EnqueueMin(TElement element, TPriority priority)
    {
        heap.Add((element, priority));

        var i = heap.Count - 1;

        while (i != 0)
        {
            var compare = heap[i].CompareTo(heap[(i - 1) / 2]);

            if (compare < 0)
            {
                var temp = heap[i];
                heap[i] = heap[(i - 1) / 2];
                heap[(i - 1) / 2] = temp;
                i = (i - 1) / 2;
            }

            else
            {
                break;
            }
        }
    }

    public void EnqueueMax(TElement element, TPriority priority)
    {
        heap.Add((element, priority));

        var i = heap.Count - 1;

        while (i != 0)
        {
            var compare = heap[i].CompareTo(heap[(i - 1) / 2]);

            if (compare > 0)
            {
                var temp = heap[i];
                heap[i] = heap[(i - 1) / 2];
                heap[(i - 1) / 2] = temp;
                i = (i - 1) / 2;
            }

            else
            {
                break;
            }
        }
    }

    public TElement DequeueMin()
    {
        var result = Peek();

        heap[0] = heap[Count - 1];
        heap.RemoveAt(Count - 1);

        var i = 0;

        while (2 * i + 1 < Count)   // 자식이 없을 때까지
        {
            int compare;
            int compareChildIndex;

            if (2 * i + 2 < Count)  // 오른쪽 자식이 있는 경우
            {
                compare = heap[2 * i + 1].CompareTo(heap[2 * i + 2]);

                if (compare < 0)    compareChildIndex = 2 * i + 1;
                else                compareChildIndex = 2 * i + 2;
            }

            else
            {
                compareChildIndex = 2 * i + 1;
            }

            compare = heap[i].CompareTo(heap[compareChildIndex]);

            if (compare > 0)
            {
                var temp = heap[i];
                heap[i] = heap[compareChildIndex];
                heap[compareChildIndex] = temp;
                i = compareChildIndex;
            }

            else
            {
                break;
            }
        }

        return result;
    }

    public TElement DequeueMax()
    {
        var result = Peek();

        heap[0] = heap[Count - 1];
        heap.RemoveAt(Count - 1);

        var i = 0;

        while (2 * i + 1 < Count)   // 자식이 없을 때까지
        {
            int compare;
            int compareChildIndex;

            if (2 * i + 2 < Count)  // 오른쪽 자식이 있는 경우
            {
                compare = heap[2 * i + 1].CompareTo(heap[2 * i + 2]);

                if (compare > 0) compareChildIndex = 2 * i + 1;
                else compareChildIndex = 2 * i + 2;
            }

            else
            {
                compareChildIndex = 2 * i + 1;
            }

            compare = heap[i].CompareTo(heap[compareChildIndex]);

            if (compare < 0)
            {
                var temp = heap[i];
                heap[i] = heap[compareChildIndex];
                heap[compareChildIndex] = temp;
                i = compareChildIndex;
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
        return heap[0].Element;
    }

    public void Clear()
    {
        heap.Clear();
    }
}