using UnityEngine;

public class NewMoveController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float flySpeed = 8f;
    [SerializeField] private float fallMultiplier = 5f;
    private float groundCheckRadius = 0.1f;
    private float jumpForce = 6f;
    private Vector2 vecGravity;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheckPoint;
    public Animator animator;
    public Joystick joystick;
    private Rigidbody2D rb2D;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
    }

    void Update()
    {
        float verticalInput = GetInput(joystick.Vertical, "Vertical");
        float horizontalInput = GetInput(joystick.Horizontal, "Horizontal");

        Flip(horizontalInput);
        Move(horizontalInput);

        if (verticalInput > 0.5f && IsGround())
        {
            Jump();
        }

        if (rb2D.velocity.y < 0)
        {
            // để tăng trọng lực -> cho nhân vật rơi nhanh hơn
            rb2D.velocity -= vecGravity * Time.deltaTime * fallMultiplier;
        }
    }

    float GetInput(float joystickValue, string axis)
    {
        float value = 0f;
        if (Mathf.Abs(joystickValue) > 0.01f)
        {
            value = joystickValue;
        }
        else if (Input.GetAxisRaw(axis) != 0)
        {
            value = Input.GetAxisRaw(axis);
        }
        return value;
    }

    private bool IsGround()
    {
        return Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
    }

    private void Move(float moveX)
    {
        if (Mathf.Abs(moveX) > 0)
        {
            SetAnimRun(true);
        }
        else
        {
            SetAnimRun(false);
        }
        float velocityX = moveX * moveSpeed;

        rb2D.velocity = new Vector2(velocityX, rb2D.velocity.y);
    }

    private void Jump()
    {
        SetAnimJump();
        rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
    }

    void Fly(float verticalInput, float horizontalInput)
    {
        AnimService.SetAnimRun(false);

        rb2D.velocity = new Vector2(horizontalInput * moveSpeed, verticalInput * flySpeed);
    }

    void FlyHorizontalOnly(float horizontalInput)
    {
        AnimService.SetAnimRun(false);

        rb2D.velocity = new Vector2(horizontalInput * moveSpeed, rb2D.velocity.y);
    }

    private void SetAnimRun(bool isRun)
    {
        if (animator == null)
        {
            Debug.Log("animator null");
            return;
        }

        animator.SetBool("isRunning", isRun);
    }

    private void SetAnimJump()
    {
        if (animator == null)
        {
            Debug.Log("animator null");
            return;
        }

        animator.SetTrigger("Attack2");
    }

    // do config anim lấy trên asset store có gọi hàm này nên tạo ra cho có
    public void ActivateAttackEffect2() { }

    void Flip(float inputHorizontal)
    {
        if (inputHorizontal == 0) return;

        int newScaleX;
        if (inputHorizontal < 0)
        {
            newScaleX = 1;
        }
        else
        {
            newScaleX = -1;
        }

        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * newScaleX, transform.localScale.y, transform.localScale.z);
    }
}