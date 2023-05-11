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

    public float dashSpeed = 15f; //dash�ٶ�
    public bool IsRight = true; //��ɫ����״̬��Ĭ��Ϊ�����Ҳ�
    public float dashTime;      //dashʱ��
    private float dashTimeSurplus;  //dashʣ��ʱ��
    private float dashLast = -10;     //��һ��dashʱ���
    public float dashCD; //dash��ȴʱ��
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

            //���ʱ���ܵ����ƶ���Ծ
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
                //���ý�ɫ����
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
                //��� ��ǰʱ�� ���ڵ��ڣ���һ�γ��ʱ��+��ȴʱ��
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
        //��״̬����Ϊ���ڳ��
        isDashing = true;
        //���ó��ʱ��
        dashTimeSurplus = dashTime;

        dashLast = Time.time;
    }
    void Dash()
    {
        if (isDashing)
        {
            if (dashTimeSurplus > 0)
            {
                //��ɫ��̣���̳���ΪIsRight����¼���������Ϣ
                frogPlayer.velocity = new Vector2(IsRight ? dashSpeed : -dashSpeed, frogPlayer.velocity.y);
                
                dashTimeSurplus -= Time.deltaTime;
                //���ɲ�Ӱ�������볯����Ϣ
                ShadowPool.instance.GetFromPool(IsRight);
            }

            //���ʱ����� ����״̬
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