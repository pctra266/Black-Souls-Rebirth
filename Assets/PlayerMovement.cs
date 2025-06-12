using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravityScale = 5f;
    [SerializeField] private float jumpHoldGravityMultiplier = 0.5f;
    
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float radiusCheckGround = 0.5f;
    
    private bool isJumping;
    private bool isGrounded;
    //private float jumpTime;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;
    }

    void Update()
    {
        MovemnetHandle();
        JumpHandle();
        checkGround();
        GravityHandle();


    }
    public void checkGround()
    {
         isGrounded = Physics2D.OverlapCircle(groundCheck.position, radiusCheckGround, groundLayer);
    }


    private void MovemnetHandle()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y); 
        
    }

    private void JumpHandle()
    {
        bool isHoldJump = Input.GetButtonDown("Jump");

        if (isHoldJump && isGrounded)
        {
            isJumping = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        bool isReleaseJump = Input.GetButtonUp("Jump");
        if (isReleaseJump)
        {
            isJumping = false;
        }


    }

    public void GravityHandle()
    {
        
        if(rb.linearVelocity.y > 0)
        {
            if (isJumping)
            {
                rb.gravityScale = gravityScale * jumpHoldGravityMultiplier;
            }
            else
            {
                rb.gravityScale = gravityScale;
            }

        }
        else
        {
            rb.gravityScale = gravityScale;
        }
    }
    

}
