using System;
using Unity;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class RhythmVignette : MonoBehaviour
{
    public float fadeSpeed = 0.5f;
    public float maxIntensity = 0.5f;
    
    Vignette vignette;
    float fadeVelocity;
    
    void Start()
    {
        Volume volumeComp = GetComponent<Volume>();
        VolumeProfile profile = volumeComp.sharedProfile;
        profile.TryGet(out vignette);
        RhythmManager.Instance.OnBeat += i => ShowVignette(i, maxIntensity);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (vignette.intensity.value > 0)
        {
            vignette.intensity.value -= fadeVelocity * Time.deltaTime;
            fadeVelocity += Time.deltaTime;
        }
        else
        {
            fadeVelocity = fadeSpeed;
        }
    }
    
    public void ShowVignette(int index, float intensity)
    {
        if (RhythmManager.Instance.isBeatFinal) intensity *= 1.5f;
        vignette.intensity.value = intensity;
    }
    
    private void OnDestroy()
    {
        if (vignette) vignette.intensity.value = 0;
    }
}