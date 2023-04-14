using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCurve : MonoBehaviour
{
    private Transform targetPlayer;

    public GameObject ruleAudio;

    private float mpTime = 0f;
    private float midwayStop = 1f;
    public bool isStop;
    public bool detIsStop;

    private float timer = 180f;
    public Text textTimer;

    void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        isStop = true;
        detIsStop = true;
    }

    void Update()
    {
        if (Score.Speed == 0)
        {
            mpTime += Time.deltaTime;
            if (mpTime >= midwayStop)
            {
                isStop = true;
                mpTime = 0;
            }
        }
        else
        {
            mpTime = 0;
            isStop = false;
            detIsStop = true;
        }

        if (isStop && detIsStop)
        {
            ruleAudio.GetComponent<RuleVoice>().Play_vehicleStopHalfWay();
            Score.DeductScore(1);
            detIsStop = false;
        }

        if (timer <= 0)
        {
            ruleAudio.GetComponent<RuleVoice>().Play_exeedTheTimeLimit();
            Score.DeductScore(30);
            timer = 0f;
        }
        else
        {
            timer -= Time.deltaTime;
            textTimer.text = "Time:" + Mathf.Round(timer) + "s";
        }
    }

    private void OnDestroy()
    {
        textTimer.text = "Time: --";
    }
}
