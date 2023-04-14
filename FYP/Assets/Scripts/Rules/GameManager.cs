using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gamePassFail;
    public bool GamePlayingFlag = true;
    bool first = true;

    private void Start()
    {

    }

    private void Update()
    {
        if (Score.GetScore() < 24 && GamePlayingFlag) 
        {
            Time.timeScale = 0;
            GamePlayingFlag = false;
            gamePassFail.SetActive(!gamePassFail.activeSelf);
            gamePassFail.GetComponentInChildren<Text>().text = "Exam End, Result Fail";
            gamePassFail.GetComponentInChildren<Text>().color = Color.red;
            FindObjectOfType<RuleVoice>().Play_examFail();
        }
        else if (Score.GetScore() >= 24 && !GamePlayingFlag && first)
        {
            first = false;
            Time.timeScale = 0;
            gamePassFail.SetActive(!gamePassFail.activeSelf);
            gamePassFail.GetComponentInChildren<Text>().text = "Exam End, Result Pass";
            gamePassFail.GetComponentInChildren<Text>().color = Color.green;
            FindObjectOfType<RuleVoice>().Play_examPass();
        }
    }
}
