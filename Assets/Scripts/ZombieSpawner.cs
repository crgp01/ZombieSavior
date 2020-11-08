using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public GameObject position;
    public GameObject position2;
    public GameObject playerPosition;
    private NavMeshAgent zombieAgent;
    static MonoBehaviour instance;

    void Start()
    {
        zombieAgent = GetComponent<NavMeshAgent>();
        instance = this;

    }

    IEnumerator EnemySpawner()
    {
        while (true)
        {
            Instantiate(zombiePrefab, position.transform.position + new Vector3(1, 9, 1), Quaternion.identity);
            Instantiate(zombiePrefab, position2.transform.position + new Vector3(1, 9, 1), Quaternion.identity);
           
            yield return new WaitForSeconds(15);
        }
    }

    public void StartZombieSpawner() {
        instance.StartCoroutine(((ZombieSpawner)instance).EnemySpawner());
    }


}
