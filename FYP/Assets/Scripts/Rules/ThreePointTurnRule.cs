using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThreePointTurnRule : MonoBehaviour
{
    private Transform targetPlayer;
    public GameObject ruleAudio;
    public float distance = 0;//distance between car and point
    public float Ab = 0;

    [Header("Point head")] 
    public Transform point1;
    public float difference_point1 = 2f;

    [Header("Point park")]
    public Transform point2;
    public float difference_point2 = 3f;

    [Header("Point tail")]
    public Transform point3;
    public float difference_point3 = 2f;

    private float mpTime = 0f;
    private float midwayStop = 1f;
    public bool isStop;
    public bool detIsStop;

    public bool ParkingCount; //park or not 
    public GameObject AirWall;
    public GameObject particlePoint1;
    public GameObject particlePoint2;

    private float timer = 180f;
    public Text textTimer;

    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        isStop = true;
        detIsStop = true;
        ParkingCount = false;
        particlePoint1.SetActive(false);
        particlePoint2.SetActive(false);
    }

    enum Step
    {
        Default, 
        Right, 
        Left, 
        Garage
    }
    Step step = Step.Left;

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
                particlePoint1.SetActive(false);
                particlePoint2.SetActive(false);
                break;
            case Step.Left:
                Debug.DrawLine(targetPlayer.position, point1.position, Color.red);
                distance = Vector3.Distance(targetPlayer.position, point1.position);

                particlePoint1.SetActive(true);
                if (Score.isStop && distance <= difference_point1)
                {
                    particlePoint1.SetActive(false);
                    particlePoint2.SetActive(true);
                    ruleAudio.GetComponent<RuleVoice>().Play_startReverce();
                    step = Step.Garage;
                }
                else if (distance <= difference_point1)//if in the distance it will not deduct mark for suddently stop the car
                {
                    detIsStop = false;
                }

                break;
            case Step.Right:
                Debug.DrawLine(targetPlayer.position, point3.position, Color.red);
                distance = Vector3.Distance(targetPlayer.position, point3.position);

                //if (Score.isStop && distance <= difference_point3)
                //{
                //    step = Step.Default;
                //}
                //else if (distance <= difference_point3)//if in the distance it will not deduct mark for suddently stop the car
                //{
                //    detIsStop = false;
                //}
                break;
            case Step.Garage:
                Debug.DrawLine(targetPlayer.position, point2.position, Color.red);
                distance = Vector3.Distance(targetPlayer.position, point2.position);

                Vector3 carbarnWord_a = point2.TransformPoint(Vector3.right);
                Vector3 carbarnWord_b = targetPlayer.position - point2.position;
                carbarnWord_a = carbarnWord_a - point2.position;

                float Angle = Mathf.Acos(Vector3.Dot(carbarnWord_a.normalized, carbarnWord_b.normalized));

                Ab = Mathf.Cos(Angle) * distance;

                if (Score.isStop && Ab <= difference_point2)
                {
                    ruleAudio.GetComponent<RuleVoice>().Play_vehicleSuccessfullyParkInTheLot();
                    Destroy(AirWall);
                    step = Step.Default;
                }
                else if (distance <= difference_point2)//if in the distance it will not deduct mark for suddently stop the car
                {
                    detIsStop = false;
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
