using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPool : MonoBehaviour
{
    public static ShadowPool instance;

    private GameObject shadowPrefab;

    public int shadowCount;

    //����
    private Queue<GameObject> availableObjects = new Queue<GameObject>();

    void Awake()
    {
        instance = this;

        shadowPrefab = Resources.Load("Shadow") as GameObject;

        //��ʼ�������
        FillPool();
    }

    public void FillPool()
    {
        for (int i = 0; i < shadowCount; i++)
        {
            var newShadow = Instantiate(shadowPrefab);
            newShadow.transform.SetParent(transform);

            //ȡ�����ã����ض����
            ReturnPool(newShadow);
        }
    }

    public void ReturnPool(GameObject gameObject)
    {
        gameObject.SetActive(false);

        //Enqueue ��ӵ�����ĩβ
        availableObjects.Enqueue(gameObject);
    }

    public GameObject GetFromPool(bool IsRight)
    {
        if (availableObjects.Count == 0)
        {
            FillPool();
        }

        //Dequeue ������е�һ��
        var outShadow = availableObjects.Dequeue();
        outShadow.SetActive(true);
        //���ò�Ӱ���� 
        if (IsRight)
        {
            outShadow.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            outShadow.GetComponent<SpriteRenderer>().flipX = false;
        }

        return outShadow;
    }
}