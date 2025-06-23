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
    private GameManager gameManager;

    private Animator animator;
    private bool isJumping;
    private bool isGrounded;
    public int maxHealth = 5;
    public int currentHeal;
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
        UploadAnimation();
        JumpHandle();
        GravityHandle();
        handleHealth();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageDealer damageDealer = collision.gameObject.GetComponent<IDamageDealer>();
        if (damageDealer != null)
        {
            int damage = damageDealer.GetDamageAmount();
            TakeDamage(damage);
        }
    }

    private void handleHealth()
    {
        if(Input.GetKeyDown(KeyCode.F4))
        {
            TakeDamage(1);
        }

    }



    private void TakeDamage(int damage)
    {
        currentHeal -= damage;
        HealBarScript.SetHealth(currentHeal);
        if (currentHeal <= 0)
        {
            Die();
        }
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
    
    private void UploadAnimation()
    {
        bool isRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        animator.SetBool("isMoving", isRunning);
    }
    void Die()
    {
        Debug.Log("Player Died!");
        gameManager.GameOver();
    }


}
