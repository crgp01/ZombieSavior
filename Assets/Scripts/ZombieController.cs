﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    private int maxDist = 2;
    private int minDist = 2;
    private GameObject player;
    private Transform playerTransform;
    private Animator animator;
    private GameObject coinPrefab;
    private int velocity = 4;
    //private GameObject peoplePrefab;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        animator = GetComponent<Animator>();
        coinPrefab = GameObject.FindGameObjectWithTag("Coin");
        //peoplePrefab = GameObject.FindGameObjectWithTag("People");
    }

    void Update()
    {
        FollowingPlayer(transform, playerTransform);
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform spawnTransform = transform;
        if (other.CompareTag("Medicine"))
        {
            transform.gameObject.SetActive(false);

            Instantiate(coinPrefab, spawnTransform.position, spawnTransform.rotation);
            //Instantiate(peoplePrefab, spawnTransform.position, spawnTransform.rotation);
        }
    }

    void FollowingPlayer(Transform zombieTransform, Transform playerTransform) {
        transform.LookAt(playerTransform.position);

        if (Vector3.Distance(zombieTransform.position, playerTransform.position) >= maxDist)
        {
            zombieTransform.position += zombieTransform.forward * velocity * Time.deltaTime;
        }

        if (Vector3.Distance(zombieTransform.position, playerTransform.position) <= minDist)
        {
            animator.Play("zombie_attack");
        }
    }
}