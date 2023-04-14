using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject camera1;
    public GameObject camera2;

    AudioListener cameraOneAudioLis;
    AudioListener cameraTwoAudioLis;

    void Start()
    {
        cameraOneAudioLis = camera1.GetComponent<AudioListener>();
        cameraTwoAudioLis = camera2.GetComponent<AudioListener>();

        cameraPositionChange(PlayerPrefs.GetInt("CameraPosition"));
    }

    void FixedUpdate()
    {
        switchCamera();
    }

    public void cameraPositionM()
    {
        cameraChangeCounter();
    }

    void cameraChangeCounter()
    {
        int cameraPositionCounter = PlayerPrefs.GetInt("CameraPosition");

        cameraPositionCounter++;
        cameraPositionChange(cameraPositionCounter);
    }

    void cameraPositionChange(int camPosition)
    {
        if (camPosition > 1)
        {
            camPosition = 0;
        }

        PlayerPrefs.SetInt("CameraPosition", camPosition);

        if(camPosition == 0)
        {
            camera1.SetActive(true);
            cameraOneAudioLis.enabled = true;

            camera2.SetActive(false);
            cameraTwoAudioLis.enabled = false;
        }

        if (camPosition == 1)
        {
            camera2.SetActive(true);
            cameraTwoAudioLis.enabled = true;

            camera1.SetActive(false);
            cameraOneAudioLis.enabled = false;
        }
    }

    void switchCamera()
    {
        if (Input.GetKeyDown(KeyCode.C) || LogitechGSDK.LogiButtonTriggered(0, 3))
        {
            cameraChangeCounter();
            Debug.Log("test");
        }
    }

}
