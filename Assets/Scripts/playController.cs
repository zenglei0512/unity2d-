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
    rightJump           //右跳跃状态
}

public class playController : MonoBehaviour
{
    float h = 0, v = 0, jumpTime = 0.0f, jumpTimedt = 0.3f;
    int jumpOnce = 0;
    public float speed = 5f, jumpValue = 10f;
    public LayerMask ground;
    public BoxCollider2D boxCollider2d;
    public bool isGameOver = false;
    string runName = "rightRun";
    Animator animator;
    Rigidbody2D rigidbody2D;
    State curState = State.rightIdle;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    playerRun();
    //}

    private void FixedUpdate()
    {
        if (isGameOver)
        {
            Time.timeScale = 0;
            rigidbody2D.velocity = new Vector2(0, 0);
            rigidbody2D.gravityScale = 0;
            return ;
        }
        playerRun();
        playerJump();
    }

    public void playerRun()
    {
        h = Input.GetAxisRaw("Horizontal"); //控制左右移动
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
        transform.Translate(h * Time.deltaTime * speed, 0, 0);
    }

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

    public void playerJump()
    {
        Debug.Log("jumpOnce: " + jumpOnce);
        jumpTime += Time.deltaTime;
        if (jumpOnce == 0)
        {
            if (boxCollider2d.IsTouchingLayers(ground))
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    jumpOnce = 1;
                    jumpTime = 0.0f;
                    rigidbody2D.velocity = Vector2.up * jumpValue;
                    playerJumpAnim();
                }
            }
            else if (rigidbody2D.velocity.y <= 0)
            {
                jumpOnce = 1;
            }
        }
        else if (jumpOnce == 1)
        {
            if (Input.GetKey(KeyCode.Space) && (jumpTime > jumpTimedt))
            {
                jumpOnce = 2;
                if (rigidbody2D.velocity.y > 0)
                {
                    rigidbody2D.velocity += Vector2.up * jumpValue;
                }
                else
                {
                    rigidbody2D.velocity = Vector2.up * jumpValue;
                }
                playerJumpAnim();
            }
        }
        if (boxCollider2d.IsTouchingLayers(ground))// && rigidbody2D.velocity.y == 0)
        {
            jumpOnce = 0;
            animator.SetBool("leftJump", false);
            animator.SetBool("rightJump", false);
        }
        if (rigidbody2D.velocity.y < 0)
        {
            animator.SetBool("leftJump", false);
            animator.SetBool("rightJump", false);
            rigidbody2D.velocity += Vector2.up * speed * Time.deltaTime;
        }
        else if (rigidbody2D.velocity.y > 0)
        {
            rigidbody2D.velocity += Vector2.up * (speed + 1) * Time.deltaTime;
        }
    }
}
