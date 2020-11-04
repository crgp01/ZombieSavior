using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreController : MonoBehaviour
{
    public int greenPotionCounter = 1;
    public int yellowPotionCounter = 0;
    public bool hasShootgun;
    public Text errorMessage;
    public Text currentCoins;
    private int GREEN_POTION_PRICE = 5;
    private int YELLOW_POTION_PRICE = 5;
    private int SHOT_GUN_PRICE = 15;
    private string NO_COINS_MESSAGE = "No tienes suficientes monedas";
    private string SHOTGUN_ERROR_MESSAGE = "Ya tienes esta arma";
    [SerializeField] private ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentCoins.text = $"{scoreManager.recolectedCoins}";
    }
    public void purchaseGreenPotion() {
        if (scoreManager.recolectedCoins >= GREEN_POTION_PRICE)
        {
            greenPotionCounter++;
            scoreManager.recolectedCoins = scoreManager.recolectedCoins - GREEN_POTION_PRICE;
        }
        else {
            errorMessage.text = NO_COINS_MESSAGE;
        }
    }
    public void purchaseYellowPotion() {
        if (scoreManager.recolectedCoins >= YELLOW_POTION_PRICE)
        {
            yellowPotionCounter++;
            scoreManager.recolectedCoins = scoreManager.recolectedCoins - YELLOW_POTION_PRICE;
        }
        else {
            errorMessage.text = NO_COINS_MESSAGE;
        }
    }
    public void purchaseShotgun() {
        if (!hasShootgun)
        {
            if (scoreManager.recolectedCoins >= SHOT_GUN_PRICE)
            {
                hasShootgun = true;
                scoreManager.recolectedCoins = scoreManager.recolectedCoins - SHOT_GUN_PRICE;
            }
            else
            {
                errorMessage.text = NO_COINS_MESSAGE;
            }
        }
        else {
            errorMessage.text = SHOTGUN_ERROR_MESSAGE;
        }
       
    }
}
