using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [Header("Text UI")]
    public Text textScore;
    [Header("Skill test score")]
    private static int score;


    private CarControl ObjSpeed;//get the current speed only
    public static float Speed = 0; //variable to store the car speed above

    public float time = 0f;
    public static bool isStop = true;//is the car stop
    public float IntervalSecond = 2f;//stop after 2 secs 

    void Start()
    {
        score = 30;
        ObjSpeed = FindObjectOfType<CarControl>();
    }

    public static void DeductScore(int dscore)
    {
        score -= dscore;
    }

    //public static void AddScore(int ascore)
    //{
    //    score += ascore;
    //}

    public static int GetScore()
    {
        return score;
    }

    private void Update()
    {
        if (textScore != null)
        {
            textScore.text = "Score :" + score;
        }

        Speed = ObjSpeed.CurrentSpeed;

        if (Speed == 0)
        {
            time += Time.deltaTime;
            if (time >= IntervalSecond)
            {
                isStop = true;
                time = 0;
            }
        }
        else
        {
            time = 0;
            isStop = false;
        }
    }


}
