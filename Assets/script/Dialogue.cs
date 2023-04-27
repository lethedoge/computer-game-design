using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GameObject dialogueBox;

    void Start()
    {

    }

    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Frog") ||collision.gameObject.CompareTag("Princess"))
        {
            dialogueBox.SetActive(true);
            Debug.Log("1");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Frog") ||collision.gameObject.CompareTag("Princess"))
        {
            dialogueBox.SetActive(false);
            Debug.Log("2");
        }
    }

}
