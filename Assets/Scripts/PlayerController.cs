using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
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
            other.gameObject.SetActive(false);
            scoreManager.recolectedCoins++;
        }
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.F))
        {
            animator.Play("Pickup");
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            animator.Play("Wave");
        }
    }

}
