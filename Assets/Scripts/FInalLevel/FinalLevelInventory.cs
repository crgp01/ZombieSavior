using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Analytics;

public class FinalLevelInventory : MonoBehaviour
{
    public GameObject greenPotionPanel;
    public GameObject yellowPotionPanel;
    public GameObject shotgunPanel;
    public GameObject pistolPanel;
    public GameObject diaryPanel;
    public GameObject coinsPanel;
    public GameObject pistolButton;
    public GameObject shotgunButton;
    public Text greenPotionCounterText;
    public Text yellowPotionCounterText;
    public Text diariesCounterText;
    public Text errorPanel;
    public FinalLevelShop storeController;
    public FinalLevelController finalLevelController;
    public RemoteConfigs remoteConfigs;
    private string COMPLETE_LIFE_MESSAGE = "Ya tienes la vida completa";
    private string NO_POTION_MESSAGE = "No tienes mas pociones";
    private string NO_ZOMBIE_MODE_MESSAGE = "El contador no está corriendo";
    private string ZERO_COUNTER_MESSAGE = "Ya se terminó el tiempo";
    private string APPLIED_POTION_MESSAGE = "Poción aplicada";
    private string EQUIPED_WEAPON = "Arma equipada";
    private string ALREADY_EQUIPED_WEAPON = "Ya tienes esa arma equipada";
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (storeController.hasShootgun)
        {
            shotgunButton.gameObject.SetActive(true);
        }
        greenPotionCounterText.text = $"x{storeController.greenPotionCounter}";
        yellowPotionCounterText.text = $"x{storeController.yellowPotionCounter}";
        diariesCounterText.text = $"x5";
    }
    public void ShowGreenPotionPanel()
    {
        greenPotionPanel.SetActive(true);
        yellowPotionPanel.SetActive(false);
        shotgunPanel.SetActive(false);
        pistolPanel.SetActive(false);
        diaryPanel.SetActive(false);
        coinsPanel.SetActive(false);
    }
    public void UseGreenPotionPanel()
    {
        if (storeController.greenPotionCounter > 0)
        {
            if (finalLevelController.lifeBarSlider.value < 3)
            {
                storeController.greenPotionCounter--;
                finalLevelController.lifeBarSlider.value = 3;
                errorPanel.text = APPLIED_POTION_MESSAGE;
                errorPanel.GetComponent<Text>().color = Color.green;

                IDictionary<string, object> eventDictionary = new Dictionary<string, object> { };
                eventDictionary.Add("Item", "Green Potion");
                eventDictionary.Add("Location use", transform.position);
                eventDictionary.Add("Level", 2);

                Analytics.CustomEvent("Use green potion", eventDictionary);
            }
            else
            {
                errorPanel.GetComponent<Text>().color = Color.red;
                errorPanel.text = COMPLETE_LIFE_MESSAGE;
            }
        }
        else
        {
            errorPanel.GetComponent<Text>().color = Color.red;
            errorPanel.text = NO_POTION_MESSAGE;
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
    public void ShowDiariesPanel()
    {
        greenPotionPanel.SetActive(false);
        yellowPotionPanel.SetActive(false);
        shotgunPanel.SetActive(false);
        pistolPanel.SetActive(false);
        diaryPanel.SetActive(true);
        coinsPanel.SetActive(false);
    }
    public void ShowPistolPanel()
    {
        greenPotionPanel.SetActive(false);
        yellowPotionPanel.SetActive(false);
        shotgunPanel.SetActive(false);
        pistolPanel.SetActive(true);
        diaryPanel.SetActive(false);
        coinsPanel.SetActive(false);
    }
    public void ShowShotgunPanel()
    {
        greenPotionPanel.SetActive(false);
        yellowPotionPanel.SetActive(false);
        shotgunPanel.SetActive(true);
        pistolPanel.SetActive(false);
        diaryPanel.SetActive(false);
        coinsPanel.SetActive(false);
    }
    public void UseYellowPotionPanel()
    {
        if (finalLevelController.timerIsRunning)
        {
            if (finalLevelController.timeRemaining >= 1)
            {
                if (storeController.yellowPotionCounter > 0)
                {
                    storeController.yellowPotionCounter--;
                    finalLevelController.timeRemaining += remoteConfigs.potionIncreasingTime;
                    errorPanel.text = APPLIED_POTION_MESSAGE;
                    errorPanel.GetComponent<Text>().color = Color.green;

                    IDictionary<string, object> eventDictionary = new Dictionary<string, object> { };
                    eventDictionary.Add("Item", "Yellow Potion");
                    eventDictionary.Add("Location use", transform.position);
                    eventDictionary.Add("Level", 2);

                    Analytics.CustomEvent("Use Yellow Potion", eventDictionary);
                }
                else
                {
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
    public void EquipShotgun()
    {
        if (!finalLevelController.shotgunEquiped)
        {
            finalLevelController.shotgunEquiped = true;
            finalLevelController.pistolEquiped = false;
            errorPanel.text = EQUIPED_WEAPON;
            errorPanel.GetComponent<Text>().color = Color.green;

            IDictionary<string, object> eventDictionary = new Dictionary<string, object> { };
            eventDictionary.Add("Item", "Shotgun");
            eventDictionary.Add("Location use", transform.position);
            eventDictionary.Add("Level", 2);

            Analytics.CustomEvent("Equip Shotgun", eventDictionary);
        }
        else
        {
            errorPanel.GetComponent<Text>().color = Color.red;
            errorPanel.text = ALREADY_EQUIPED_WEAPON;
        }

    }
    public void EquipPistol()
    {
        if (!finalLevelController.pistolEquiped)
        {
            finalLevelController.shotgunEquiped = false;
            finalLevelController.pistolEquiped = true;
            errorPanel.text = EQUIPED_WEAPON;
            errorPanel.GetComponent<Text>().color = Color.green;

            IDictionary<string, object> eventDictionary = new Dictionary<string, object> { };
            eventDictionary.Add("Item", "Pistol");
            eventDictionary.Add("Location use", transform.position);
            eventDictionary.Add("Level", 2);

            Analytics.CustomEvent("Equip Pistol", eventDictionary);
        }
        else
        {
            errorPanel.GetComponent<Text>().color = Color.red;
            errorPanel.text = ALREADY_EQUIPED_WEAPON;
        }
    }
}
