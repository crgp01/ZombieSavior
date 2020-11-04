using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform spawnPointShotgun;
    [SerializeField] float bulletSpeed;
    private GameObject newGo;
    private Rigidbody bulletRB;
    public GameObject fire;
    public GameObject fireShotgun;
    public int bulletNumber = 3;
    private ParticleSystem fireParticleSystem;
    private ParticleSystem fireParticleSystemShotgun;
    [SerializeField] private ScoreManager scoreManager;
    // [SerializeField] AudioSource shootSound;

    // Update is called once per frame
    void Start()
    {
        if (fire)
        {
            fireParticleSystem = fire.GetComponent<ParticleSystem>();
        }
        if (fireShotgun)
        {
            fireParticleSystemShotgun = fireShotgun.GetComponent<ParticleSystem>();
        }

    }

    void Update()
    {
        if (scoreManager.weapon1Collected)
        {
            if (Input.GetButtonDown("Fire1") && scoreManager.canShoot)
            {
                // shootSound.Play();
                if (scoreManager.shotgunEquiped) {
                    for (int i = 1; i < bulletNumber; i++) {
                        newGo = Object.Instantiate(bullet);

                        newGo.transform.position = spawnPointShotgun.position;
                        newGo.transform.rotation = spawnPointShotgun.rotation;

                        bulletRB = newGo.GetComponent<Rigidbody>();

                        bulletRB.AddForce(newGo.transform.forward * bulletSpeed, ForceMode.Impulse);
                        fireParticleSystemShotgun.Play();
                    }
                }
                newGo = Object.Instantiate(bullet);

                newGo.transform.position = spawnPoint.position;
                newGo.transform.rotation = spawnPoint.rotation;

                bulletRB = newGo.GetComponent<Rigidbody>();

                bulletRB.AddForce(newGo.transform.forward * bulletSpeed, ForceMode.Impulse);
                fireParticleSystem.Play();

            }
        }
    }
}
