﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelController : MonoBehaviour
{
    public GameObject storyPanel, miniMapPanel, mainPanel, playerPanel, cemeteryPanel, townPanel, endgamePanel, gameOptionsPanel,
        storePanel, inventoryPanel, creditsPanel, pausePanel, endLevel1Panel, finalGameObject, instructionText, gunInstructionText,
        coinInstructionText, finalInstructionText, respawnPoint, Document1Panel, Document2Panel, Document3Panel, Document4Panel, Document5Panel;
    public ScoreManager scoreManager;
    public RemoteConfigs remoteConfigs;
    public bool enterStore;
    private GameObject player;
    public Text diariesInstructionText, coinCounterText;

    void Start()
    {
        PauseGame();
        player = GameObject.FindGameObjectWithTag("Player");
        miniMapPanel.gameObject.SetActive(false);
        playerPanel.gameObject.SetActive(false);
        storyPanel.gameObject.SetActive(false);
        cemeteryPanel.gameObject.SetActive(false);
        
    }

    // Update is called once per frames
    void Update()
    {
        coinCounterText.text = $"Recoge {scoreManager.COINS_TARGET} monedas: {scoreManager.recolectedCoins}/{scoreManager.COINS_TARGET}";
        diariesInstructionText.text = $"Recoge los diarios del Dr. Cuaticus: {scoreManager.diariesCounter}/{scoreManager.DIARIES_TARGET}";
        EndGame();
        if (scoreManager.showSignal)
        {
            PauseGame();
            cemeteryPanel.gameObject.SetActive(true);
        }
        if (scoreManager.showStory) {
            PauseGame();
            storyPanel.gameObject.SetActive(true);
        }
        if (scoreManager.showSignal2) {
            PauseGame();
            townPanel.gameObject.SetActive(true);
        }
        if (playerPanel) {
            ChangeColorText();
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            pausePanel.gameObject.SetActive(true);
            PauseGame();
        }
        if (scoreManager.showFinalPanel) {
            endLevel1Panel.gameObject.SetActive(true);
            PauseGame();
        }
        ShowDocumentsPanel(scoreManager.document1WasPicked, Document1Panel);
        ShowDocumentsPanel(scoreManager.document2WasPicked, Document2Panel);
        ShowDocumentsPanel(scoreManager.document3WasPicked, Document3Panel);
        ShowDocumentsPanel(scoreManager.document4WasPicked, Document4Panel);
        ShowDocumentsPanel(scoreManager.document5WasPicked, Document5Panel);
    }

    public void CloseStoryPanel() {
        storyPanel.gameObject.SetActive(false);
        UnpauseGame();
        scoreManager.showStory = false;
    }
    public void CloseTownPanel() {
        townPanel.gameObject.SetActive(false);
        UnpauseGame();
        scoreManager.showSignal2 = false;
    }
    public void CloseDocumentsPanel() {
        Document1Panel.gameObject.SetActive(false);
        scoreManager.document1WasPicked = false;

        Document2Panel.gameObject.SetActive(false);
        scoreManager.document2WasPicked = false;

        Document3Panel.gameObject.SetActive(false);
        scoreManager.document3WasPicked = false;

        Document4Panel.gameObject.SetActive(false);
        scoreManager.document4WasPicked = false;

        Document5Panel.gameObject.SetActive(false);
        scoreManager.document5WasPicked = false;
        UnpauseGame();
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
    public void ClosePauseMenu() {
        pausePanel.gameObject.SetActive(false);
        UnpauseGame();
    }

    private void PauseGame() {
        scoreManager.canShoot = false;
        Time.timeScale = 0f;
    }
    private void UnpauseGame() {
        scoreManager.canShoot = true;
        Time.timeScale = 1;
    }
    public void RestartGame()
    {
        scoreManager.RestartZombieMode();
        scoreManager.KillAllZombies();
        endgamePanel.SetActive(false);
        scoreManager.cureText.SetActive(false);
        playerPanel.SetActive(true);
        miniMapPanel.SetActive(true);
        scoreManager.RespawnPlayer();
        UnpauseGame();

    }
    public void GoToBegining()
    {
        scoreManager.RestartZombieMode();
        scoreManager.KillAllZombies();
        endgamePanel.SetActive(false);
        scoreManager.cureText.SetActive(false);
        scoreManager.RespawnPlayer();
        PauseGame();

    }
    public void GoToLevel2()
    {
        endLevel1Panel.gameObject.SetActive(true);
        SceneManager.LoadScene("Desert");

    }
    public void GoToGameOptions()
    {
        gameOptionsPanel.SetActive(true);
        //mainPanel.gameObject.SetActive(false);
    }
    public void GoToStore()
    {
        if (remoteConfigs.storeIsActive) {
            storePanel.SetActive(true);
            PauseGame();
        }
    }
    public void GoBackFromStore() {
        IDictionary<string, object> eventDictionary = new Dictionary<string, object> { };
        eventDictionary.Add("Screen", "Store");
        eventDictionary.Add("Access location", player.transform.position);
        eventDictionary.Add("Level", 1);

        Analytics.CustomEvent("Access Store Panel", eventDictionary);

        if (enterStore)
        {
            storePanel.SetActive(false);
            enterStore = false;
            UnpauseGame();
        }
        else {
            BackToMenu();
        }
    }
    public void GoToCredits()
    {
        creditsPanel.SetActive(true);
        mainPanel.gameObject.SetActive(false);
    }
    public void GoToInventory()
    {
        inventoryPanel.SetActive(true);
        PauseGame();
        pausePanel.gameObject.SetActive(false);
    }
    public void GoBackFromInventory()
    {
        inventoryPanel.SetActive(false);
        pausePanel.gameObject.SetActive(true);
    }
    public void BackToMenu()
    {
        gameOptionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        endgamePanel.SetActive(false);
        pausePanel.SetActive(false);
        storePanel.SetActive(false);
        mainPanel.gameObject.SetActive(true);
    }
    public void BackToMenuFromEndGame()
    {
        endgamePanel.SetActive(false);
        mainPanel.gameObject.SetActive(true);
        GoToBegining();
    }
    private void ChangeColorText() {
        if (scoreManager.cureWasPicked)
        {
            instructionText.GetComponent<Text>().color = Color.green;
        }
        if (scoreManager.weapon1Collected)
        {
            gunInstructionText.GetComponent<Text>().color = Color.green;
        }
        if (scoreManager.allCoinsCollected)
        {
            coinInstructionText.GetComponent<Text>().color = Color.green;
        }
        if (scoreManager.allDiariesCollected)
        {
            diariesInstructionText.GetComponent<Text>().color = Color.green;
        }if (scoreManager.level1Finished)
        {
            finalInstructionText.GetComponent<Text>().color = Color.red;
            finalGameObject.gameObject.SetActive(true);
        }
    }
    private void ShowDocumentsPanel(bool documentWasPicked, GameObject panelType)
    {
        if (documentWasPicked)
        {
            panelType.gameObject.SetActive(true);
            PauseGame();
        }
    }
    private void EndGame() {
        if (scoreManager.timeRemaining == 0)
        {
            Analytics.CustomEvent("Level 1 Fail", transform.position);
            PauseGame();
            endgamePanel.gameObject.SetActive(true);
            playerPanel.gameObject.SetActive(false);
            miniMapPanel.gameObject.SetActive(false);
        }
    }
  }
  