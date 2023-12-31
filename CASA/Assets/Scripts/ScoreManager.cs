﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public int score = 0;
    public bool changeLightNum = false;
    [SerializeField] Text ScoreTextUI;

    GenerateFallingTrash generateFallingTrash;
    public int teamBalance = 1;

    [SerializeField] private GameObject[] MissionUIArray = new GameObject[3];
    private bool[] MissionBoolArray = new bool[3] { false, false, false };

    [SerializeField] public GameObject[] HandObjectArray = new GameObject[4];

    [SerializeField] private GameObject[] PositiveEndingObjectArray = new GameObject[3];
    [SerializeField] private GameObject[] NagativeEndingObjectArray = new GameObject[3];

    [SerializeField] private GameObject LoadCircularUI;
    [SerializeField] private GameObject LoadReverseCircularUI;

    [SerializeField] public GameObject[] GaugeArray = new GameObject[3];

    float scoreSave0;
    float scoreSave1;
    float scoreSave2;

    SliderTimer sliderTimer;
    ActivateUI activateUI;

    public int NumVertical;
    public int NumHorizontal;
    public int NumCircle;
    public int NumRCircle;

	
    [SerializeField] GameObject MissionPointUI;

    int basicBGM;
    private SoundManager soundManagerScript;

    bool OpenFirstMission = false;
    bool OpenSecondMission = false;
    bool OpenThirdMission = false;



    void Awake()
    {
        generateFallingTrash = this.GetComponent<GenerateFallingTrash>();
        sliderTimer = this.GetComponent<SliderTimer>();
        activateUI = this.GetComponent<ActivateUI>();
        
        soundManagerScript = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        basicBGM = soundManagerScript.bGMNumber;
        scoreSave0 = GaugeArray[0].GetComponent<Slider>().value;
        scoreSave1 = GaugeArray[1].GetComponent<Slider>().value;
        scoreSave2 = GaugeArray[2].GetComponent<Slider>().value;

        GaugeArray[0].GetComponent<Slider>().maxValue = 30 * teamBalance;
        GaugeArray[1].GetComponent<Slider>().maxValue = 20 * teamBalance;
        GaugeArray[2].GetComponent<Slider>().maxValue = 10 * teamBalance;
    }

    void Update()
   {
        if (GaugeArray[0].GetComponent<Slider>().value - scoreSave0> 1)
        {
            soundManagerScript.SFXSound(soundManagerScript.sFXList[1]);
            scoreSave0 = GaugeArray[0].GetComponent<Slider>().value;
			//Debug.Log("1");
        }
        if (GaugeArray[1].GetComponent<Slider>().value - scoreSave1> 1)
        {
            soundManagerScript.SFXSound(soundManagerScript.sFXList[1]);
            scoreSave1 = GaugeArray[1].GetComponent<Slider>().value;
			//Debug.Log("2");
		}
        if (GaugeArray[2].GetComponent<Slider>().value - scoreSave2 > 1)
        {
            soundManagerScript.SFXSound(soundManagerScript.sFXList[1]);
            scoreSave2 = GaugeArray[2].GetComponent<Slider>().value;
			//Debug.Log("3");
		}
        ScoreTextUI.text = string.Format("{0:0}", score);

        if (generateFallingTrash.isTeam == true)
        {
            teamBalance = 3;
        }
        else
        {
            teamBalance = 1;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            OpenFirstMission = true;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            OpenSecondMission = true;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            OpenThirdMission = true;
        }

        if ((score >= 4500 || sliderTimer.gameTime2 > sliderTimer.gameTime -1) && OpenThirdMission == true) //최종 시간에 도달하거나 4700점에 도달할 시 마지막 미션 등장
        {
            NumCircle = HandObjectArray[0].GetComponent<HandTracker>().numOfCircles + HandObjectArray[1].GetComponent<HandTracker>().numOfCircles + HandObjectArray[2].GetComponent<HandTracker>().numOfCircles + HandObjectArray[3].GetComponent<HandTracker>().numOfCircles;
            GaugeArray[2].GetComponent<Slider>().value = NumCircle;
            
            if (generateFallingTrash.IsMissionTime == false)
            {
                ActiveThirdEvent();
            }
            else if (NumCircle >= 5 * teamBalance)
            {
                LoadCircularUI.SetActive(false);
                LoadReverseCircularUI.SetActive(true);

                NumRCircle = HandObjectArray[0].GetComponent<HandTracker>().numOfRCircles + HandObjectArray[1].GetComponent<HandTracker>().numOfRCircles + HandObjectArray[2].GetComponent<HandTracker>().numOfRCircles + HandObjectArray[3].GetComponent<HandTracker>().numOfRCircles;
                GaugeArray[2].GetComponent<Slider>().value = 5 * teamBalance + NumRCircle;
                if (NumRCircle >= 5 * teamBalance)
                    FinishThirdEvent();
            }
        }
        else if(score >= 2500 && OpenSecondMission == true) //60초 후나 2500점 도달하면 두번째 미션 등장
        {
            NumVertical = HandObjectArray[0].GetComponent<HandTracker>().zeroCrossings + HandObjectArray[1].GetComponent<HandTracker>().zeroCrossings + HandObjectArray[2].GetComponent<HandTracker>().zeroCrossings + HandObjectArray[3].GetComponent<HandTracker>().zeroCrossings;
            GaugeArray[1].GetComponent<Slider>().value = NumVertical;
            
            if (generateFallingTrash.IsMissionTime == false)
            {
                ActiveSecondEvent();
            }
            else if (NumVertical >= 20 * teamBalance)
            {
                FinishSecondEvent();
            }
        }
        else if(score >= 600 && OpenFirstMission == true) //30초 후나 600점 도달하면 두번째 미션 등장
        {
            NumHorizontal = HandObjectArray[0].GetComponent<HandTracker>().zeroCrossings_H + HandObjectArray[1].GetComponent<HandTracker>().zeroCrossings_H + HandObjectArray[2].GetComponent<HandTracker>().zeroCrossings_H + HandObjectArray[3].GetComponent<HandTracker>().zeroCrossings_H;
            GaugeArray[0].GetComponent<Slider>().value = NumHorizontal-6;
            
            if (generateFallingTrash.IsMissionTime == false)
            {
                ActiveFirstEvent();
            }
            else if (NumHorizontal >= 30 * teamBalance)
            {
                FinishFirstEvent();
            }
        }
   }

    void FixedUpdate()
    {
        
    }

    void ActiveFirstEvent()
    {
        if (MissionBoolArray[0] == true)
            return;
        soundManagerScript.bGMNumber = 5;
        soundManagerScript.BGMSound();
        soundManagerScript.SFXSound(soundManagerScript.sFXList[6]);
        MissionUIArray[0].SetActive(true);
            HandObjectArray[0].GetComponent<HandTracker>().InitReset();
            HandObjectArray[1].GetComponent<HandTracker>().InitReset();
            HandObjectArray[2].GetComponent<HandTracker>().InitReset();
			HandObjectArray[3].GetComponent<HandTracker>().InitReset();

        MissionBoolArray[0] = true;
        generateFallingTrash.IsMissionTime = true;
    }

    void FinishFirstEvent()
    {
        if (generateFallingTrash.IsMissionTime == false)
            return;
        soundManagerScript.bGMNumber = basicBGM;
        soundManagerScript.BGMSound();
        soundManagerScript.SFXSound(soundManagerScript.sFXList[2]);
        
        MissionUIArray[0].SetActive(false);
        score += 1000;
        generateFallingTrash.IsMissionTime = false;
        if (generateFallingTrash.positive == true)
            PositiveEndingObjectArray[0].SetActive(true);
        else
            NagativeEndingObjectArray[0].SetActive(true);
        ActiveUI();
		    HandObjectArray[0].GetComponent<HandTracker>().InitReset();
            HandObjectArray[1].GetComponent<HandTracker>().InitReset();
            HandObjectArray[2].GetComponent<HandTracker>().InitReset();
			HandObjectArray[3].GetComponent<HandTracker>().InitReset();
        Invoke("Whale",15);
    }

    void ActiveSecondEvent()
    {
        if (MissionBoolArray[1] == true)
            return;
        soundManagerScript.bGMNumber = 5;
        soundManagerScript.BGMSound();
        soundManagerScript.SFXSound(soundManagerScript.sFXList[6]);
        MissionUIArray[1].SetActive(true);
            HandObjectArray[0].GetComponent<HandTracker>().InitReset();
            HandObjectArray[1].GetComponent<HandTracker>().InitReset();
            HandObjectArray[2].GetComponent<HandTracker>().InitReset();
			HandObjectArray[3].GetComponent<HandTracker>().InitReset();
		MissionBoolArray[1] = true;
        generateFallingTrash.IsMissionTime = true;
    }

    void FinishSecondEvent()
    {
        if (generateFallingTrash.IsMissionTime == false)
            return;
        soundManagerScript.bGMNumber = basicBGM;
        soundManagerScript.BGMSound();
        soundManagerScript.SFXSound(soundManagerScript.sFXList[2]);
        MissionUIArray[1].SetActive(false);
        score += 1000;
        generateFallingTrash.IsMissionTime = false;
        if (generateFallingTrash.positive == true)
            PositiveEndingObjectArray[1].SetActive(true);
        else
            NagativeEndingObjectArray[1].SetActive(true);
        ActiveUI();
			HandObjectArray[0].GetComponent<HandTracker>().InitReset();
            HandObjectArray[1].GetComponent<HandTracker>().InitReset();
            HandObjectArray[2].GetComponent<HandTracker>().InitReset();
			HandObjectArray[3].GetComponent<HandTracker>().InitReset();
    }

    void ActiveThirdEvent()
    {
        if (MissionBoolArray[2] == true)
            return;
        soundManagerScript.bGMNumber = 5;
        soundManagerScript.BGMSound();
        soundManagerScript.SFXSound(soundManagerScript.sFXList[6]);
        MissionUIArray[2].SetActive(true);
            HandObjectArray[0].GetComponent<HandTracker>().InitReset();
            HandObjectArray[1].GetComponent<HandTracker>().InitReset();
            HandObjectArray[2].GetComponent<HandTracker>().InitReset();
			HandObjectArray[3].GetComponent<HandTracker>().InitReset();
        HandObjectArray[0].GetComponent<HandTracker>().minimun_height = 0;
        HandObjectArray[0].GetComponent<HandTracker>().minimun_width = 0;
        HandObjectArray[1].GetComponent<HandTracker>().minimun_height = 0;
        HandObjectArray[1].GetComponent<HandTracker>().minimun_width = 0;
        HandObjectArray[2].GetComponent<HandTracker>().minimun_height = 0;
        HandObjectArray[2].GetComponent<HandTracker>().minimun_width = 0;
		HandObjectArray[3].GetComponent<HandTracker>().minimun_height = 0;
        HandObjectArray[3].GetComponent<HandTracker>().minimun_width = 0;

        MissionBoolArray[2] = true;
        generateFallingTrash.IsMissionTime = true;
        
    }

    void FinishThirdEvent()
    {
        if (generateFallingTrash.IsMissionTime == false)
            return;
        soundManagerScript.bGMNumber = basicBGM;
        soundManagerScript.BGMSound();
        soundManagerScript.SFXSound(soundManagerScript.sFXList[2]);
        MissionUIArray[2].SetActive(false);
        score += 1000;
        generateFallingTrash.IsMissionTime = false;
		activateUI.IsGaming = false;
        if (generateFallingTrash.positive == true)
            PositiveEndingObjectArray[2].SetActive(true);
        else
            NagativeEndingObjectArray[2].SetActive(true);
        generateFallingTrash.endingSystem();
        ActiveUI();
			HandObjectArray[0].GetComponent<HandTracker>().InitReset();
            HandObjectArray[1].GetComponent<HandTracker>().InitReset();
            HandObjectArray[2].GetComponent<HandTracker>().InitReset();
			HandObjectArray[3].GetComponent<HandTracker>().InitReset();
    }

    void ActiveUI()
    {
        MissionPointUI.SetActive(true);
        Invoke("UnActiveUI", 2);
    }

    void UnActiveUI()
    {
        MissionPointUI.SetActive(false);
    }

    void Whale()
    {
        soundManagerScript.SFXSound(soundManagerScript.sFXList[9]);
    }
}
