using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playSE : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        // Get the AudioSource component attached to the same GameObject as this script
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundEffect()
    {
        // Play the sound effect
        audioSource.Play();
    }
}