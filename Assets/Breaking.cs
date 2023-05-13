using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breaking : MonoBehaviour
{
    private Animator frogAnim;
    private bool breaking;
    // Start is called before the first frame update
    void Start()
    {
        frogAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Break"))
            {
                frogAnim.SetBool("Break", true);
            } 
        else 
            {
                frogAnim.SetBool("Break", false);
            }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Breaking" && frogAnim.GetBool("Break") == true)
        {
            Destroy(other.gameObject);
        }
    }
}
