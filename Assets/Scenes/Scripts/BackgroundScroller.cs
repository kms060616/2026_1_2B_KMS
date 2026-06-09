using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 5f;
    public float backgroundWidth = 20f;

    void Update()
    {
        transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);
        if (transform.position.x <= -backgroundWidth)
        {
            RepositionBackground();
        }
    }

    void RepositionBackground()
    {
        Vector2 offset = new Vector2(backgroundWidth * 2f, 0);
        transform.position = (Vector2)transform.position + offset;
    }
}
