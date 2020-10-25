using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    private Vector3 position = new Vector3(257, 0, 347);
    public GameObject playerPosition;
    private NavMeshAgent zombieAgent;
   
    void Start()
    {
        StartCoroutine(EnemySpawner());
        zombieAgent = GetComponent<NavMeshAgent>();
        
    }

    IEnumerator EnemySpawner()
    {
        while (true)
        {
            GameObject zombieSpawned = Instantiate(zombiePrefab, position + new Vector3(1, 9, 1), Quaternion.identity);
            //NavMeshAgent zombieAgent = zombieSpawned.AddComponent<NavMeshAgent>();
            //zombieAgent.SetDestination(playerPosition.transform.position);
           
            yield return new WaitForSeconds(15);
        }
    }
    float timer = 0f;

    void Update()
    {
        if (timer <= 15f)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            Instantiate(zombiePrefab, position + new Vector3(1, 9, 1), new Quaternion());
        }
    }
}
