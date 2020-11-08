using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ZombieBossController : MonoBehaviour
{
    private int maxDist = 2;
    private int minDist = 2;
    private GameObject player;
    private Transform playerTransform;
    private Animator animator;
    private GameObject coinPrefab;
    private NavMeshAgent zombieAgent;
    private Transform peopleTransform;
    public GameObject zombiePrefab;
    public Slider zombieLifeSlider;
    private int hitCounter;
    public bool spawnZombie = true;
    private int rangeDistance = 5;
    public FinalLevelPanelController panelController;
    private static float STEP_TIMER = 0.5f;
    private float timeRemaining = STEP_TIMER;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        animator = GetComponent<Animator>();
        zombieAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        FollowingPlayer(transform, playerTransform);
        if (zombieLifeSlider.value == 0)
        {
        
            //FindObjectOfType<AudioManager>().Play("ZombieBossAttack");

            panelController.PauseGame();
            panelController.finalGamePanel.SetActive(true);
            panelController.mainPanel.gameObject.SetActive(false);
            panelController.playerPanel.gameObject.SetActive(false);
            FindObjectOfType<AudioManager>().Play("ZombieBossDie");
        }
    }
    void Update() {           
        if (spawnZombie)
        {
            Instantiate(zombiePrefab, transform.position + new Vector3(1, 1, 1), Quaternion.identity);
            Instantiate(zombiePrefab, transform.position + new Vector3(1, 1, 1), Quaternion.identity);
            spawnZombie = false;
        }
    }
    void OnCollisionEnter(Collision col)
    {
         if (col.gameObject.tag == "Medicine")
        {
            hitCounter++;
            if (hitCounter % 2 == 0)
            {
                spawnZombie = true;
            }
                zombieLifeSlider.value -= 1;
            FindObjectOfType<AudioManager>().Play("ZombieBossHit");
            animator.Play("zombie_death_standing");
        }
        if (col.gameObject.tag == "Player") {
            FindObjectOfType<AudioManager>().Play("ZombieBossAttack");
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
            animator.Play("zombie_attack");
        }
        if (Vector3.Distance(zombieTransform.position, playerTransform.position) <= rangeDistance)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                FindObjectOfType<AudioManager>().Play("ZombieSteps");
                timeRemaining = STEP_TIMER;
            }
        }
    }
}

