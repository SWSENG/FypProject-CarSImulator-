using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scope : MonoBehaviour
{
    private Transform targetPlayer;

    public float Ab = 0;
    private const float Ab_Max = 1.6f;
    private const float Ab_Min = 1.0f;
    public GameObject ruleAudio;

    [Header("Point park")]
    public Transform point1;

    private float mpTime = 0f;
    private float midwayStop = 1f;
    public bool isStop;
    public bool detIsStop;

    public bool ParkingCount; //park or not 
    public GameObject AirWall;
    public GameObject backWall;
    public GameObject particlePoint;

    private float timer = 180f;
    public Text textTimer;
    public Text Distance;

    public GameObject distanceColor;

    void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        isStop = true;
        detIsStop = true;
        ParkingCount = false;
        backWall.SetActive(false);
        particlePoint.SetActive(false);
    }

    enum Step
    {
        Default,
        Garage
    }
    Step step = Step.Garage;

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

        Debug.DrawLine(targetPlayer.position, point1.position, Color.red);

        Vector3 carbarnWord_a = point1.TransformPoint(Vector3.forward);
        Vector3 carbarnWord_b = targetPlayer.position - point1.position;
        carbarnWord_a = carbarnWord_a - point1.position;

        float Angle = Mathf.Acos(Vector3.Dot(carbarnWord_a.normalized, carbarnWord_b.normalized));
        float distance = Vector3.Distance(targetPlayer.position, point1.position);

        Ab = Mathf.Sin(Angle) * distance;
        Distance.gameObject.SetActive(true);
        particlePoint.SetActive(true);
        //Do the task step by step
        switch (step)
        {
            case Step.Default:
                Distance.gameObject.SetActive(false);
                particlePoint.SetActive(false);
                Destroy(AirWall);
                break;
            case Step.Garage:
                if (Score.isStop && Ab <= Ab_Max && Ab >= Ab_Min)
                {
                    Debug.Log("Pass");
                    ruleAudio.GetComponent<RuleVoice>().Play_stepOnLine();
                    backWall.SetActive(true);
                    step = Step.Default;
                }
                else if (Ab <= Ab_Max && Ab >= Ab_Min)
                {
                    detIsStop = false;
                }
                break;
        }

        if (isStop && detIsStop)
        {
            ruleAudio.GetComponent<RuleVoice>().Play_vehicleStopHalfWay();
            Score.DeductScore(30);
            detIsStop = false;
        }

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

        Distance.text = "Distance:" +  Ab.ToString("F2");

        if (Ab >= 1.0 && Ab <= 1.6)
        {
            distanceColor.GetComponentInChildren<Text>().color = Color.green;
        }
        else
        {
            distanceColor.GetComponentInChildren<Text>().color = Color.red;
        }
    }
    private void OnDestroy()
    {
        textTimer.text = "Time: --";
    }

}
