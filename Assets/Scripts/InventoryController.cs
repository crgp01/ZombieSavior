﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public GameObject greenPotionPanel;
    public GameObject yellowPotionPanel;
    public GameObject shotgunPanel;
    public GameObject pistolPanel;
    public GameObject diaryPanel;
    public GameObject coinsPanel;
    public Text greenPotionCounterText;
    public Text yellowPotionCounterText;
    public Text errorPanel;
    public StoreController storeController;
    public ScoreManager scoreManager;
    private string COMPLETE_LIFE_MESSAGE = "Ya tienes la vida completa";
    private string NO_POTION_MESSAGE = "No tienes mas pociones";
    private string NO_ZOMBIE_MODE_MESSAGE = "El contador no está corriendo";
    private string ZERO_COUNTER_MESSAGE = "Ya se terminó el tiempo";
    private string APPLIED_POTION_MESSAGE = "Poción aplicada";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        greenPotionCounterText.text = $"x{storeController.greenPotionCounter}";
        yellowPotionCounterText.text = $"x{storeController.yellowPotionCounter}";
    }
    public void ShowGreenPotionPanel() {
        greenPotionPanel.SetActive(true);
        yellowPotionPanel.SetActive(false);
        shotgunPanel.SetActive(false);
        pistolPanel.SetActive(false);
        diaryPanel.SetActive(false);
        coinsPanel.SetActive(false);
    }
    public void UseGreenPotionPanel() {
        if (scoreManager.lifeBarSlider.value < 3)
        {
            if (storeController.greenPotionCounter > 0)
            {
                storeController.greenPotionCounter--;
                scoreManager.lifeBarSlider.value = 3;
                errorPanel.text = APPLIED_POTION_MESSAGE;
                errorPanel.GetComponent<Text>().color = Color.green;
            }
            else {
                errorPanel.GetComponent<Text>().color = Color.red;
                errorPanel.text = NO_POTION_MESSAGE;
            }
        }
        else {
            errorPanel.GetComponent<Text>().color = Color.red;
            errorPanel.text = COMPLETE_LIFE_MESSAGE;
        }
        
    }
    public void ShowYellowPotionPanel()
    {
        greenPotionPanel.SetActive(false);
        yellowPotionPanel.SetActive(true);
        shotgunPanel.SetActive(false);
        pistolPanel.SetActive(false);
        diaryPanel.SetActive(false);
        coinsPanel.SetActive(false);
    }
    public void UseYellowPotionPanel()
    {
        if (scoreManager.timerIsRunning)
        {
            if (scoreManager.timeRemaining >= 1)
            {
                if (storeController.yellowPotionCounter > 0)
                {
                    storeController.yellowPotionCounter--;
                    scoreManager.timeRemaining += 30;
                    errorPanel.text = APPLIED_POTION_MESSAGE;
                    errorPanel.GetComponent<Text>().color = Color.green;
                }
                else {
                    errorPanel.GetComponent<Text>().color = Color.red;
                    errorPanel.text = ZERO_COUNTER_MESSAGE;
                }
            }
            else
            {
                errorPanel.GetComponent<Text>().color = Color.red;
                errorPanel.text = NO_POTION_MESSAGE;
            }
        }
        else
        {
            errorPanel.GetComponent<Text>().color = Color.red;
            errorPanel.text = NO_ZOMBIE_MODE_MESSAGE;
        }

    }
}
