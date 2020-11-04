using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLevelGunController : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    private float bulletSpeed = 1;
    public GameObject fire;
    private ParticleSystem fireParticleSystem;
    // [SerializeField] AudioSource shootSound;

    // Update is called once per frame
    void Start()
    {
        Debug.Log("Dentro del start");
        if (fire)
        {
            fireParticleSystem = fire.GetComponent<ParticleSystem>();
        }

    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Dentro del disparo");
            GameObject newGo = Object.Instantiate(bullet);
            // shootSound.Play();

            newGo.transform.position = spawnPoint.position;
            newGo.transform.rotation = spawnPoint.rotation;

            Rigidbody bulletRB = newGo.GetComponent<Rigidbody>();

            bulletRB.AddForce(newGo.transform.forward * bulletSpeed, ForceMode.Impulse);
            fireParticleSystem.Play();

        }
    }

}
