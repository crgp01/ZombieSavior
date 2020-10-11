using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    private int maxDist = 10;
    private int minDist = 2;
    private GameObject player;
    private Transform playerTransform;
    private Animator animator;
    private int velocity = 4;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerTransform);

        if (Vector3.Distance(transform.position, playerTransform.position) >= maxDist)

            transform.position += transform.forward * velocity * Time.deltaTime;

        if (Vector3.Distance(transform.position, playerTransform.position) <= minDist)
        {
            animator.Play("zombie_attack");
        }
    }
}