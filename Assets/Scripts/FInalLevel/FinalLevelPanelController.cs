﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalLevelPanelController : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject playerPanel;
    public GameObject endgamePanel;
    public GameObject gameOptionsPanel;
    public GameObject creditsPanel;
    public GameObject finalGamePanel;
    public GameObject pausePanel;
    public FinalLevelController scoreManager;
    public GameObject respawnPoint;
    private GameObject player;
    public ZombieBossController ZombieBossController;
    // Start is called before the first frame update
    void Start()
    {
        PauseGame();
        player = GameObject.FindGameObjectWithTag("Player");
        playerPanel.gameObject.SetActive(false);

    }
     
    // Update is called once per frames
    void Update()
    {
        EndGame();
        if (Input.GetKeyDown(KeyCode.P))
        {
            pausePanel.gameObject.SetActive(true);
            PauseGame();
        }
    }

  
    public void PlayGame()
    {
        mainPanel.gameObject.SetActive(false);
        playerPanel.gameObject.SetActive(true);
        UnpauseGame();
    }
    public void QuitGame()
    {
        #if UNITY_EDITOR
   
                        UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    
    public void ClosePauseMenu()
    {
        pausePanel.gameObject.SetActive(false);
        UnpauseGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
    private void UnpauseGame()
    {
        Time.timeScale = 1;
    }
    public void RestartGame()
    {
        scoreManager.RestartZombieMode();
        endgamePanel.SetActive(false);
        scoreManager.cureText.SetActive(false);
        playerPanel.SetActive(true);
        scoreManager.RespawnPlayer();
        ZombieBossController.zombieLifeSlider.value = 6;

        UnpauseGame();

    }
    public void GoToBegining()
    {
        scoreManager.RestartZombieMode();
        endgamePanel.SetActive(false);
        scoreManager.cureText.SetActive(false);
        scoreManager.RespawnPlayer();
        PauseGame();

    }
    public void GoToGameOptions()
    {
        gameOptionsPanel.SetActive(true);
        //mainPanel.gameObject.SetActive(false);
    }
    public void GoToCredits()
    {
        creditsPanel.SetActive(true);
        mainPanel.gameObject.SetActive(false);
    }
    public void BackToMenu()
    {
        gameOptionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        endgamePanel.SetActive(false);
        pausePanel.SetActive(false);
        mainPanel.gameObject.SetActive(true);
    }
    public void BackToMenuFromEndGame()
    {
        endgamePanel.SetActive(false);
        mainPanel.gameObject.SetActive(true);
        GoToBegining();
    }
    public void FinishGame() {
        SceneManager.LoadScene("SampleScene");
    }
    private void EndGame()
    {
        if (scoreManager.timeRemaining == 0)
        {
            PauseGame();
            endgamePanel.gameObject.SetActive(true);
            playerPanel.gameObject.SetActive(false);
        }
    }
}
