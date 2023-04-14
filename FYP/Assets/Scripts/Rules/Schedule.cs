using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schedule : MonoBehaviour
{
    public GameObject nextExam;
    public AudioClip clip;
    new AudioSource audio;

    private void Start()
    {
        //close the next exam first
        if (nextExam != null)
        {
            nextExam.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (nextExam != null)
            {
                nextExam.SetActive(true);
            }
            audio = GetComponent<AudioSource>();
            audio.loop = false;
            audio.volume = 1.0f;
            audio.pitch = 1f;
            audio.clip = clip;
            audio.Play();
        }
    }
}
