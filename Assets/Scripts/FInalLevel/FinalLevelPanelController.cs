using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
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
    public GameObject inventoryPanel;
    public GameObject storePanel;
    public GameObject pausePanel;
    public FinalLevelController scoreManager;
    public RemoteConfigs remoteConfigs;
    public GameObject respawnPoint;
    private GameObject player;
    private bool enterStore = false;
    public ZombieBossController ZombieBossController;
    public AudioSource bossMusic;
    // Start is called before the first frame update
    void Start()
    {
        PauseGame();
        player = GameObject.FindGameObjectWithTag("Player");
        playerPanel.gameObject.SetActive(false);
        bossMusic = player.GetComponent<AudioSource>();

    }
     
    // Update is called once per frames
    void Update()
    {
        EndGame();
        if (Input.GetKeyDown(KeyCode.P))
        {
            pausePanel.gameObject.SetActive(true);
            playerPanel.gameObject.SetActive(false);
            PauseGame();
        }
    }
    public void PlayGame()
    {
        mainPanel.gameObject.SetActive(false);
        playerPanel.gameObject.SetActive(true);
        bossMusic.Play();
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
        playerPanel.gameObject.SetActive(true);
        UnpauseGame();
    }

    public void PauseGame()
    {
        scoreManager.canShoot = false;
        Time.timeScale = 0f;
    }
    private void UnpauseGame()
    {
        scoreManager.canShoot = true;
        Time.timeScale = 1;
    }
    public void RestartGame()
    {
        scoreManager.RestartZombieMode();
        endgamePanel.SetActive(false);
        scoreManager.cureText.SetActive(false);
        playerPanel.SetActive(true);
        scoreManager.RespawnPlayer();
        ZombieBossController.zombieLifeSlider.value = 10;
        Destroy(scoreManager.medicine1);
        Destroy(scoreManager.medicine2);


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

        IDictionary<string, object> eventDictionary = new Dictionary<string, object> { };
        eventDictionary.Add("Screen", "Credits");
        eventDictionary.Add("Access location", player.transform.position);
        eventDictionary.Add("Level", 2);

        Analytics.CustomEvent("Access Credits Panel", eventDictionary);
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
    public void GoToInventory()
    {
        inventoryPanel.SetActive(true);
        playerPanel.gameObject.SetActive(false);
        PauseGame();
        pausePanel.gameObject.SetActive(false);

        IDictionary<string, object> eventDictionary = new Dictionary<string, object> { };
        eventDictionary.Add("Screen", "Inventory");
        eventDictionary.Add("Access location", player.transform.position);
        eventDictionary.Add("Level", 2);

        Analytics.CustomEvent("Access Inventory Panel", eventDictionary);
    }
    public void GoBackFromInventory()
    {
        inventoryPanel.SetActive(false);
        pausePanel.gameObject.SetActive(true);
        playerPanel.gameObject.SetActive(false);
    }
    public void GoToStore()
    {
        if (remoteConfigs.storeIsActive)
        {
            storePanel.SetActive(true);
            PauseGame();

        }

        IDictionary<string, object> eventDictionary = new Dictionary<string, object> { };
        eventDictionary.Add("Screen", "Store");
        eventDictionary.Add("Access location", player.transform.position);
        eventDictionary.Add("Level", 2);

        Analytics.CustomEvent("Access Store Panel", eventDictionary);
    }
    public void GoBackFromStore()
    {
        if (enterStore)
        {
            storePanel.SetActive(false);
            enterStore = false;
            UnpauseGame();
        }
        else
        {
            BackToMenu();
        }
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
