using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 100), ""+1.0f / Time.smoothDeltaTime);
    }

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
  
   }
