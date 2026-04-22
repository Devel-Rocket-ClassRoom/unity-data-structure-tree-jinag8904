using System.Collections.Generic;
using UnityEngine;

public class TreeVisualize : MonoBehaviour
{
    private readonly Dictionary<object, Vector3> nodePositions = new();
    public float horizontalSpacing = 2.0f;
    public float verticalSpacing = 2.0f;

    public enum VisualizeMode { Pow, LevelOrder, InOrder }
    public VisualizeMode mode = VisualizeMode.Pow;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

        }
    }

    private void AssignPositionsPow<TKey, TValue>(TreeNode<TKey, TValue> node, Vector3 position, int height)
    {
        if (node == null) return;

        nodePositions[node] = position;

        float offset = horizontalSpacing * 0.5f * Mathf.Pow(2, height);

        Vector3 childBase = position + Vector3.down * verticalSpacing;

        AssignPositionsPow(node.Left, childBase + Vector3.left * offset, height -1);
        AssignPositionsPow(node.Right, childBase + Vector3.right * offset, height -1);
    }

    private void AssignPositionsLevelOrder<TKey, TValue>(TreeNode<TKey, TValue> root)
    {
        var levels = new List<List<TreeNode<TKey, TValue>>>();
        var queue = new Queue<(TreeNode<TKey, TValue> node, int depth)>();

        queue.Enqueue((root, 0));

        while (queue.Count > 0)
        {
            var (node, depth) = queue.Dequeue();

            // TODO: levels 리스트 크기가 depth보다 작으면 빈 List를 추가해 늘려준다
            while (levels.Count <= depth) 
            { 
                levels.Add(new List<TreeNode<TKey, TValue>>());
            }

            levels[depth].Add(node);

            // TODO: 좌/우 자식을 depth + 1로 큐에 넣기
            if (node.Left != null)
                queue.Enqueue((node.Left, depth + 1));

            if (node.Right != null)
                queue.Enqueue((node.Right, depth + 1));
        }

        for (int depth = 0; depth < levels.Count; depth++)
        {
            float y = -depth * verticalSpacing;
            var row = levels[depth];

            for (int i = 0; i < row.Count; i++)
            {
                // TODO: i번째 노드의 x좌표는?
                nodePositions[row[i]] = new Vector3(i * horizontalSpacing, y, 0f);
            }
        }
    }

    private void AssignPositionsInOrder<TKey, TValue>(TreeNode<TKey, TValue> node, int depth, ref int xIndex)
    {
        if (node == null) return;

        // TODO: 왼쪽 서브트리 먼저 방문 (depth + 1)
        
        ;

        // TODO: 자신의 좌표 기록 — x는 xIndex 기반, y는 depth 기반
        nodePositions[node] = new Vector3(/* ??? */, /* ??? */, 0f);
        xIndex++;

        // TODO: 오른쪽 서브트리 방문 (depth + 1)
        /* ??? */
        ;
    }
}
