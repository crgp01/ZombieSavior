using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class StoreController : MonoBehaviour
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
    private string NO_ABLE_BUY_SHOTGUN = "La compra de esta arma está deshabilitada";
    public ScoreManager scoreManager;
    public RemoteConfigs remoteConfigs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCoins) {
            currentCoins.text = $"{scoreManager.recolectedCoins}";
        }
        if (hasShootgun) {
            unavailableText.SetActive(true);
        }
    }
    public void purchaseGreenPotion() {

        if (scoreManager.recolectedCoins >= GREEN_POTION_PRICE)
        {
            greenPotionCounter++;
            scoreManager.recolectedCoins = scoreManager.recolectedCoins - GREEN_POTION_PRICE;

            IDictionary<string, object> eventDictionary = new Dictionary<string, object> { };
            eventDictionary.Add("Item", "Green Potion");
            eventDictionary.Add("Potion Price", GREEN_POTION_PRICE);
            eventDictionary.Add("Level", 1);

            Analytics.CustomEvent("Purchase green potion", eventDictionary);
        }
        else {
            errorMessage.text = NO_COINS_MESSAGE;
            IDictionary<string, object> eventDictionary = new Dictionary<string, object> { };
            eventDictionary.Add("Current Coins", scoreManager.recolectedCoins);
            eventDictionary.Add("Potion Price", GREEN_POTION_PRICE);
            eventDictionary.Add("Level", 1);

            Analytics.CustomEvent("Error Purchasing green potion", eventDictionary);
        }
    }
    public void purchaseYellowPotion() {
        if (scoreManager.recolectedCoins >= YELLOW_POTION_PRICE)
        {
            yellowPotionCounter++;
            scoreManager.recolectedCoins = scoreManager.recolectedCoins - YELLOW_POTION_PRICE;
            IDictionary<string, object> eventDictionary = new Dictionary<string, object> { };
            eventDictionary.Add("Item", "Yellow Potion");
            eventDictionary.Add("Potion Price", YELLOW_POTION_PRICE);
            eventDictionary.Add("Level", 1);

            Analytics.CustomEvent("Purchase yellow potion", eventDictionary);
        }
        else {
            errorMessage.text = NO_COINS_MESSAGE;
            IDictionary<string, object> eventDictionary = new Dictionary<string, object> { };
            eventDictionary.Add("Current Coins", scoreManager.recolectedCoins);
            eventDictionary.Add("Potion Price", YELLOW_POTION_PRICE);
            eventDictionary.Add("Level", 1);

            Analytics.CustomEvent("Error Purchasing yellow potion", eventDictionary);
        }
    }
    public void purchaseShotgun() {
        if (remoteConfigs.canPurchaseShotgun)
        {
            if (!hasShootgun)
            {
                if (scoreManager.recolectedCoins >= SHOT_GUN_PRICE)
                {
                    hasShootgun = true;
                    scoreManager.shotgunEquiped = true;
                    scoreManager.recolectedCoins = scoreManager.recolectedCoins - SHOT_GUN_PRICE;

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
        }
        else {
            errorMessage.text = NO_ABLE_BUY_SHOTGUN;
        }
    }
}
