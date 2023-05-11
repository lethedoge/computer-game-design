using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSprite : MonoBehaviour
{
    private Transform player;

    private SpriteRenderer thisSprite;
    private SpriteRenderer playerSprite;

    private Color color;

    [Header("时间控制参数")]
    public float activeTime;    //显示时间
    public float activeStart;   //开始显示的时间点

    [Header("不透明度控制")]
    public float alphaOriginal; //透明度初始值
    public float alphaMultiplier;   //渐变乘积
    private float alpha;

    private void OnEnable()
    {
        //找到角色
        player = GameObject.FindGameObjectWithTag("Frog").transform;
        //获取自身的SpriteRenderer
        thisSprite = GetComponent<SpriteRenderer>();
        //获取角色的SpriteRenderer
        playerSprite = player.GetComponent<SpriteRenderer>();

        //设置残影的初始alpha值
        alpha = alphaOriginal;
        //将角色当前帧的图片设置为残影图片
        thisSprite.sprite = playerSprite.sprite;
        //将残影的transform信息设置为角色的transform信息
        transform.position = player.position;
        transform.localScale = player.localScale;
        transform.rotation = player.rotation;

        activeStart = Time.time;    //当前时间
    }

    void Update()
    {
        //每帧设置残影的Alpha值，alphaMultiplier值越小，残影消失的越快
        alpha *= alphaMultiplier;

        color = new Color(0.5f, 0.5f, 1, alpha);  //残影的颜色，默认为蓝色

        thisSprite.color = color;

        //当时间超过显示时间
        if (Time.time >= activeStart + activeTime)
        {
            //返回对象池
            ShadowPool.instance.ReturnPool(this.gameObject);
        }
    }
}