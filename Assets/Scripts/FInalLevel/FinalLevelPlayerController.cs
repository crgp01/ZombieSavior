using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class FinalLevelPlayerController : MonoBehaviour
{
    [SerializeField] public FinalLevelController scoreManager;
    Animator animator;
    public GameObject medicine;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
         if (other.CompareTag("Cure"))
        {
            FindObjectOfType<AudioManager>().Play("Cure");
            scoreManager.cureText.gameObject.SetActive(false);
            scoreManager.lifeBarSlider.value = 3;
            scoreManager.medicinePicked = true;
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Coin")
        {
            animator.Play("Pickup");
            FindObjectOfType<AudioManager>().Play("CoinPickup");
            col.gameObject.SetActive(false);
            scoreManager.recolectedCoins++;
        }
        if (col.gameObject.tag == "CureHouse")
        {
            col.gameObject.SetActive(false);
            scoreManager.cureWasPicked = true;
        }
        if (col.gameObject.tag == "ZombieBoss")
        {
            IDictionary<string, object> eventDictionary = new Dictionary<string, object> { };
            eventDictionary.Add("Life value", scoreManager.lifeBarSlider.value);
            eventDictionary.Add("Player Position", transform.position);
            eventDictionary.Add("Damage Amount", 1);
            eventDictionary.Add("Level", 2);

            Analytics.CustomEvent("Player Damaged by Zombie Boss", eventDictionary);

            scoreManager.lifeBarSlider.value -= 1;
            FindObjectOfType<AudioManager>().Play("ArchieHurt");
        }
        if (col.gameObject.tag == "Zombie")
        {
            IDictionary<string, object> eventDictionary = new Dictionary<string, object> { };
            eventDictionary.Add("Life value", scoreManager.lifeBarSlider.value);
            eventDictionary.Add("Player Position", transform.position);
            eventDictionary.Add("Damage Amount", 1);
            eventDictionary.Add("Level", 2);

            Analytics.CustomEvent("Player Damaged by Zombie", eventDictionary);
            scoreManager.lifeBarSlider.value -= 1;
            FindObjectOfType<AudioManager>().Play("ArchieHurt");
        }
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            animator.Play("Wave");
        }
    }
    private void Update()
    {
        if (scoreManager.zombieMode)
        {
            IDictionary<string, object> eventDictionary = new Dictionary<string, object> { };
            eventDictionary.Add("Player position", transform.position);
            eventDictionary.Add("Level", 2);

            Analytics.CustomEvent("Searching cure", eventDictionary);
        }

    }
}