using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Terrain")
        {
            FindObjectOfType<AudioManager>().Play("CoinFall");
        }
    }
}
