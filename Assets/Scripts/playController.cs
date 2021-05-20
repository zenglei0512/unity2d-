using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class playController : MonoBehaviour
{
    float h = 0;
    float CapsuleSizeX;
    public float speed = 5f, jumpSpeed = 10f;
    int jumpOnce = 0;
    public LayerMask ground;
    public bool isGameOver = false;
    bool isGround;
    Animator animator;
    BoxCollider2D playerFeet;
    CapsuleCollider2D Capsule;
    Rigidbody2D playerBody;
    gameController gameController;
    saveMapData saveMapData;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Capsule = GetComponent<CapsuleCollider2D>();
        playerFeet = GetComponent<BoxCollider2D>();
        playerBody = GetComponent<Rigidbody2D>();
        gameController = GameObject.Find("Canvas").GetComponent<gameController>();
        CapsuleSizeX = Capsule.size.x;
        saveMapData = gameObject.AddComponent<saveMapData>();
        saveMapData.saveCurrentMapIndex();
    }

    // Update is called once per frame
    void Update()
    {
        cheakGround();
        playerFlip();
        setPlayerCapsuleSize();
    }

    private void FixedUpdate()
    {
        if (isGameOver)
        {
            playerBody.velocity = new Vector2(0, playerBody.velocity.y);
            return ;
        }
        playerRun();
        playerJump();
        switchPlayerAnim();
    }

    public void playerRun()
    {
        h = Input.GetAxisRaw("Horizontal"); //控制左右移动
        Vector2 runVector = new Vector2(h * speed, playerBody.velocity.y);
        playerBody.velocity = runVector;
        //playerBody.velocity = Vector2.left * h * Time.deltaTime * speed;
        //transform.Translate(h * Time.deltaTime * speed, 0, 0);
        bool isRun = Mathf.Abs(playerBody.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isRun", isRun);
    }

    //翻转人物
    void playerFlip()
    {
        float playerSpeedX = h;
        if (playerSpeedX > Mathf.Epsilon)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        } else if (Mathf.Abs(playerSpeedX) > Mathf.Epsilon)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    //检查人物脚是否接触到地面
    void cheakGround()
    {
        isGround = playerFeet.IsTouchingLayers(LayerMask.GetMask("ground"));
    }

    //设置人物胶囊碰撞体大小
    void setPlayerCapsuleSize()
    {
        float SpriteRendererSizeY = transform.GetComponent<SpriteRenderer>().bounds.size.y;
        if (CapsuleSizeX < SpriteRendererSizeY)
        {
            Capsule.size = new Vector2(CapsuleSizeX, SpriteRendererSizeY);
        } else
        {
            Capsule.size = new Vector2(SpriteRendererSizeY, SpriteRendererSizeY);
        }
    }

    void switchPlayerAnim()
    {
        animator.SetBool("isIdle", false);
        bool isJump = animator.GetBool("isJump");
        if (isJump)
        {
            if (playerBody.velocity.y < 0)
            {
                animator.SetBool("isJump", false);
                animator.SetBool("isFall", true);
            }
        } else if (isGround)
        {
            animator.SetBool("isFall", false);
            animator.SetBool("isIdle", true);
        }
    }

    public void playerJump()
    {
        if (isGround)
        {
            jumpOnce = 0;
        } else if (playerBody.velocity.y <= 0 && jumpOnce == 0)
        {
            jumpOnce = 1;
        }
        //if (Input.GetButtonDown("Jump"))
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.J))
        {
            if (isGround)
            {
                animator.SetBool("isJump", true);
                jumpOnce = 1;
                Vector2 jumpVector = new Vector2(playerBody.velocity.x, jumpSpeed);
                playerBody.velocity = jumpVector;
            } else
            {
                if (jumpOnce == 1)// && (jumpTime > jumpTimedt))
                {
                    animator.SetBool("isJump", true);
                    animator.SetBool("isFall", false);
                    jumpOnce = 2;
                    Vector2 jumpVector = new Vector2(playerBody.velocity.x, jumpSpeed);
                    playerBody.velocity = jumpVector;
                }
            }
        }
    }

    public void playerDeath()
    {
        gameController.resetGame();
    }
}
