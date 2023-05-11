using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopbgm : MonoBehaviour
{
    private void Start()
    {
        // Find the GameObject with the Audio Source component attached to it
        GameObject audioGameObject = GameObject.Find("bgmmenu");

        if (audioGameObject != null && audioGameObject.GetComponent<AudioSource>().isPlaying)
        {
            // If the GameObject is found and the audio source is playing, destroy it to stop the audio from playing
            Destroy(audioGameObject);
        }
    }
}
