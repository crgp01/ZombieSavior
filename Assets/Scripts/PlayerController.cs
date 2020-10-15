using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            animator.Play("Pickup");
            other.gameObject.SetActive(false);
            scoreManager.recolectedCoins++;
           
        }
        if (other.CompareTag("Gun"))
        {
            animator.Play("Pickup");
            other.gameObject.SetActive(false);
            scoreManager.weapon1Collected = true;
        }
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            animator.Play("Wave");
        }
    }
     
}
