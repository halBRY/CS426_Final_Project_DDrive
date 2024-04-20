using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrackTime : MonoBehaviour
{
    public PlayerController myPlayer;
    public AccuracyManager accuracyManager;
    public MenuManager menuManager;

    public GameObject endGUI;
    public GameObject endPoint;

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

    private float parTime = 275f;
    private float parAccuracy = 65f;
    private uint parScore = 2000000;

    public bool trackCleared = true;

    public Color clear;
    public Color fail;

    public Slider scoreBar;
    public Slider accurBar;

    void Start()
    {
        scoreBar.maxValue = 3000000;
        accurBar.maxValue = 0.75f;
    }

    public void beginGame()
    {
        startTime = Time.time;
        gameStarted = true;
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

    public void UpdateScoreBar(uint score)
    {
        scoreBar.value = score;
    }

    public void UpdateAccurBar(float accur)
    {
        accurBar.value = accur;
    }

    public void endGame()
    {
        gameStarted = false;
        menuManager.gameStarted = false;
        endGUI.SetActive(true);
        endTime = currentTime;

        int minutes = Mathf.FloorToInt(endTime / 60f);
        int seconds = Mathf.FloorToInt(endTime % 60f);

        timeText.text = minutes.ToString("00") + " : " + seconds.ToString("00");
        accurText.text = string.Format("{0:#.00}%", (myPlayer.getAccuracy()* 100));
        scoreText.text = accuracyManager.getScore().ToString();

        myPlayer.LockControls();

        Debug.Log("Hello");

        timeTextpassFail.text = "Pass";
        accurTextpassFail.text = "Pass";
        scoreTextpassFail.text = "Pass";

        timeTextpassFail.color = clear;
        accurTextpassFail.color = clear;
        scoreTextpassFail.color = clear;

        if(endTime > parTime)
        {
            timeTextpassFail.text = "Fail";
            timeTextpassFail.color = fail;
            trackCleared = false;
        }
        
        if(myPlayer.getAccuracy()* 100 < parAccuracy)
        {
            accurTextpassFail.text = "Fail";
            accurTextpassFail.color = fail;
            trackCleared = false;
        }

        if(accuracyManager.getScore() < parScore)
        {
            scoreTextpassFail.text = "Fail";
            scoreTextpassFail.color = fail;
            trackCleared = false;
        }

        if(trackCleared)
        {
            clearText.text = "Track Cleared!";
            clearText.color = clear;
        }
        else
        {
            clearText.text = "Track Failed!";
            clearText.color = fail;
        }
    }
}
