using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level3 : MonoBehaviour
{
    public void OnButton1Click()
    {
        SceneManager.LoadScene("menu/menu");
    }
}

