using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public ScoreManager scoreManager;
    public RemoteConfigs remoteConfigs;
    Animator animator;
    public GameObject medicine;
    public GameObject curePosition;
    public GameObject zombiePrefab;
    public GameObject zombieLocationSpawner;
    public PanelController panelController;
    public ZombieSpawner zombieSpawner;

    void Start()
    {
        animator = GetComponent<Animator>();
        medicine.gameObject.SetActive(false);
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
            medicine.gameObject.SetActive(true);
            curePosition.gameObject.SetActive(true);

            IDictionary<string, object> eventDictionary = new Dictionary<string, object> { };
            eventDictionary.Add("Player position", transform.position);
            eventDictionary.Add("Level", 1);

            Analytics.CustomEvent("Searching cure", eventDictionary);
            scoreManager.lifeBarSlider.value -= 1;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gun"))
        {
            FindObjectOfType<AudioManager>().Play("PickGun");
            animator.Play("Pickup");
            other.gameObject.SetActive(false);
            scoreManager.weapon1Collected = true;
            scoreManager.canShoot = true;
            Instantiate(zombiePrefab, zombieLocationSpawner.transform.position, Quaternion.identity);
        }
        if (other.CompareTag("Cure"))
        {
            Analytics.CustomEvent("Pickup Cure", transform.position);
            FindObjectOfType<AudioManager>().Play("CoinPickup");
            medicine.gameObject.SetActive(false);
            curePosition.gameObject.SetActive(false);
            scoreManager.cureText.gameObject.SetActive(false);
            scoreManager.lifeBarSlider.value = 3;
            scoreManager.medicinePicked = true;
        }
        if (other.CompareTag("FinalGameObject") && scoreManager.level1Finished)
        {
            scoreManager.showFinalPanel = true;

        }
        if (other.CompareTag("Document1"))
        {
            DocumentsCounting("document1", other);
        }
        if (other.CompareTag("Document2"))
        {
            DocumentsCounting("document2", other);
        }
        if (other.CompareTag("Document3"))
        {
            DocumentsCounting("document3", other);
        }
        if (other.CompareTag("Document4"))
        {
            DocumentsCounting("document4", other);
        }
        if (other.CompareTag("Document5"))
        {
            DocumentsCounting("document5", other);
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
        if (col.gameObject.tag == "Signal")
        {
            scoreManager.showSignal = true;
        }
        if (col.gameObject.tag == "CureHouse")
        {
            col.gameObject.SetActive(false);
            scoreManager.showStory = true;
            scoreManager.cureWasPicked = true;
        }
        if (col.gameObject.tag == "Zombie")
        {
            IDictionary<string, object> eventDictionary = new Dictionary<string, object> { };
            eventDictionary.Add("Life value", scoreManager.lifeBarSlider.value);
            eventDictionary.Add("Player Position", transform.position);
            eventDictionary.Add("Damage Amount", 1);
            eventDictionary.Add("Level", 1);

            Analytics.CustomEvent("Player Damaged by Zombie", eventDictionary);
            scoreManager.lifeBarSlider.value -= 1;
            FindObjectOfType<AudioManager>().Play("ArchieHurt");
        }
        if (col.gameObject.tag == "Store" && remoteConfigs.storeIsActive)
        {
            panelController.GoToStore();
            panelController.enterStore = true;
        }
        
    }
    
    private void DocumentsCounting(string documentType, Collider other)
    {
        FindObjectOfType<AudioManager>().Play("PickDiary");
        scoreManager.diariesCounter++;
        animator.Play("Pickup");
        other.gameObject.SetActive(false);

        if (documentType == "document1") {
            scoreManager.document1WasPicked = true;
        } else if (documentType == "document2") {
            scoreManager.document2WasPicked = true;
        } else if (documentType == "document3") {
            scoreManager.document3WasPicked = true;
        } else if (documentType == "document4") {
            scoreManager.document4WasPicked = true;
        } else if (documentType == "document5") {
            scoreManager.document5WasPicked = true;
        }  
    }
}