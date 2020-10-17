using UnityEngine.Rendering.PostProcessing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosprocesingController : MonoBehaviour
{
    public PostProcessVolume volume;
    private ColorGrading colorgrading = null;

    // Start is called before the first frame update
    void Start()
    {
        volume.profile.TryGetSettings(out colorgrading);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableColorGrading(bool enabled)
    {
        colorgrading.enabled.value = enabled;
    }
}
