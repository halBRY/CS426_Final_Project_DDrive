using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrackTime : MonoBehaviour
{
    public Button startButton;
    public GameObject backButton;
    public Button controlButton;
    public Button creditsButton;

    public PlayerController myPlayer;
    public AccuracyManager accuracyManager;

    public GameObject startGUI;
    public GameObject endGUI;
    public GameObject endPoint;
    public GameObject mainMenuGUI;
    public GameObject controlGUI;
    public GameObject creditsGUI;

    public TMP_Text timeText;
    public TMP_Text accurText;
    public TMP_Text scoreText;
    public TMP_Text timeTextpassFail;
    public TMP_Text accurTextpassFail;
    public TMP_Text scoreTextpassFail;
    public TMP_Text clearText;

    public float startTime;
    public float currentTime;
    public float endTime;

    public TMP_Text timer;

    public bool gameStarted = false;

    public float parTime = 240f;
    public float parAccuracy = 50f;

    public void beginGame()
    {
        startTime = Time.time;
        startGUI.SetActive(false);
        gameStarted = true;
    }

    public void showControls()
    {
        mainMenuGUI.SetActive(false);
        backButton.SetActive(true);
        controlGUI.SetActive(true);
        creditsGUI.SetActive(false);
    }

    public void showCredits()
    {
        mainMenuGUI.SetActive(false);
        backButton.SetActive(true);
        controlGUI.SetActive(false);
        creditsGUI.SetActive(true);
    }

    public void showMainMenu()
    {
        mainMenuGUI.SetActive(true);
        backButton.SetActive(false);
        controlGUI.SetActive(false);
        creditsGUI.SetActive(false);
    }

    void Update()
    {
        if(gameStarted)
        {
            currentTime += Time.deltaTime;

            int minutes = Mathf.FloorToInt(currentTime / 60f);
            int seconds = Mathf.FloorToInt(currentTime % 60f);
    
            timer.text = minutes.ToString("00") + " : " + seconds.ToString("00");
        }
    }

    public void endGame()
    {
        gameStarted = false;
        endGUI.SetActive(true);
        endTime = currentTime;

        int minutes = Mathf.FloorToInt(endTime / 60f);
        int seconds = Mathf.FloorToInt(endTime % 60f);

        timeText.text = minutes.ToString("00") + " : " + seconds.ToString("00");
        accurText.text = string.Format("{0:#.00}%", (myPlayer.getAccuracy()* 100));
        scoreText.text = accuracyManager.getScore().ToString();

        if(endTime < parTime && (myPlayer.getAccuracy()* 100) > parAccuracy)
        {
            timeTextpassFail.text = "Pass";
            accurTextpassFail.text = "Pass";
            scoreTextpassFail.text = "Pass";
            clearText.text = "Track Cleared";
        }
        else if(endTime > parTime && (myPlayer.getAccuracy()* 100) > parAccuracy)
        {
            timeTextpassFail.text = "Fail";
            accurTextpassFail.text = "Pass";
            scoreTextpassFail.text = "Pass";
            clearText.text = "Track Failed";
        }
        else if(endTime < parTime && (myPlayer.getAccuracy()* 100) < parAccuracy)
        {
            timeTextpassFail.text = "Pass";
            accurTextpassFail.text = "Fail";
            scoreTextpassFail.text = "Pass";
            clearText.text = "Track Failed";
        }
        else
        {
            timeTextpassFail.text = "Fail";
            accurTextpassFail.text = "Fail";
            scoreTextpassFail.text = "Pass";
            clearText.text = "Track Failed";
        }
    }
}
