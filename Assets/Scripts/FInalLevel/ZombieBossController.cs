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
    public FinalLevelPanelController panelController;
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
        if (hitCounter == 2 && spawnZombie)
        {
            Debug.Log("Dentro del hit");
            Instantiate(zombiePrefab, transform.position + new Vector3(1, 1, 1), Quaternion.identity);
            spawnZombie = false;
        }
        FollowingPlayer(transform, playerTransform);
        if (zombieLifeSlider.value == 0)
        {
            panelController.PauseGame();
            panelController.finalGamePanel.SetActive(true);
            panelController.mainPanel.gameObject.SetActive(false);
            panelController.playerPanel.gameObject.SetActive(false);
        }
    }
    void Update() {
        
    }
    void OnCollisionEnter(Collision col)
    {
         if (col.gameObject.tag == "Medicine")
        {
            hitCounter++;
            zombieLifeSlider.value -= 1;
            animator.Play("zombie_death_standing");
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
    }
}

