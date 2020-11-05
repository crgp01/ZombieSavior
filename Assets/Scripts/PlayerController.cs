using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public ScoreManager scoreManager;
    Animator animator;
    public GameObject medicine;
    public GameObject curePosition;
    public PanelController panelController;
    //public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        medicine.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gun"))
        {
            animator.Play("Pickup");
            other.gameObject.SetActive(false);
            scoreManager.weapon1Collected = true;
            scoreManager.canShoot = true;
        }
         if (other.CompareTag("Cure"))
        {
            medicine.gameObject.SetActive(false);
            curePosition.gameObject.SetActive(false);
            scoreManager.cureText.gameObject.SetActive(false);
            scoreManager.lifeBarSlider.value = 3;
            scoreManager.medicinePicked = true;
        }
        if (other.CompareTag("FinalGameObject"))
        {
            Debug.Log("Dentro del trigger");
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
            scoreManager.lifeBarSlider.value -= 1;
        }
        if (col.gameObject.tag == "Store")
        {
            panelController.GoToStore();
            panelController.enterStore = true;
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
            medicine.gameObject.SetActive(true);
            curePosition.gameObject.SetActive(true);
        }

    }
    private void DocumentsCounting(string documentType, Collider other)
    {
        scoreManager.diariesCounter++;
        animator.Play("Pickup");
        other.gameObject.SetActive(false);

        if (documentType == "document1") {
            scoreManager.document1WasPicked = true;
        } else if (documentType == "document2") {
            scoreManager.document2WasPicked = true;
        }
        

    }
}