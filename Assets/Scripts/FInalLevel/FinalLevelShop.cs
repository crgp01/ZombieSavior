﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Analytics;

public class FinalLevelShop : MonoBehaviour
{
    public int greenPotionCounter;
    public GameObject unavailableText;
    public int yellowPotionCounter;
    public bool hasShootgun = false;
    public Text errorMessage;
    public Text currentCoins;
    private int GREEN_POTION_PRICE = 5;
    private int YELLOW_POTION_PRICE = 5;
    private int SHOT_GUN_PRICE = 15;
    private string NO_COINS_MESSAGE = "No tienes suficientes monedas";
    private string SHOTGUN_ERROR_MESSAGE = "Ya tienes esta arma";
    private string NO_ABLE_BUY_SHOTGUN = "La compra de esta arma está deshabilitada";
    public FinalLevelController finalLevelController;
    public RemoteConfigs remoteConfigs;

    private void Awake()
    {
        greenPotionCounter = PlayerPrefs.GetInt("greenPotionCounter");
        yellowPotionCounter = PlayerPrefs.GetInt("yellowPotionCounter");
        hasShootgun = PlayerPrefs.GetInt("hasShootgun") == 1 ? true : false;
    }
    void Update()
    {
        currentCoins.text = $"{finalLevelController.recolectedCoins}";
        if (hasShootgun)
        {
            unavailableText.SetActive(true);
        }
    }
    public void purchaseGreenPotion()
    {
        if (finalLevelController.recolectedCoins >= GREEN_POTION_PRICE)
        {
            greenPotionCounter++;
            FindObjectOfType<AudioManager>().Play("Compra1");
            finalLevelController.recolectedCoins = finalLevelController.recolectedCoins - GREEN_POTION_PRICE;
            IDictionary<string, object> eventDictionary = new Dictionary<string, object> { };
            eventDictionary.Add("Item", "Green Potion");
            eventDictionary.Add("Potion Price", GREEN_POTION_PRICE);
            eventDictionary.Add("Level", 1);

            Analytics.CustomEvent("Purchase green potion", eventDictionary);
        }
        else
        {
            errorMessage.text = NO_COINS_MESSAGE;
            IDictionary<string, object> eventDictionary = new Dictionary<string, object> { };
            eventDictionary.Add("Current Coins", finalLevelController.recolectedCoins);
            eventDictionary.Add("Potion Price", GREEN_POTION_PRICE);
            eventDictionary.Add("Level", 1);

            Analytics.CustomEvent("Error Purchasing green potion", eventDictionary);
        }
    }
    public void purchaseYellowPotion()
    {
        if (finalLevelController.recolectedCoins >= YELLOW_POTION_PRICE)
        {
            yellowPotionCounter++;
            FindObjectOfType<AudioManager>().Play("Compra1");
            finalLevelController.recolectedCoins = finalLevelController.recolectedCoins - YELLOW_POTION_PRICE;
            IDictionary<string, object> eventDictionary = new Dictionary<string, object> { };
            eventDictionary.Add("Item", "Yellow Potion");
            eventDictionary.Add("Potion Price", YELLOW_POTION_PRICE);
            eventDictionary.Add("Level", 1);

            Analytics.CustomEvent("Purchase yellow potion", eventDictionary);
        }
        else
        {
            errorMessage.text = NO_COINS_MESSAGE;
            IDictionary<string, object> eventDictionary = new Dictionary<string, object> { };
            eventDictionary.Add("Current Coins", finalLevelController.recolectedCoins);
            eventDictionary.Add("Potion Price", YELLOW_POTION_PRICE);
            eventDictionary.Add("Level", 1);

            Analytics.CustomEvent("Error Purchasing yellow potion", eventDictionary);
        }
    }
    public void purchaseShotgun()
    {
        if (remoteConfigs.canPurchaseShotgun)
        {
            if (!hasShootgun)
            {
                if (finalLevelController.recolectedCoins >= SHOT_GUN_PRICE)
                {
                    hasShootgun = true;
                    FindObjectOfType<AudioManager>().Play("Compra2");
                    finalLevelController.shotgunEquiped = true;
                    finalLevelController.recolectedCoins = finalLevelController.recolectedCoins - SHOT_GUN_PRICE;

                    IDictionary<string, object> eventDictionary = new Dictionary<string, object> { };
                    eventDictionary.Add("Item", "Shotgun");
                    eventDictionary.Add("Shotgun Price", SHOT_GUN_PRICE);
                    eventDictionary.Add("Level", 1);

                    Analytics.CustomEvent("Purchase shotgun", eventDictionary);
                }
                else
                {
                    errorMessage.text = NO_COINS_MESSAGE;
                }
            }
            else
            {
                errorMessage.text = SHOTGUN_ERROR_MESSAGE;
            }
        } else
        {
            errorMessage.text = NO_ABLE_BUY_SHOTGUN;
        }

    }
}
