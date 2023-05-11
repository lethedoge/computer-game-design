using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lvl1tohmpg : MonoBehaviour
{
    public void tohmpg()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -3);
    }
    // Start is called before the first frame update

}


