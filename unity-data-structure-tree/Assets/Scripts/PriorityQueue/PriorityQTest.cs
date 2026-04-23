using UnityEngine;
using System.Collections;

public class PriorityQTest : MonoBehaviour
{
    PriorityQueue<int, int> pq = new();

    public enum Mode { Min, Max }
    public Mode mode = Mode.Min;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            switch (mode)
            {
                case Mode.Min:
                    pq.EnqueueMin(Random.Range(0, 100), Random.Range(0, 100));
                    break;
                case Mode.Max:
                    pq.EnqueueMax(Random.Range(0, 100), Random.Range(0, 100));
                    break;
            }

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

            switch (mode)
            {
                case Mode.Min:
                    Debug.Log($"Dequeue: {pq.DequeueMin()}");
                    break;
                case Mode.Max:
                    Debug.Log($"Dequeue: {pq.DequeueMax()}");
                    break;
            }

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