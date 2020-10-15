using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float bulletSpeed;
    private GameObject fire;
    private ParticleSystem fireParticleSystem;
    [SerializeField] private ScoreManager scoreManager;
    // [SerializeField] AudioSource shootSound;

    // Update is called once per frame
    void Start()
    {
        fire = GameObject.FindGameObjectWithTag("fire");

        if (fire) {
            fireParticleSystem = fire.GetComponent<ParticleSystem>();
        }
       
    }

    void Update()
    {
        if (scoreManager.weapon1Collected)
        {
            if (Input.GetButtonDown("Fire1"))
            {
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
}
