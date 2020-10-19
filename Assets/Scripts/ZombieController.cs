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
    public GameObject people;
    private NavMeshAgent zombieAgent;
    private Transform peopleTransform;

    [SerializeField] private ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Medicine"))
        {
            //animator.Play("zombie_death_standing");
            Transform spawnTransform = transform;
            transform.gameObject.SetActive(false);
            Instantiate(coinPrefab, spawnTransform.position + new Vector3(0, 8, 0), spawnTransform.rotation);
            GameObject peoplePrefab = Instantiate(people, spawnTransform.position + new Vector3(0, 1, 0), spawnTransform.rotation);

            NavMeshAgent peopleAgent = peoplePrefab.AddComponent<NavMeshAgent>();
            peopleAgent.SetDestination(new Vector3(200, 1, 190));

            Animator peopleAnimator = peoplePrefab.GetComponent<Animator>();
        }
    }

    void FollowingPlayer(Transform zombieTransform, Transform playerTransform) {
        transform.LookAt(playerTransform.position);

        if (Vector3.Distance(zombieTransform.position, playerTransform.position) >= maxDist)
        {
            //animator.Play("zombie_walk_forward");
            if (scoreManager.lifeBarSlider.value > 0) {
                zombieAgent.SetDestination(playerTransform.position);
            }
      
            //zombieTransform.position += zombieTransform.forward * velocity * Time.deltaTime;
        }

        if (Vector3.Distance(zombieTransform.position, playerTransform.position) <= minDist)
        {
            animator.Play("zombie_attack");
        }
    }
}