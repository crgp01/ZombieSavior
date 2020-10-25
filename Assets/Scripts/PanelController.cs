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
    public GameObject gunInstructionText;
    public GameObject coinInstructionText;
    public Text diariesInstructionText;
    public Text coinCounterText;
    // Start is called before the first frame update
    void Start()
    {
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
}
