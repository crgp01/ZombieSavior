using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelController : MonoBehaviour
{
    public GameObject storyPanel, miniMapPanel, mainPanel, playerPanel, cemeteryPanel, endgamePanel, gameOptionsPanel,
        storePanel, inventoryPanel, creditsPanel, pausePanel, endLevel1Panel, finalGameObject, instructionText, gunInstructionText,
        coinInstructionText, finalInstructionText, respawnPoint, Document1Panel, Document2Panel;
    public ScoreManager scoreManager;
    public bool enterStore;
    private GameObject player;
    public Text diariesInstructionText, coinCounterText;

    void Start()
    {
        //PauseGame();
        player = GameObject.FindGameObjectWithTag("Player");
        miniMapPanel.gameObject.SetActive(false);
        playerPanel.gameObject.SetActive(false);
        storyPanel.gameObject.SetActive(false);
        cemeteryPanel.gameObject.SetActive(false);
        
    }

    // Update is called once per frames
    void Update()
    {
        coinCounterText.text = $"Recoge 20 monedas: {scoreManager.recolectedCoins}/20";
        diariesInstructionText.text = $"Recoge los diarios del Dr. Cuaticus: {scoreManager.diariesCounter}/5";
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
        if (scoreManager.document1WasPicked) {
            Document1Panel.gameObject.SetActive(true);
            PauseGame();
        }
        if (scoreManager.document2WasPicked) {
            Document2Panel.gameObject.SetActive(true);
            PauseGame();
        }
    }

    public void CloseStoryPanel() {
        storyPanel.gameObject.SetActive(false);
        UnpauseGame();
        scoreManager.showStory = false;
    }
    public void CloseDocumentsPanel() {
        Document1Panel.gameObject.SetActive(false);
        scoreManager.document1WasPicked = false;
        Document2Panel.gameObject.SetActive(false);
        scoreManager.document2WasPicked = false;
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
        storePanel.SetActive(true);
        PauseGame();
    }
    public void GoBackFromStore() {
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
    private void EndGame() {
        if (scoreManager.timeRemaining == 0)
        {
            PauseGame();
            endgamePanel.gameObject.SetActive(true);
            playerPanel.gameObject.SetActive(false);
            miniMapPanel.gameObject.SetActive(false);
        }
    }
  }
