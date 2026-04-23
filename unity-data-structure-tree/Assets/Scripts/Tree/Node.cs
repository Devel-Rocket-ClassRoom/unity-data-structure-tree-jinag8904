using UnityEngine;

public class Node : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        Move(0, 0);
    }

    public void Move(int x, int y)
    {
        transform.position = new Vector3(x, y, 0);
        spriteRenderer.enabled = true;
    }
}
