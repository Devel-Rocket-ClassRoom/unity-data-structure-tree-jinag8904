using System.Collections.Generic;
using UnityEngine;

public class TreeVisualize: MonoBehaviour
{
    public enum Mode { Pow, LevelOrder, InOrder }
    public Mode mode = Mode.Pow;

    BinarySearchTree<int, int> bst = new();
    public KeyValuePair<int, int> root;

    public GameObject nodePrefab;

    private void Start()
    {
        root = new KeyValuePair<int, int>(Random.Range(0, 1000), Random.Range(0, 1000));
        bst.Add(root);

        UpdateTree();
    }

    private void OnValidate()
    {
        UpdateTree();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var key = Random.Range(0, 1000);
            var value = Random.Range(0, 1000);
            bst.Add(key, value);
                
            UpdateTree();
        }
    }

    private void UpdateTree()   // 순회, 계산, 시각화
    {
        switch (mode)
        {
            case Mode.Pow:
                Pow();
                break;
            case Mode.LevelOrder:
                LevelOrder();
                break;
            case Mode.InOrder:
                InOrder();
                break;
        }
    }

    private void Pow()  // 레벨이 내려갈수록 간격이 절반으로 줄어든다.
    {
        foreach (var pair in bst.LevelOrderTraversal())
        {

            Vector3 pos = Vector3.zero;
            Instantiate(nodePrefab, pos, Quaternion.identity);
        }
    }

    private void LevelOrder()   // 좌측부터 균등 배치함
    {
        foreach (var pair in bst.LevelOrderTraversal())
        {
        }
    }

    private void InOrder()  // x 인덱스를 증가시켜 배치. (x 좌표 순서 = 키 오름차순)
    {
        foreach (var pair in bst.InOrderTraversal())
        {
        }
    }
}