using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class FinalLevelGunController : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public Transform spawnPointShotgun;
    public float bulletSpeed = 3;
    private GameObject newGo;
    private Rigidbody bulletRB;
    public GameObject fire;
    public GameObject fireShotgun;
    public int bulletNumber = 3;
    private ParticleSystem fireParticleSystem;
    private ParticleSystem fireParticleSystemShotgun;
    public FinalLevelController finalLevelController;
    // [SerializeField] AudioSource shootSound;

    // Update is called once per frame
    void Start()
    {
        if (fire)
        {
            fireParticleSystem = fire.GetComponent<ParticleSystem>();
        }

    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && finalLevelController.canShoot)
        {
            FindObjectOfType<AudioManager>().Play("Shoot");
            if (finalLevelController.shotgunEquiped)
            {
                for (int i = 1; i < bulletNumber; i++)
                {
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
