using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Playerinfo playerInfo;

    public float moveSpeed = 100;
    public float jumpSpeed = 100;
    public float dashSpeed = 500;

    private float currentSpeed;
    
    private Rigidbody2D rb;
    
    //private bool facingRight = true;
    
    private bool isGrounded = true;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    public int extraJumps;
    private int currExtraJumps;

    private float direction;
    private Vector2 noRotationVector = new Vector2(0, 0);

    private float dashLenght = 0.1f;
    private float dashCooldown = 0.6f;
    private float dashLenCounter;
    private float dashCDCounter;

    public SpriteRenderer sprite;

    public PlayerAni animationControl;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = moveSpeed;

        if(playerInfo.playerstats.SPDtalents >= 3)
        {
            dashCooldown = 0.48f;
        }

        moveSpeed += (int)(0.1 * playerInfo.playerstats.SPD);
        if (playerInfo.playerstats.SPDtalents >= 2)
        {
            moveSpeed += (int)(0.05 * playerInfo.playerstats.SPD);
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if(playerInfo.CanAct == true)
        {
            direction = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(direction * currentSpeed, rb.velocity.y);

            if(!animationControl.currState.Equals("jump") && !animationControl.currState.Equals("attack") && !animationControl.currState.Equals("dash") && !animationControl.currState.Equals("ulti"))
            {
                if (direction < 0 && isGrounded)
                {
                    animationControl.SetCharacterState("run");
                }
                else if (direction > 0 && isGrounded)
                {
                    animationControl.SetCharacterState("run");
                }
                else
                {
                    animationControl.SetCharacterState("idle");
                }
            }
            if (direction < 0)
            {
                transform.localScale = new Vector2(-0.7f, 0.7f);
            }
            else if (direction > 0)
            {
                transform.localScale = new Vector2(0.7f, 0.7f);
            }
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
    
    void Update()
    {
        transform.eulerAngles = noRotationVector;

        if (isGrounded == true)
        {
            currExtraJumps = extraJumps;
        }

        if(Input.GetKeyDown(KeyCode.Space) && currExtraJumps > 0 && playerInfo.CanAct == true)
        {
            rb.velocity = Vector2.up * jumpSpeed;
            currExtraJumps--;
            animationControl.SetCharacterState("jump");
        }
        else if (Input.GetKeyDown(KeyCode.Space) && currExtraJumps == 0 && isGrounded == true && playerInfo.CanAct == true)
        {
            rb.velocity = Vector2.up * jumpSpeed;
            animationControl.SetCharacterState("jump");
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && playerInfo.CanAct == true)
        {
            if (dashLenCounter <= 0 && dashCDCounter <= 0)
            {
                currentSpeed = dashSpeed;
                dashLenCounter = dashLenght;
                animationControl.SetCharacterState("dash");

            }
        }

        if (dashLenCounter > 0)
        {
            dashLenCounter -= Time.deltaTime;
            if (dashLenCounter <= 0)
            {
                currentSpeed = moveSpeed;
                dashCDCounter = dashCooldown;
            }
        }

        if (dashCDCounter > 0)
        {
            dashCDCounter -= Time.deltaTime;
        }
    }
}
