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
}
