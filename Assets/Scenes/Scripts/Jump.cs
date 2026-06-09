using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D myCollider;

    [Header("Movement Physics")]
    public float jumpForce = 12f;
    public float gravityScaleMultiplier = 2f;

    private bool isGrounded;
    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
        originalColliderSize = myCollider.size;
        originalColliderOffset = myCollider.offset;
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        bool jumpPressed = Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame;
        if (jumpPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(0, jumpForce);
            isGrounded = false;
        }

        if (Keyboard.current.downArrowKey.isPressed)
        {
            if (isGrounded)
            {
                myCollider.size = new Vector2(originalColliderSize.x, originalColliderSize.y * 0.5f);
                myCollider.offset = new Vector2(originalColliderOffset.x, originalColliderOffset.y - (originalColliderSize.y * 0.25f));
            }
            rb.gravityScale = gravityScaleMultiplier * 2f;
        }
        else
        {
            myCollider.size = originalColliderSize;
            myCollider.offset = originalColliderOffset;
            rb.gravityScale = gravityScaleMultiplier;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
