using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleVoice : MonoBehaviour
{
    public new AudioSource audio;

    [Header("All Audio")]
    public AudioClip nextExamSideParking;
    public AudioClip nextExamSCurve;
    public AudioClip examPass;
    public AudioClip examFail;
    public AudioClip touchingTheLine;
    public AudioClip exeedTheTimeLimit;
    public AudioClip failToStopWithFrontWheelOnTheLine;
    public AudioClip vehicleSuccessfullyParkInTheLot;
    public AudioClip vehicleStopHalfWay;
    public AudioClip engineStart;
    public AudioClip engineOff;
    public AudioClip startReverce;
    public AudioClip nextExamHill;
    public AudioClip nextExamZCurve;
    public AudioClip nextExamThreePointTurn;
    public AudioClip examHasStarted;
    public AudioClip stepOnLine;
    public AudioClip vehicleMoveBackwardFail;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        StartCoroutine(Setout());
    }

    public void Play_nextExamSideParking()
    {
        audio.clip = nextExamSideParking;
        audio.loop = false;
        audio.volume = 1.0f;
        audio.Play();
    }
    public void Play_nextExamSCurve()
    {
        audio.clip = nextExamSCurve;
        audio.loop = false;
        audio.volume = 1.0f;
        audio.Play();
    }
    public void Play_examPass()
    {
        audio.clip = examPass;
        audio.loop = false;
        audio.volume = 1.0f;
        audio.Play();
    }
    public void Play_examFail()
    {
        audio.clip = examFail;
        audio.loop = false;
        audio.volume = 1.0f;
        audio.Play();
    }
    public void Play_touchingTheLine()
    {
        audio.clip = touchingTheLine;
        audio.loop = false;
        audio.volume = 1.0f;
        audio.Play();
    }
    public void Play_exeedTheTimeLimit()
    {
        audio.clip = exeedTheTimeLimit;
        audio.loop = false;
        audio.volume = 1.0f;
        audio.Play();
    }
    public void Play_failToStopWithFrontWheelOnTheLine()
    {
        audio.clip = failToStopWithFrontWheelOnTheLine;
        audio.loop = false;
        audio.volume = 1.0f;
        audio.Play();
    }
    public void Play_vehicleSuccessfullyParkInTheLot()
    {
        audio.clip = vehicleSuccessfullyParkInTheLot;
        audio.loop = false;
        audio.volume = 1.0f;
        audio.Play();
    }
    public void Play_vehicleStopHalfWay()
    {
        audio.clip = vehicleStopHalfWay;
        audio.loop = false;
        audio.volume = 1.0f;
        audio.Play();
    }
    public void Play_engineStart()
    {
        audio.clip = engineStart;
        audio.loop = false;
        audio.volume = 1.0f;
        audio.Play();
    }
    public void Play_engineOff()
    {
        audio.clip = engineOff;
        audio.loop = false;
        audio.volume = 1.0f;
        audio.Play();
    }
    public void Play_startReverce()
    {
        audio.clip = startReverce;
        audio.loop = false;
        audio.volume = 1.0f;
        audio.Play();
    }
    public void Play_nextExamHill()
    {
        audio.clip = nextExamHill;
        audio.loop = false;
        audio.volume = 1.0f;
        audio.Play();
    }
    public void Play_nextExamZCurve()
    {
        audio.clip = nextExamZCurve;
        audio.loop = false;
        audio.volume = 1.0f;
        audio.Play();
    }
    public void Play_nextExamThreePointTurn()
    {
        audio.clip = nextExamThreePointTurn;
        audio.loop = false;
        audio.volume = 1.0f;
        audio.Play();
    }

    public void Play_examHasStarted()
    {
        audio.clip = examHasStarted;
        audio.loop = false;
        audio.volume = 1.0f;
        audio.Play();
    }

    public void Play_stepOnLine()
    {
        audio.clip = stepOnLine;
        audio.loop = false;
        audio.volume = 1.0f;
        audio.Play();
    }

    public void Play_vehicleMoveBackwardFail()
    {
        audio.clip = vehicleMoveBackwardFail;
        audio.loop = false;
        audio.volume = 1.0f;
        audio.Play();
    }

    IEnumerator Setout()
    {
        yield return new WaitForSeconds(0.1f);
        Play_examHasStarted();
        yield return new WaitForSeconds(1.64f);
        StopAllCoroutines();
    }
}
