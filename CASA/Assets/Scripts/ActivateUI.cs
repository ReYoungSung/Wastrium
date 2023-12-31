﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateUI : MonoBehaviour {

	//public bool activateUI = false;
    Vector3 velo = Vector3.zero;
    Vector3 velo2 = Vector3.zero;
    [SerializeField] GameObject image;
    [SerializeField] GameObject slider;
    [SerializeField] GameObject scoreText;

    [SerializeField] GameObject totalScore;
    [SerializeField] GameObject finalScore;
    [SerializeField] GameObject frame;
    [SerializeField] GameObject pressEnter;
    [SerializeField] GameObject positiveKText;
    [SerializeField] GameObject positiveEText;
    [SerializeField] GameObject negativeKText;
    [SerializeField] GameObject negativeEText;
    GenerateFallingTrash generateFalling;
    [SerializeField] int resetTime;

    public bool IsGaming = false;

    bool endScoreText = false;
    void Awake()
    {
        generateFalling = GetComponent<GenerateFallingTrash>();
    }
    public void UIDisabled()
    {
        image.SetActive(false);
        slider.SetActive(false);
        scoreText.SetActive(false);
    }    

    public void UIActivate()
    {
        IsGaming = true;
        image.SetActive(true);
        slider.SetActive(true);
        scoreText.SetActive(true);
        GetComponent<SliderTimer>().gameTime2 = resetTime;
    }


    public void GetFinalScore()
    {
        Debug.Log("GetFinalScore()");
;       Invoke("TotalScoreMove", 1);
        Invoke("FinalScoreMove", 2);  
    }

    public void FrameActivate()
    {
        if (IsGaming == false)
        {
            frame.SetActive(true);
            Invoke("TotalActivate", 2f);
        }
    }

    void TotalActivate()
    {
        totalScore.SetActive(true);
        Invoke("ScoreActivate", 1.8f);
    }
    void ScoreActivate()
    {
        finalScore.GetComponent<Text>().text = string.Format("{0:0}", GetComponent<ScoreManager>().score);
        finalScore.SetActive(true);
        if (generateFalling.positive == true) 
        {
            positiveKText.SetActive(true);
            positiveEText.SetActive(true);
        }
        else if (generateFalling.positive == false) 
        {
            negativeKText.SetActive(true);
            negativeEText.SetActive(true);
        }
        endScoreText = true;
        Invoke("EndScoreDisabled", 15);
    }
    void PressEnterActive()
    {
        pressEnter.SetActive(true);
    }

    void EndScoreDisabled()
    {
        frame.SetActive(false);
        totalScore.SetActive(false);
        finalScore.SetActive(false);
        pressEnter.SetActive(false);
        positiveKText.SetActive(false);
        positiveEText.SetActive(false);
        negativeKText.SetActive(false);
        negativeEText.SetActive(false);
    }
}
