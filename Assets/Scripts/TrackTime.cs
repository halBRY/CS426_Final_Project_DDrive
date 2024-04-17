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

    public float startTime;
    public float currentTime;
    public float endTime;

    public TMP_Text timer;

    public bool gameStarted = false;

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
        accurText.text = string.Format("{0:#.00}%", myPlayer.getAccuracy());
        scoreText.text = accuracyManager.getScore().ToString();
    }
}
