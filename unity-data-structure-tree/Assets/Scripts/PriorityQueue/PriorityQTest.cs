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

            for (int i = 0;  i < pq.Count; i++)
            {
                Debug.Log(pq[i].Element);
            }

            Debug.Log("ㅡㅡㅡㅡㅡ");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (pq.Count == 0)
            {
                return;
            }

            Debug.Log($"Dequeue: {pq.Dequeue()}");

            for (int i = 0; i < pq.Count; i++)
            {
                Debug.Log(pq[i].Element);
            }

            Debug.Log("ㅡㅡㅡㅡㅡ");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            pq.Clear();
            Debug.Log("클리어");
        }
    }
}