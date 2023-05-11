using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{
    public GameObject frog, princess;
    private Rigidbody2D frogPlayer, princessPlayer;
    private SpriteRenderer frogSprite, princessSprite;
    private Animator frogAnim, princessAnim;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public float speed = 5f;
    public float jumpSpeed = 7f;
    private float direction = 0f;
    public float groundCheckRadius;
    private bool isTouchingGround;
    public int isPlayerNearby = 0;
    int whichAvatarIsOn = 1;

    public float duration = 7.0f;
    private float currentTime;

    public float dashSpeed = 15f; //dash速度
    public bool IsRight = true; //角色朝向状态，默认为朝向右侧
    public float dashTime;      //dash时长
    private float dashTimeSurplus;  //dash剩余时间
    private float dashLast = -10;     //上一次dash时间点
    public float dashCD; //dash冷却时间
    private bool isDashing;

    // Start is called before the first frame update
    private void Start()
    {
        frogPlayer = frog.GetComponent<Rigidbody2D>();
        princessPlayer = princess.GetComponent<Rigidbody2D>();
        frogSprite = frog.GetComponent<SpriteRenderer>();
        princessSprite = princess.GetComponent<SpriteRenderer>();
        frogAnim = frog.GetComponent<Animator>();
        princessAnim = princess.GetComponent<Animator>();

        frog.gameObject.SetActive(true);
        princess.gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {

        if (whichAvatarIsOn == 1)
        {
            Dash();

            //冲锋时不能调用移动跳跃
            if (isDashing)
            {
                return;
            }

            isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
            direction = Input.GetAxis("Horizontal");
            if (direction > 0f)
            {
                frogPlayer.velocity = new Vector2(direction * speed, frogPlayer.velocity.y);
                frogSprite.flipX = true;
                //设置角色朝向
                IsRight = true;
            }
            else if (direction < 0f)
            {
                frogPlayer.velocity = new Vector2(direction * speed, frogPlayer.velocity.y);
                frogSprite.flipX = false;
                IsRight = false;

            }
            else
            {
                frogPlayer.velocity = new Vector2(0, frogPlayer.velocity.y);
            }

            if (Input.GetButtonDown("Jump") && isTouchingGround)
            {
                frogPlayer.velocity = new Vector2(frogPlayer.velocity.x, jumpSpeed);
            }

            if (Input.GetButtonDown("Dash"))
            {
                //如果 当前时间 大于等于（上一次冲锋时间+冷却时间
                if (Time.time >= (dashLast + dashCD))
                {
                    ReadyToDash();
                }
            }

            princessPlayer.transform.position = frogPlayer.position;

            frogAnim.SetBool("OnGround", isTouchingGround);
            frogAnim.SetFloat("Speed", Mathf.Abs(frogPlayer.velocity.x));
            isPlayerNearby = FindObjectOfType<Transformation>().IsPlayerNearby();

        }
        else if (whichAvatarIsOn == 2)
        {
            direction = Input.GetAxis("Horizontal");
            if (direction > 0f)
            {
                princessPlayer.velocity = new Vector2(direction * speed, princessPlayer.velocity.y);
                princessSprite.flipX = false;

            }
            else if (direction < 0f)
            {
                princessPlayer.velocity = new Vector2(direction * speed, princessPlayer.velocity.y);
                princessSprite.flipX = true;

            }
            else
            {
                princessPlayer.velocity = new Vector2(0, princessPlayer.velocity.y);
            }
            frogPlayer.transform.position = princessPlayer.position;
            princessAnim.SetFloat("Speed", Mathf.Abs(princessPlayer.velocity.x));
            isPlayerNearby = FindObjectOfType<Princesstransform>().IsPlayerNearby();

            Debug.Log(currentTime);
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                TransformBack();
            }


        }
        if (Input.GetButtonDown("Transform") && isPlayerNearby == 1)
        {
            // processing whichAvatarIsOn variable
            switch (whichAvatarIsOn)
            {

                // if the first avatar is on
                case 1:

                    // then the second avatar is on now
                    whichAvatarIsOn = 2;

                    // disable the first one and anable the second one
                    frog.gameObject.SetActive(false);
                    princess.gameObject.SetActive(true);
                    currentTime = duration;
                    break;

                // if the second avatar is on
                case 2:

                    // then the first avatar is on now
                    whichAvatarIsOn = 1;

                    // disable the second one and anable the first one
                    frog.gameObject.SetActive(true);
                    princess.gameObject.SetActive(false);
                    break;
            }
        }
    }

    void ReadyToDash()
    {
        //将状态设置为正在冲刺
        isDashing = true;
        //重置冲锋时长
        dashTimeSurplus = dashTime;

        dashLast = Time.time;
    }
    void Dash()
    {
        if (isDashing)
        {
            if (dashTimeSurplus > 0)
            {
                //角色冲刺，冲刺朝向为IsRight所记录的最后朝向信息
                frogPlayer.velocity = new Vector2(IsRight ? dashSpeed : -dashSpeed, frogPlayer.velocity.y);
                
                dashTimeSurplus -= Time.deltaTime;
                //生成残影，并传入朝向信息
                ShadowPool.instance.GetFromPool(IsRight);
            }

            //冲刺时间结束 重置状态
            if (dashTimeSurplus <= 0)
            {
                isDashing = false;
            }
        }
    }

    void TransformBack()
    {
        whichAvatarIsOn = 1;
        frog.gameObject.SetActive(true);
        princess.gameObject.SetActive(false);
    }
}