﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    Animator animator;
    GameObject medicine;
    GameObject curePosition;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        medicine = GameObject.FindGameObjectWithTag("Cure");
        curePosition = GameObject.FindGameObjectWithTag("CurePosition");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            animator.Play("Pickup");
            other.gameObject.SetActive(false);
            scoreManager.recolectedCoins++;
           
        }
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
            scoreManager.lifeBarSlider.value = 3;
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
        if (scoreManager.lifeBarSlider.value == 0) {
            medicine.gameObject.SetActive(true);
            curePosition.gameObject.SetActive(true);
        }
     
    }

}
