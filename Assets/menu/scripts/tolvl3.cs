using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tolvl3 : MonoBehaviour
{
    public void tolevel3()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
    }
    // Start is called before the first frame update

}
