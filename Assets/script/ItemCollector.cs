using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    //public TextMeshProUGUI myText;
    public GameObject key, dialogue;
    public TextMeshProUGUI collectionText;

    public int numObjects = 0;
    public bool hasAllObjects = false;
    public bool appearKey = false;
    public bool hasKey = false;
    public string scene;

    // Start is called before the first frame update
    private void Start()
    {
        key.SetActive(false);
        dialogue.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Apple")
        {
            numObjects++;
            Destroy(other.gameObject);
            collectionText.text = ":" + numObjects.ToString();
            if (numObjects >= 3)
            {
                hasAllObjects = true;
            }
        }
        else if (other.tag == "Witch" && hasAllObjects)
        {
            appearKey = true;
            //myText.text = "Hi";
            Destroy(other.gameObject);
            dialogue.SetActive(true);
            key.SetActive(true);
        }

        else if (other.tag == "Key" && appearKey)
        {
            Destroy(other.gameObject);
            hasKey = true;
            // Level complete!
        }
        if (other.tag == "Door" && hasKey)
        {
            SceneManager.LoadScene(scene);
        }
    }
}