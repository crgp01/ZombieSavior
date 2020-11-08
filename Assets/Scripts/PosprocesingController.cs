using UnityEngine.Rendering.PostProcessing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosprocesingController : MonoBehaviour
{
    public PostProcessVolume volume;
    private ColorGrading colorgrading = null;
    private Vignette vignette = null;

    void Start()
    {
        volume.profile.TryGetSettings(out colorgrading);
        volume.profile.TryGetSettings(out vignette);
    }

    public void EnableColorGrading(bool enabled)
    {
        colorgrading.enabled.value = enabled;
    }
    public void EnableVignet(bool enabled)
    {
        vignette.enabled.value = enabled;
    }
}
