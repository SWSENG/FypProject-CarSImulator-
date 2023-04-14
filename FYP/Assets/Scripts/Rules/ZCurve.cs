using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZCurve : MonoBehaviour
{
    private Transform targetPlayer;

    public float distance = 0;//distance between car and point
    public float Ab = 0;
    public GameObject ruleAudio;

    [Header("Point head")]
    public Transform point1;
    public float difference_point1 = 2f;

    [Header("Point park")]
    public Transform point2;
    public float difference_point2 = 2f;

    private float mpTime = 0f;
    private float midwayStop = 1f;
    public bool isStop;
    public bool detIsStop;

    public bool ParkingCount; //park or not 
    public GameObject AirWall;

    private float timer = 180f;
    public Text textTimer;

    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        isStop = true;
        detIsStop = true;
        ParkingCount = false;
    }

    enum Step
    {
        Default,
        First,
        Second
    }
    Step step = Step.First;

    // Update is called once per frame
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

        //Do the task step by step
        switch (step)
        {
            case Step.Default:
                Destroy(AirWall);
                break;
            case Step.First:
                Debug.DrawLine(targetPlayer.position, point1.position, Color.red);
                distance = Vector3.Distance(targetPlayer.position, point1.position);

                if (distance <= difference_point1)
                {
                    step = Step.Second;
                }
                break;
            case Step.Second:
                Debug.DrawLine(targetPlayer.position, point2.position, Color.red);
                distance = Vector3.Distance(targetPlayer.position, point2.position);

                if (distance <= difference_point2)
                {
                    step = Step.Default;
                }
                break;
        }

        if (isStop && detIsStop)
        {
            ruleAudio.GetComponent<RuleVoice>().Play_vehicleStopHalfWay();
            Score.DeductScore(1);
            detIsStop = false;
        }

        //if (ParkingCount == true)
        //{
        //    Destroy(AirWall);
        //    step = Step.Default;
        //}

        if (timer <= 0)
        {
            ruleAudio.GetComponent<RuleVoice>().Play_exeedTheTimeLimit();
            Score.DeductScore(30);
            timer = 0f;
            Destroy(gameObject);
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
