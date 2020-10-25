﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelController : MonoBehaviour
{
    public GameObject storyPanel;
    public GameObject miniMapPanel;
    public GameObject mainPanel;
    public GameObject playerPanel;
    public GameObject cemeteryPanel;
    public GameObject endgamePanel;
    public GameObject gameOptionsPanel;
    public GameObject creditsPanel;
    public ScoreManager scoreManager;
    private GameObject instructionText;
    public GameObject gunInstructionText;
    public GameObject coinInstructionText;
    public Text diariesInstructionText;
    public Text coinCounterText;
    // Start is called before the first frame update
    void Start()
    {
        PauseGame();
        miniMapPanel.gameObject.SetActive(false);
        playerPanel.gameObject.SetActive(false);
        instructionText = GameObject.FindGameObjectWithTag("InstructionText");
        storyPanel.gameObject.SetActive(false);
        cemeteryPanel.gameObject.SetActive(false);
        
    }

    // Update is called once per frames
    void Update()
    {
        coinCounterText.text = $"Recoge 20 monedas: {scoreManager.recolectedCoins}/20";
        diariesInstructionText.text = $"Recoge los diarios del Dr. Cuaticus: {scoreManager.diariesCounter}/5";

        if (scoreManager.timeRemaining == 0) {
            PauseGame();
            endgamePanel.gameObject.SetActive(true);
            playerPanel.gameObject.SetActive(false);
        }
        if (scoreManager.showSignal)
        {
            PauseGame();
            cemeteryPanel.gameObject.SetActive(true);
        }
        if (scoreManager.showStory) {
            PauseGame();
            storyPanel.gameObject.SetActive(true);
        }
        if (scoreManager.cureWasPicked) {
            instructionText.GetComponent<Text>().color = Color.green;
            gunInstructionText.gameObject.SetActive(true);
        }
        if (scoreManager.weapon1Collected) {
            gunInstructionText.GetComponent<Text>().color = Color.green;
        }
        if (scoreManager.allCoinsCollected) {
            coinInstructionText.GetComponent<Text>().color = Color.green;
        }
        if (scoreManager.allDiariesCollected) {
            diariesInstructionText.GetComponent<Text>().color = Color.green;
        }

    }

    public void CloseStoryPanel() {
        storyPanel.gameObject.SetActive(false);
        UnpauseGame();
        scoreManager.showStory = false;
    }
    public void PlayGame() {
        mainPanel.gameObject.SetActive(false);
        playerPanel.gameObject.SetActive(true);
        miniMapPanel.gameObject.SetActive(true);
        UnpauseGame();
    }
    public void QuitGame() {
        #if UNITY_EDITOR
   
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                 Application.Quit();
        #endif
    }

    public void CloseCemeteryPanel() {
        cemeteryPanel.gameObject.SetActive(false);
        UnpauseGame();
        scoreManager.showSignal = false;
    }

    private void PauseGame() {
        Time.timeScale = 0f;
    }
    private void UnpauseGame() {
        Time.timeScale = 1;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void GoToLevel2()
    {
        SceneManager.LoadScene("Nivel2");
    }
    public void GoToGameOptions()
    {
        gameOptionsPanel.SetActive(true);
        mainPanel.gameObject.SetActive(false);
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
        mainPanel.gameObject.SetActive(true);
    }
}
