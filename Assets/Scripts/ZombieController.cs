using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    private int maxDist = 2;
    private int minDist = 2;
    private GameObject player;
    private Transform playerTransform;
    private Animator animator;
    private GameObject coinPrefab;
    private NavMeshAgent zombieAgent;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        animator = GetComponent<Animator>();
        coinPrefab = GameObject.FindGameObjectWithTag("Coin");
        zombieAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        FollowingPlayer(transform, playerTransform);       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Medicine"))
        {
            //animator.Play("zombie_death_standing");
            Invoke("KillZombie", 1);
        }
    }

    private void KillZombie() {
        Transform spawnTransform = transform;
        transform.gameObject.SetActive(false);
        Instantiate(coinPrefab, spawnTransform.position, spawnTransform.rotation);
    }

    void FollowingPlayer(Transform zombieTransform, Transform playerTransform) {
        transform.LookAt(playerTransform.position);

        if (Vector3.Distance(zombieTransform.position, playerTransform.position) >= maxDist)
        {
            //animator.Play("zombie_walk_forward");
            zombieAgent.SetDestination(playerTransform.position);
            //zombieTransform.position += zombieTransform.forward * velocity * Time.deltaTime;
        }

        if (Vector3.Distance(zombieTransform.position, playerTransform.position) <= minDist)
        {
            animator.Play("zombie_attack");
        }
    }
}