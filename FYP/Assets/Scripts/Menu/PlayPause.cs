using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayPause : MonoBehaviour
{
    public VideoPlayer video;
    public Button button;
    public Sprite startSprite;
    public Sprite stopSprite;

    // Start is called before the first frame update
    void Start()
    {
        video = GetComponent<VideoPlayer>();
        video.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeStartStop()
    {
        if (video.isPlaying == false)
        {
            video.Play();
            button.image.sprite = stopSprite;
        }
        else
        {
            video.Pause();
            button.image.sprite = startSprite;
        }
    }
}
