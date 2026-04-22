using UnityEngine;

public class TreeTest : MonoBehaviour
{
    private void Start()
    {
        var bst = new BinarySearchTree<string, string>();
        bst["123"] = "ABC";
        bst["1"] = "DFE";
        bst["10"] = "GDA";
        bst["55"] = "JAS";
        bst["123"] = "MWE";

        foreach (var pair in bst)
        {
            Debug.Log(pair);
        }

        Debug.Log($"123 Contains: {bst.ContainsKey("123")}");
        bst.Remove("123");
        Debug.Log($"123 Contains: {bst.ContainsKey("123")}");

        foreach (var pair in bst.InOrderTraversal()) Debug.Log(pair);
        foreach (var pair in bst.PreOrderTraversal()) Debug.Log(pair);
        foreach (var pair in bst.PostOrderTraversal()) Debug.Log(pair);
        foreach (var pair in bst.LevelOrderTraversal()) Debug.Log(pair);
    }
}