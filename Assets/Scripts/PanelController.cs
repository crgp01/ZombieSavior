using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    public GameObject storyPanel;
    public GameObject cemeteryPanel;
    public GameObject endgamePanel;
    public ScoreManager scoreManager;
    private GameObject instructionText;
    // Start is called before the first frame update
    void Start()
    {
        instructionText = GameObject.FindGameObjectWithTag("InstructionText");
        storyPanel.gameObject.SetActive(false);
        cemeteryPanel.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreManager.timeRemaining == 0) {
            PauseGame();
            endgamePanel.gameObject.SetActive(true);
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
            instructionText.gameObject.SetActive(false);
        }

    }

    public void CloseStoryPanel() {
        storyPanel.gameObject.SetActive(false);
        UnpauseGame();
        scoreManager.showStory = false;
    }

    public void CloseCemeteryPanel() {
        cemeteryPanel.gameObject.SetActive(false);
        UnpauseGame();
        scoreManager.showSignal = false;
        instructionText.gameObject.SetActive(false);
    }

    private void PauseGame() {
        Time.timeScale = 0f;
    }
    private void UnpauseGame() {
        Time.timeScale = 1;
    }
}
