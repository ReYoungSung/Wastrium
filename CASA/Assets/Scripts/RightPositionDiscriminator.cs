﻿using System.IO.Compression;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightPositionDiscriminator : MonoBehaviour
{
    private GameObject gameManager;

    private int wasteTypeIndex;
    private GameObject ScorePreFab;
    private GameObject ScorePreFab_300;

    private AreaToCoordinate areaToCoordinate;
    private List<float[]> wasteCoordinateList;
    
    private int type = 0;
    private int score;
    public bool justOnce = false;
	bool scoreGiven = false;

    private SoundManager soundManagerScript;
    GenerateFallingTrash generateFallingTrash;

    float prevPositionY = 0;
    Vector3 fallingTrashPosition = new Vector3(0,0,0);

	float elapsedTime = 0.0f;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        ScorePreFab = gameManager.GetComponent<GoodObjectList>().ScorePreFab;
        ScorePreFab_300 = gameManager.GetComponent<GoodObjectList>().ScorePreFab_300;
        areaToCoordinate = gameManager.GetComponent<AreaToCoordinate>();
        score = gameManager.GetComponent<ScoreManager>().score;
        generateFallingTrash = gameManager.GetComponent<GenerateFallingTrash>();

        wasteCoordinateList = areaToCoordinate.wasteCoordinateList;
        soundManagerScript = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    void Start()
    {
        if(this.gameObject.tag == "Plastic")
        {
            wasteTypeIndex = 0;
        }
        else if(this.gameObject.tag == "Paper")
        {
            wasteTypeIndex = 1;
        }
        else if(this.gameObject.tag == "Can")
        {
            wasteTypeIndex = 2;
        }
        else
        {
            wasteTypeIndex = 3;
        }
    }

    void Update()
    {
        IsInCorrectPlace(); 
    }

    private void IsInCorrectPlace()
    {
        if(generateFallingTrash.positive == false)
        {
            wasteTypeIndex = 0;
        }

        if(justOnce == false)
        { 
            if(this.gameObject.transform.position.x > wasteCoordinateList[wasteTypeIndex][0] &&
                this.gameObject.transform.position.x < wasteCoordinateList[wasteTypeIndex][1] &&
                this.gameObject.transform.position.z > wasteCoordinateList[wasteTypeIndex][2] &&
                this.gameObject.transform.position.z < wasteCoordinateList[wasteTypeIndex][3] &&
                this.gameObject.transform.position.y < wasteCoordinateList[wasteTypeIndex][4])
            {
                //soundManagerScript.SFXSound(soundManagerScript.sFXList[4]);

                if (prevPositionY == null)
                {
                    prevPositionY = this.transform.localPosition.y;
                    return;
                }

                float dist = prevPositionY - this.transform.localPosition.y;

                if (dist != 0 && dist < 0.1f && dist > -0.1f)
                {
                    gameManager.GetComponent<ScoreManager>().changeLightNum = true; 
                    justOnce = true;

                    //GetPoint();
                }

				prevPositionY = this.transform.position.y;
            }
			elapsedTime = 0;
        }
		else if(!scoreGiven) {
			elapsedTime += Time.deltaTime;
			if(elapsedTime > 0.5f) {
				if( this.gameObject.transform.position.x <= wasteCoordinateList[wasteTypeIndex][0] ||
					this.gameObject.transform.position.x >= wasteCoordinateList[wasteTypeIndex][1] ||
					this.gameObject.transform.position.z <= wasteCoordinateList[wasteTypeIndex][2] ||
					this.gameObject.transform.position.z >= wasteCoordinateList[wasteTypeIndex][3] ||
					this.gameObject.transform.position.y >= wasteCoordinateList[wasteTypeIndex][4])
				{
					justOnce =  false;
				}
				else
				{
					GetPoint();
					scoreGiven = true;
				}
			}
		}
    }

    private void changingTrash()
    {
        if (this.gameObject.tag == "Plastic")
        {
            if (generateFallingTrash.spawnedPlasticList[0] == null)
                return;
            // 생성된 오브젝트 리스트에 추가
            fallingTrashPosition = generateFallingTrash.spawnedPlasticList[0].transform.position;
            Destroy(generateFallingTrash.spawnedPlasticList[0]);
            generateFallingTrash.spawnedPlasticList.RemoveAt(0);
        }
        else if (this.gameObject.tag == "Paper")
        {
            if (generateFallingTrash.spawnedPaperList[0] == null)
                return;
            fallingTrashPosition = generateFallingTrash.spawnedPaperList[0].transform.position;
            Destroy(generateFallingTrash.spawnedPaperList[0]);
            generateFallingTrash.spawnedPaperList.RemoveAt(0);
        }
        else if (this.gameObject.tag == "Can")
        {
            if (generateFallingTrash.spawnedCanList[0] == null)
                return;
            fallingTrashPosition = generateFallingTrash.spawnedCanList[0].transform.position;
            Destroy(generateFallingTrash.spawnedCanList[0]);
            generateFallingTrash.spawnedCanList.RemoveAt(0);
        }
        StartCoroutine(ProjectScore());
    }

    IEnumerator ProjectScore()
    {
        GameObject ScoreObject = null;
        if (generateFallingTrash.isTeam == true) //협동 모드일 때 
        {
            ScoreObject = Instantiate(ScorePreFab, fallingTrashPosition, ScorePreFab.transform.rotation);
        }
        else if (generateFallingTrash.isTeam == false)
        {
            ScoreObject = Instantiate(ScorePreFab_300, fallingTrashPosition, ScorePreFab.transform.rotation);
        }
       yield return new WaitForSecondsRealtime(3);
       Destroy(ScoreObject);
    }

    public void GetPoint()
    {
        if (generateFallingTrash.positive == false) //부정 모드일 때 
        {
            if (generateFallingTrash.isTeam == true) //협동 모드일 때 
            {
                generateFallingTrash.SetRandomPosition();
                gameManager.GetComponent<ScoreManager>().score += 100;
            }
            else  //개인 모드
            {
                generateFallingTrash.SetRandomPosition();
                gameManager.GetComponent<ScoreManager>().score += 300;
            }
        }
        else  //긍정 모드일 때 
        {
            changingTrash();
            if (generateFallingTrash.isTeam == true) //협동 모드일 때 
            {
                gameManager.GetComponent<ScoreManager>().score += 100;
            }
            else  //개인 모드
            {
                gameManager.GetComponent<ScoreManager>().score += 300;
            }
        }
        soundManagerScript.SFXSound(soundManagerScript.sFXList[4]);
		gameManager.GetComponent<ScoreManager>().changeLightNum = true;
    }
}