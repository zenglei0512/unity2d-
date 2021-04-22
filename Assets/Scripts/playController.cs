using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum State
{
    leftIdle,           //左闲置状态
    rightIdle,          //右闲置状态
    leftRun,            //左跑状态
    rightRun,           //右跑状态
    leftJump,           //左跳跃状态
    rightJump,          //右跳跃状态
    Idle,               //闲置状态
    Run,                //跑状态
    Jump,               //跳跃状态
    Fall,               //下落状态
}

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
    State curState = State.Idle;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Capsule = GetComponent<CapsuleCollider2D>();
        playerFeet = GetComponent<BoxCollider2D>();
        playerBody = GetComponent<Rigidbody2D>();
        gameController = GameObject.Find("Canvas").GetComponent<gameController>();
        CapsuleSizeX = Capsule.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        //if (isGameOver)
        //{
        //    return;
        //}
        cheakGround();
        playerFlip();
        setPlayerCapsuleSize();
    }

    private void FixedUpdate()
    {
        if (isGameOver)
        {
            //Time.timeScale = 0;
            playerBody.velocity = new Vector2(0, playerBody.velocity.y);
            //playerBody.gravityScale = 0;
            return ;
        }
        //cheakGround();
        //playerFlip();
        playerRun();
        playerJump();
        switchPlayerAnim();
    }

    public void playerRun()
    {
        /*h = Input.GetAxisRaw("Horizontal"); //控制左右移动
        //v = Input.GetAxisRaw("Vertical");   //控制上下移动

        if (h == 0)
        {
            if (curState == State.leftRun || curState == State.leftIdle)
            {
                playerLeftIdle();
            }
            else if (curState == State.rightRun || curState == State.rightIdle)
            {
                playerRightIdle();
            }
        }
        else if (h > 0)
        {
            if (curState == State.leftIdle || curState == State.leftRun || curState == State.leftJump)
            {
                animator.SetBool("leftJump", false);
                animator.SetFloat("leftRun", 0);
            }
            playerRightRun();
        }
        else
        {
            if (curState == State.rightIdle || curState == State.rightRun || curState == State.rightJump)
            {
                animator.SetBool("rightJump", false);
                animator.SetFloat("rightRun", 0);
            }
            playerLeftRun();
        }
        animator.SetFloat(runName, h);
        transform.Translate(h * Time.deltaTime * speed, 0, 0);*/

        h = Input.GetAxisRaw("Horizontal"); //控制左右移动
        Vector2 runVector = new Vector2(h * speed, playerBody.velocity.y);
        playerBody.velocity = runVector;
        //playerBody.velocity = Vector2.left * h * Time.deltaTime * speed;
        //transform.Translate(h * Time.deltaTime * speed, 0, 0);
        bool isRun = Mathf.Abs(playerBody.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isRun", isRun);
        if (isRun)
        {
            curState = State.Run;
        }
    }

    /*
    public void playerLeftRun()
    {
        Debug.Log("向左移动中！");
        curState = State.leftRun;
        runName = "leftRun";
    }

    public void playerRightRun()
    {
        Debug.Log("向右移动中！");
        curState = State.rightRun;
        runName = "rightRun";
    }

    public void playerLeftIdle()
    {
        Debug.Log("左闲置中！");
        curState = State.leftIdle;
        runName = "leftRun";
    }

    public void playerRightIdle()
    {
        Debug.Log("右闲置中！");
        curState = State.rightIdle;
        runName = "rightRun";
    }

    public void playerJumpAnim()
    {
        if (curState == State.leftIdle || curState == State.leftRun)
        {
            animator.SetBool("rightJump", false);
            animator.SetBool("leftJump", true);
            curState = State.leftJump;
        }
        else if (curState == State.rightIdle || curState == State.rightRun)
        {
            animator.SetBool("leftJump", false);
            animator.SetBool("rightJump", true);
            curState = State.rightJump;
        }
    }
    */

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
        /*Debug.Log("jumpOnce: " + jumpOnce);
        jumpTime += Time.deltaTime;
        if (jumpOnce == 0)
        {
            if (playerFeet.IsTouchingLayers(ground))
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    jumpOnce = 1;
                    jumpTime = 0.0f;
                    playerBody.velocity = Vector2.up * jumpValue;
                    playerJumpAnim();
                }
            }
            else if (playerBody.velocity.y <= 0)
            {
                jumpOnce = 1;
            }
        }
        else if (jumpOnce == 1)
        {
            if (Input.GetKey(KeyCode.Space) && (jumpTime > jumpTimedt))
            {
                jumpOnce = 2;
                if (playerBody.velocity.y > 0)
                {
                    playerBody.velocity += Vector2.up * jumpValue;
                }
                else
                {
                    playerBody.velocity = Vector2.up * jumpValue;
                }
                playerJumpAnim();
            }
        }
        if (playerFeet.IsTouchingLayers(ground))// && playerBody.velocity.y == 0)
        {
            jumpOnce = 0;
            animator.SetBool("leftJump", false);
            animator.SetBool("rightJump", false);
        }
        if (playerBody.velocity.y < 0)
        {
            animator.SetBool("leftJump", false);
            animator.SetBool("rightJump", false);
            playerBody.velocity += Vector2.up * speed * Time.deltaTime;
        }
        else if (playerBody.velocity.y > 0)
        {
            playerBody.velocity += Vector2.up * (speed + 1) * Time.deltaTime;
        }*/

        if (isGround)
        {
            jumpOnce = 0;
        } else if (playerBody.velocity.y <= 0 && jumpOnce == 0)
        {
            jumpOnce = 1;
        }
        //if (Input.GetButtonDown("Jump"))
        if (Input.GetKeyDown(KeyCode.J))
            {
            if (isGround)
            {
                curState = State.Jump;
                animator.SetBool("isJump", true);
                jumpOnce = 1;
                Vector2 jumpVector = new Vector2(playerBody.velocity.x, jumpSpeed);
                playerBody.velocity = jumpVector;
            } else
            {
                if (jumpOnce == 1)// && (jumpTime > jumpTimedt))
                {
                    curState = State.Jump;
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
