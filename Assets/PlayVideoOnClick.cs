using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayVideoOnClick : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Button playButton;

    void Start()
    {
        playButton.onClick.AddListener(PlayVideo);
    }

    void PlayVideo()
    {
        videoPlayer.Play();
    }
}
