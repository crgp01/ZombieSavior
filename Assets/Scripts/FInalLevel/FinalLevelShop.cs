using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FinalLevelShop : MonoBehaviour
{
    public int greenPotionCounter = 1;
    public GameObject unavailableText;
    public int yellowPotionCounter = 0;
    public bool hasShootgun;
    public Text errorMessage;
    public Text currentCoins;
    private int GREEN_POTION_PRICE = 5;
    private int YELLOW_POTION_PRICE = 5;
    private int SHOT_GUN_PRICE = 15;
    private string NO_COINS_MESSAGE = "No tienes suficientes monedas";
    private string SHOTGUN_ERROR_MESSAGE = "Ya tienes esta arma";
    public FinalLevelController finalLevelController;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
            finalLevelController.recolectedCoins =- GREEN_POTION_PRICE;
        }
        else
        {
            errorMessage.text = NO_COINS_MESSAGE;
        }
    }
    public void purchaseYellowPotion()
    {
        if (finalLevelController.recolectedCoins >= YELLOW_POTION_PRICE)
        {
            yellowPotionCounter++;
            finalLevelController.recolectedCoins =- YELLOW_POTION_PRICE;
        }
        else
        {
            errorMessage.text = NO_COINS_MESSAGE;
        }
    }
    public void purchaseShotgun()
    {
        if (!hasShootgun)
        {
            if (finalLevelController.recolectedCoins >= SHOT_GUN_PRICE)
            {
                hasShootgun = true;
                finalLevelController.shotgunEquiped = true;
                finalLevelController.recolectedCoins =- SHOT_GUN_PRICE;
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

    }
}
