using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed = 5f;
    private float leftBound = -15f;

    void Update()
    {
        float currentSpeed = speed * GameManager.Instance.GameSpeedMultiplier;

        transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);

        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }
}
