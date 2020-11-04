using System.Collections;
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
            }
            else {
                errorPanel.text = NO_POTION_MESSAGE;
            }
        }
        else {
            errorPanel.text = COMPLETE_LIFE_MESSAGE;
        }
        
    }
}
