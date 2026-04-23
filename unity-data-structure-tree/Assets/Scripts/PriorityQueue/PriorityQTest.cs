using UnityEngine;
using System.Collections;

public class PriorityQTest : MonoBehaviour
{
    PriorityQueue<int, int> pq = new();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            pq.Enqueue(Random.Range(0, 100), Random.Range(0, 100));
            Debug.Log($"Count: {pq.Count}");

            for (int i = 0;  i < pq.Count; i++)
            {
                Debug.Log(pq[i].Element);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (pq.Count == 0)
            {
                Debug.Log("Count: 0");
                return;
            }

            Debug.Log($"Dequeue: {pq.Dequeue()}");
            Debug.Log($"Count: {pq.Count}");

            for (int i = 0; i < pq.Count; i++)
            {
                Debug.Log(pq[i].Element);
            }
        }
    }
}