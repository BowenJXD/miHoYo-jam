using System;
using Unity;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class RhythmVignette : MonoBehaviour
{
    public float fadeSpeed = 0.5f;
    public float maxIntensity = 0.5f;
    
    Vignette _vignette;
    float _fadeVelocity;
    
    void Start()
    {
        Volume volumeComp = GetComponent<Volume>();
        VolumeProfile profile = volumeComp.sharedProfile;
        profile.TryGet(out _vignette);
        RhythmManager.Instance.OnBeat += i => ShowVignette(i, maxIntensity);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_vignette.intensity.value > 0)
        {
            _vignette.intensity.value -= _fadeVelocity * Time.deltaTime;
            _fadeVelocity += Time.deltaTime;
        }
        else
        {
            _fadeVelocity = fadeSpeed;
        }
    }
    
    public void ShowVignette(int index, float intensity)
    {
        if (RhythmManager.Instance.IsBeatFinal) intensity *= 1.5f;
        _vignette.intensity.value = intensity;
    }
    
    private void OnDestroy()
    {
        if (_vignette) _vignette.intensity.value = 0;
    }
}