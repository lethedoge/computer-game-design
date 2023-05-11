using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSprite : MonoBehaviour
{
    private Transform player;

    private SpriteRenderer thisSprite;
    private SpriteRenderer playerSprite;

    private Color color;

    [Header("ʱ����Ʋ���")]
    public float activeTime;    //��ʾʱ��
    public float activeStart;   //��ʼ��ʾ��ʱ���

    [Header("��͸���ȿ���")]
    public float alphaOriginal; //͸���ȳ�ʼֵ
    public float alphaMultiplier;   //����˻�
    private float alpha;

    private void OnEnable()
    {
        //�ҵ���ɫ
        player = GameObject.FindGameObjectWithTag("Frog").transform;
        //��ȡ�����SpriteRenderer
        thisSprite = GetComponent<SpriteRenderer>();
        //��ȡ��ɫ��SpriteRenderer
        playerSprite = player.GetComponent<SpriteRenderer>();

        //���ò�Ӱ�ĳ�ʼalphaֵ
        alpha = alphaOriginal;
        //����ɫ��ǰ֡��ͼƬ����Ϊ��ӰͼƬ
        thisSprite.sprite = playerSprite.sprite;
        //����Ӱ��transform��Ϣ����Ϊ��ɫ��transform��Ϣ
        transform.position = player.position;
        transform.localScale = player.localScale;
        transform.rotation = player.rotation;

        activeStart = Time.time;    //��ǰʱ��
    }

    void Update()
    {
        //ÿ֡���ò�Ӱ��Alphaֵ��alphaMultiplierֵԽС����Ӱ��ʧ��Խ��
        alpha *= alphaMultiplier;

        color = new Color(0.5f, 0.5f, 1, alpha);  //��Ӱ����ɫ��Ĭ��Ϊ��ɫ

        thisSprite.color = color;

        //��ʱ�䳬����ʾʱ��
        if (Time.time >= activeStart + activeTime)
        {
            //���ض����
            ShadowPool.instance.ReturnPool(this.gameObject);
        }
    }
}