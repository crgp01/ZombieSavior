﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ZombieController : MonoBehaviour
{
    private int maxDist = 2;
    private int minDist = 2;
    private GameObject player;
    private Transform playerTransform;
    private Animator animator;
    public GameObject coinPrefab;
    public GameObject people;
    private NavMeshAgent zombieAgent;
    private Transform peopleTransform;
    private Scene currentScene;

    //private ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        animator = GetComponent<Animator>();
        coinPrefab = GameObject.FindGameObjectWithTag("Coin");
        zombieAgent = GetComponent<NavMeshAgent>();
        peopleTransform = people.GetComponent<Transform>();
       
    }

    void Update()
    {
               FollowingPlayer(transform, playerTransform);       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Medicine")
        {
            FindObjectOfType<AudioManager>().Play("ZombieDie");
            Transform spawnTransform = transform;
           
            string sceneName = currentScene.name;

            if (sceneName != "Desert") {
                Instantiate(coinPrefab, spawnTransform.position + new Vector3(1, 8, 1), spawnTransform.rotation);
                GameObject peoplePrefab = Instantiate(people, spawnTransform.position + new Vector3(0, 1, 0), spawnTransform.rotation);
                NavMeshAgent peopleAgent = peoplePrefab.AddComponent<NavMeshAgent>();
                peopleAgent.SetDestination(new Vector3(200, 1, 190));
            }
            transform.gameObject.SetActive(false);
        }
    }

    void FollowingPlayer(Transform zombieTransform, Transform playerTransform) {
        transform.LookAt(playerTransform.position);

        if (Vector3.Distance(zombieTransform.position, playerTransform.position) >= maxDist)
        {
            zombieAgent.SetDestination(playerTransform.position);
        }
        if (Vector3.Distance(zombieTransform.position, playerTransform.position) <= minDist)
        {
            FindObjectOfType<AudioManager>().Play("ZombieAttack");
            animator.Play("zombie_attack");
        }
    }
}