using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D myCollider;

    [Header("Jump Settings")]
    public float jumpForce = 12f;
    public int maxJumpCount = 2;
    private int currentJumpCount;
    private bool isGrounded;

    [Header("Crouch & Fast Fall Settings")]
    public float gravityScaleMultiplier = 2f;
    public float fastFallForce = 25f;

    public float doubleTapTimeLimit = 0.25f;
    private float lastDownArrowPressTime;

    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();

        originalColliderSize = myCollider.size;
        originalColliderOffset = myCollider.offset;

        currentJumpCount = maxJumpCount;
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        HandleJump();
        HandleCrouchAndFastFall();
    }
    private void HandleJump()
    {
        bool jumpPressed = Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame;

        if (jumpPressed && currentJumpCount > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            currentJumpCount--;
            isGrounded = false;
        }
    }
    private void HandleCrouchAndFastFall()
    {
        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            float timeSinceLastPress = Time.time - lastDownArrowPressTime;

            if (timeSinceLastPress <= doubleTapTimeLimit)
            {
                if (!isGrounded)
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, -fastFallForce);
                }
            }

            lastDownArrowPressTime = Time.time;
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
            currentJumpCount = maxJumpCount;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
