using System;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravityScale = 5f;
    [SerializeField] private float jumpHoldGravityMultiplier = 0.5f;
    private Rigidbody2D rb;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    private Animator animator;
    private bool isJumping;
    private bool isGrounded;
    private bool isAttack;
    public int maxHealth = 5;
    public int currentHeal;
    private GameManager gameManager;
    public HealBarScript HealBarScript;


    //private AudioManager audioManager;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameManager = FindAnyObjectByType<GameManager>();
        
        //audioManager = FindAnyObjectByType<AudioManager>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        currentHeal = maxHealth;
        rb.gravityScale = gravityScale;
        HealBarScript.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        //if (gameManager.IsGameOver() || gameManager.IsGameWin())
        //{
            
        //    return;
        //}
        HandleMovement();
        CheckGround();
        CheckAttack();
        UploadAnimation();
        JumpHandle();
        GravityHandle();
        handleHealth();
    }

    private void handleHealth()
    {
        if(Input.GetKeyDown(KeyCode.F4))
        {
            Debug.Log("go to health controller");
            TakeDamage(1);
        }
    }

    private void TakeDamage(int damage)
    {
        currentHeal -= damage;
        HealBarScript.SetHealth(currentHeal);
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
        if (rb.linearVelocity.y > 0)
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




    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        if(moveInput > 0)
        {
            transform.localScale = new Vector3((float)-0.35, (float)0.35, 0);
        }
        if(moveInput < 0)
        {
            transform.localScale = new Vector3((float)0.35, (float)0.35, 0);

        }
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void CheckAttack()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            isAttack = true;
        }
        else
        {
            isAttack = false;
        }
    }
    private void UploadAnimation()
    {
        bool isRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        bool isAttacking = isAttack;
        animator.SetBool("isMoving", isRunning);
        animator.SetBool("attack", isAttacking);
    }


}
