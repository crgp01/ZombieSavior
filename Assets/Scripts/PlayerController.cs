﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    Animator animator;
    public GameObject medicine;
    public GameObject curePosition;
    private PanelController panelController;

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
        }
        if (other.CompareTag("Zombie"))
        {
            scoreManager.lifeBarSlider.value -= 1;
        }
        if (other.CompareTag("Cure"))
        {
            medicine.gameObject.SetActive(false);
            curePosition.gameObject.SetActive(false);
            scoreManager.cureText.gameObject.SetActive(false);
            scoreManager.lifeBarSlider.value = 3;
            scoreManager.medicinePicked = true;
        }
        if (other.CompareTag("Document")) {
            scoreManager.diariesCounter++;
            animator.Play("Pickup");
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("FinalGameObject"))
        {
            Debug.Log("Dentro del trigger");
            scoreManager.showFinalPanel = true;

        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Coin")
        {
            animator.Play("Pickup");
            col.gameObject.SetActive(false);
            scoreManager.recolectedCoins++;
        }
        if (col.gameObject.tag == "Signal") {
            scoreManager.showSignal = true;
        }
        if (col.gameObject.tag == "CureHouse") {
            col.gameObject.SetActive(false);
            scoreManager.showStory = true;
            scoreManager.cureWasPicked = true;
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
        if (scoreManager.zombieMode) {
            medicine.gameObject.SetActive(true);
            curePosition.gameObject.SetActive(true);
        }
     
    }

}
