using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject DemoPauseMenu;
    public GameObject GamePauseMenu;
    public GameObject ControlsMenu;
    public GameObject CreditsMenu;

    public GameObject DemoButton;

    public GameObject GameScene;
    public GameObject DemoScene;

    public GameObject GameUI;

    public PlayerController Player;
    public AccuracyManager accuracyManager;
    public TrackTime trackTime;

    public bool gameStarted = false;

    public void ShowMainMenu()
    {
        Player.isDemo = true;
        HideAllMenu();
        MainMenu.SetActive(true);
    }

    public void ShowPauseMenu(bool isDemo)
    {
        HideAllMenu();
        HideGameUI();
        if(isDemo == true)
        {
            DemoPauseMenu.SetActive(true);
        }
        else if(isDemo == false)
        {
            GamePauseMenu.SetActive(true);
        }
        Player.LockControls();
    }

    public void ShowControlsMenu()
    {
        HideGameUI();
        HideAllMenu();
        ControlsMenu.SetActive(true);
        if(Player.isDemo)
        {
            if(Player.pauseActive)
            {
                DemoButton.SetActive(false);
            }
            else
            {
                DemoButton.SetActive(true);
            }
        }
        else
        {
            DemoButton.SetActive(false);
        }
    }

    public void ShowCredits()
    {
        HideGameUI();
        HideAllMenu();
        CreditsMenu.SetActive(true);
    }

    public void StartControlDemo()
    {
        ShowGameUI();
        Player.UnlockControls();
        HideAllMenu();
    }

    public void StopControlDemo()
    {
        ShowMainMenu();
        HideGameUI();
        Player.LockControls();
    }

    public void HideAllMenu()
    {
        MainMenu.SetActive(false);
        DemoPauseMenu.SetActive(false);
        GamePauseMenu.SetActive(false);
        ControlsMenu.SetActive(false);
        CreditsMenu.SetActive(false);
    }

    public void StartGame()
    {
        ResetGameScene();
        
        gameStarted = true;
        trackTime.beginGame();
        
        Player.isDemo = false;

        DemoScene.SetActive(false);
        GameScene.SetActive(true);

        Player.UnlockControls();
        HideAllMenu();
        ShowGameUI();
    }

    public void PauseOff()
    {
        ShowGameUI();
        Player.pauseActive = false;
    }

    public void BackButton()
    {
        HideAllMenu();
        if(Player.pauseActive)
        {
            if(Player.isDemo)
            {
                DemoPauseMenu.SetActive(true);
            }
            else
            {
                GamePauseMenu.SetActive(true);
            }
        }
        else
        {
            ShowMainMenu();
        }
    }

    public void ShowGameUI()
    {
        GameUI.SetActive(true);
    }

    public void HideGameUI()
    {
        GameUI.SetActive(false);
    }
    public void ResetForDemo()
    {
        ResetGameScene();
        gameStarted = false;
        Player.isDemo = true;

        DemoScene.SetActive(true);
        GameScene.SetActive(false);
    }

    public void ResetGameScene()
    {
        //Move player to start
        Player.GetComponent<CharacterController>().enabled = false;

        Player.transform.position = Player.startingLocation.position;
        Debug.Log("Moving player to " + Player.startingLocation.position);

        Player.lastPos = Player.startingLocation.position;
        Player.GetComponent<CharacterController>().enabled = true;

        //Reset accuracy to 100
        Player.SetAccuracy(1f);

        //Reset scoring
        accuracyManager.score = 0;
        accuracyManager.UpdateScoreText();
        accuracyManager.ResetCombo();
    }
}
