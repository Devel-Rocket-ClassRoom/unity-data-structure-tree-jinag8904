using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BinarySearchTree<TKey, TValue> : IDictionary<TKey, TValue> where TKey : IComparable<TKey>
{
    protected TreeNode<TKey, TValue> root;

    public BinarySearchTree()
    {
        root = null;
    }

    public TValue this[TKey key] 
    { 
        get
        {
            if (TryGetValue(key, out TValue value))
            {
                return value;
            }

            throw new KeyNotFoundException($"키 {key} 없음");
        }

        set => root = AddOrUpdate(root, key, value);
    }

    public ICollection<TKey> Keys => InOrderTraversal().Select(keyValuePair => keyValuePair.Key).ToList();
    public ICollection<TValue> Values => InOrderTraversal().Select(keyValuePair => keyValuePair.Value).ToList();

    public int Count => CountNodes(root);
    protected virtual int CountNodes(TreeNode<TKey, TValue> node)
    {
        if (node == null) return 0;

        return 1 + CountNodes(node.Left) + CountNodes(node.Right);
    }

    public bool IsReadOnly => false;

    public void Add(KeyValuePair<TKey, TValue> item)
    {
        Add(item.Key, item.Value);
    }

    public void Add(TKey key, TValue value)
    {
        root = Add(root, key, value);
    }

    protected virtual TreeNode<TKey, TValue> Add(TreeNode<TKey, TValue> node, TKey key, TValue value)
    {
        if (node == null)
            return new TreeNode<TKey, TValue>(key, value);

        int compare = key.CompareTo(node.Key);

        if (compare < 0)
        {
            node.Left = Add(node.Left, key, value);
        }

        else if (compare > 0)
        {
            node.Right = Add(node.Right, key, value);
        }

        else
        {
            throw new ArgumentException($"키 [{node.Key}]가 존재합니다.");
        }

        UpdateHeight(node);
        return node;
    }

    protected virtual TreeNode<TKey, TValue> AddOrUpdate(TreeNode<TKey, TValue> node, TKey key, TValue value)
    {
        if (node == null)
            return new TreeNode<TKey, TValue>(key, value);

        int compare = key.CompareTo(node.Key);

        if (compare < 0)
        {
            node.Left = AddOrUpdate(node.Left, key, value);
        }

        else if (compare > 0)
        {
            node.Right = AddOrUpdate(node.Right, key, value);
        }

        else
        {
            node.Value = value;
        }

        UpdateHeight(node);
        return node;
    }

    public void Clear() => root = null;
   
    public bool Contains(KeyValuePair<TKey, TValue> item) => TryGetValue(item.Key, out _);
    public bool ContainsKey(TKey key) => TryGetValue(key, out _);
    
    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        foreach (var item in this)
        {
            array[arrayIndex++] = item;
        }
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return InOrderTraversal().GetEnumerator();
    }

    public virtual IEnumerable<KeyValuePair<TKey, TValue>> InOrderTraversal()
    {
        return InOrderTraversal(root);
    }

    protected virtual IEnumerable<KeyValuePair<TKey, TValue>> InOrderTraversal(TreeNode<TKey, TValue> node)
    {
        if (node != null)
        {
            foreach (var keyValuePair in InOrderTraversal(node.Left))
            {
                yield return keyValuePair;
            }

            yield return new KeyValuePair<TKey, TValue>(node.Key, node.Value);

            foreach (var keyValuePair in InOrderTraversal(node.Right))
            {
                yield return keyValuePair;
            }
        }
    }

    public virtual IEnumerable<KeyValuePair<TKey, TValue>> PreOrderTraversal()
    {
        return PreOrderTraversal(root);
    }

    protected virtual IEnumerable<KeyValuePair<TKey, TValue>> PreOrderTraversal(TreeNode<TKey, TValue> node)
    {
        if (node != null)
        {
            yield return new KeyValuePair<TKey, TValue>(node.Key, node.Value);

            foreach (var keyValuePair in PreOrderTraversal(node.Left))
            {
                yield return keyValuePair;
            }

            foreach (var keyValuePair in PreOrderTraversal(node.Right))
            {
                yield return keyValuePair;
            }
        }
    }

    public virtual IEnumerable<KeyValuePair<TKey, TValue>> PostOrderTraversal()
    {
        return PostOrderTraversal(root);
    }

    protected virtual IEnumerable<KeyValuePair<TKey, TValue>> PostOrderTraversal(TreeNode<TKey, TValue> node)
    {
        if (node != null)
        {
            foreach (var keyValuePair in PostOrderTraversal(node.Left))
            {
                yield return keyValuePair;
            }

            foreach (var keyValuePair in PostOrderTraversal(node.Right))
            {
                yield return keyValuePair;
            }

            yield return new KeyValuePair<TKey, TValue>(node.Key, node.Value);
        }
    }

    public virtual IEnumerable<KeyValuePair<TKey, TValue>> LevelOrderTraversal()
    {
        return LevelOrderTraversal(root);
    }

    protected virtual IEnumerable<KeyValuePair<TKey, TValue>> LevelOrderTraversal(TreeNode<TKey, TValue> node)
    {
        if (node == null) yield break;

        var queue = new Queue<TreeNode<TKey, TValue>>();
        queue.Enqueue(node);

        while (queue.Count > 0)
        {
            var currentNode = queue.Dequeue();
            yield return new KeyValuePair<TKey, TValue>(currentNode.Key, currentNode.Value);

            if (currentNode.Left != null) queue.Enqueue(currentNode.Left);
            if (currentNode.Right != null) queue.Enqueue(currentNode.Right);
        }
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        return Remove(item.Key);
    }

    public bool Remove(TKey key)
    {
        int initialCount = Count;
        root = Remove(root, key);
        return Count < initialCount; 
    }

    protected virtual TreeNode<TKey, TValue> Remove(TreeNode<TKey, TValue> node, TKey key)
    {
        if (node == null) return node;

        int compare = key.CompareTo(node.Key);

        if (compare < 0)
        {
            node.Left = Remove(node.Left, key);
        }
        else if (compare > 0)
        {
            node.Right = Remove(node.Right, key);
        }
        else
        {
            if (node.Left == null)
            {
                return node.Right;
            }
            else if (node.Right == null)
            {
                return node.Left;
            }

            TreeNode<TKey, TValue> minNode = FindMin(node.Right);
            node.Key = minNode.Key;
            node.Value = minNode.Value;

            node.Right = Remove(node.Right, minNode.Key);
        }

        UpdateHeight(node);
        return node;
    }

    protected virtual TreeNode<TKey, TValue> FindMin(TreeNode<TKey, TValue> node)
    {
        while (node.Left != null) node = node.Left;

        return node;
    }

    public bool TryGetValue(TKey key, out TValue value) => TryGetValue(root, key, out value);
    protected bool TryGetValue(TreeNode<TKey, TValue> node, TKey key, out TValue value)
    {
        if (node == null)
        {
            value = default;
            return false;
        }

        int compare = key.CompareTo(node.Key);    

        if (compare == 0)
        {
            value = node.Value;
            return true;
        }

        else if (compare < 0)
        {
            return TryGetValue(node.Left, key, out value);
        }

        else
        {
            return TryGetValue(node.Right, key, out value);
        }
    }

    protected virtual void UpdateHeight(TreeNode<TKey, TValue> node)
    {
        node.Height = Mathf.Max(Height(node.Left), Height(node.Right)) + 1;
    }

    protected virtual int Height(TreeNode<TKey, TValue> node)
    {
        return node == null ? 0 : node.Height;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}