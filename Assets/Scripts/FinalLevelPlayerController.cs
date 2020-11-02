﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalLevelPlayerController : MonoBehaviour
{
    [SerializeField] private FinalLevelController scoreManager;
    Animator animator;
    public GameObject medicine;
    private PanelController panelController;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        medicine.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            scoreManager.lifeBarSlider.value -= 1;
        }
        if (other.CompareTag("Cure"))
        {
            medicine.gameObject.SetActive(false);
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
            col.gameObject.SetActive(false);
            scoreManager.recolectedCoins++;
        }
        if (col.gameObject.tag == "CureHouse")
        {
            col.gameObject.SetActive(false);
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
        if (scoreManager.zombieMode)
        {
            medicine.gameObject.SetActive(true);
        }

    }
}