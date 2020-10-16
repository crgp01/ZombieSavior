using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int recolectedCoins;
    public Text coinScore;
    public GameObject gunImage;
    public GameObject gunInHand;
    public bool weapon1Collected = false;
    public Slider lifeBarSlider;


    // Start is called before the first frame update
    private void Start()
    {
     
    }
    private void Update()
    {
        coinScore.text = $"Monedas: {recolectedCoins}";
        if (weapon1Collected == true)
        {
            gunImage.gameObject.SetActive(true);
            gunInHand.gameObject.SetActive(true);
        }
    }
    private void ZombieMode()
    {
        if (lifeBarSlider.value == 0)
        {
            Debug.Log("Modo zombie");
        }
       
    }
}
