﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class LoadManager : MonoBehaviour
{
	public bool pause = false;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.F5))
		{
			restart();
		}

		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			loadInGameScene();
		}

		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			loadUIScene();
		}

        if (Input.GetKeyDown(KeyCode.Space))
        {
			if(pause == false)
            {
				pauseGame();
			}
            else
            {
				resumeGame();
			}
		}
	}

	void restart()
    {
		int currentIndex = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(currentIndex);
	}

	void loadInGameScene()
    {
		if (SceneManager.GetActiveScene().buildIndex == 1)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
		}
	}

	void loadUIScene()
    {
		if (SceneManager.GetActiveScene().buildIndex == 0)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
		}
	}

	void pauseGame()
    {
		Time.timeScale = 0f;
		pause = true;
	}

	void resumeGame()
    {
		Time.timeScale = 1.0f;
		pause = false;
	}
}
