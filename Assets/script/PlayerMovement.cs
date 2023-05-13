using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public GameObject frog, princess, textTimer;
    private Rigidbody2D frogPlayer, princessPlayer;
    private SpriteRenderer frogSprite, princessSprite;
    private Animator frogAnim, princessAnim;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public TextMeshProUGUI collectionText;

    public float speed = 5f;
    public float jumpSpeed = 7f;
    private float direction = 0f;
    public float groundCheckRadius;
    private bool isTouchingGround;
    public int isPlayerNearby = 0;
    int whichAvatarIsOn = 1;

    public float duration = 7.0f;
    private float currentTime;

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
            isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
            direction = Input.GetAxis("Horizontal");
            if (direction > 0f)
            {
                frogPlayer.velocity = new Vector2(direction * speed, frogPlayer.velocity.y);
                frogSprite.flipX = true;

            }
            else if (direction < 0f)
            {
                frogPlayer.velocity = new Vector2(direction * speed, frogPlayer.velocity.y);
                frogSprite.flipX = false;

            }
            else
            {
                frogPlayer.velocity = new Vector2(0, frogPlayer.velocity.y);
            }

            if (Input.GetButtonDown("Jump") && isTouchingGround)
            {
                frogPlayer.velocity = new Vector2(frogPlayer.velocity.x, jumpSpeed);
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
            textTimer.SetActive(true);
            collectionText.text = (Math.Round(currentTime, 1)).ToString();
            if (currentTime <= 0) 
            {            
                textTimer.SetActive(false);
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
                    textTimer.SetActive(false);
                    break;
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