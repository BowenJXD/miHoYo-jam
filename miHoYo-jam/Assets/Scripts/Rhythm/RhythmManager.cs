using System;
using System.Collections.Generic;
using DG.Tweening;
using QFramework;
using Sirenix.OdinInspector;
using Unity;
using UnityEngine;
using UnityEngine.Serialization;

public class RhythmManager : MonoSingleton<RhythmManager>
{
    public int bpm = 80;
    public List<AudioClip> beatSounds = new List<AudioClip>();
    public Action<int> OnBeat;
    public Action OnLoopComplete;
    
    [ReadOnly] public float timeSinceLastBeat = 0f;
    [ReadOnly] public float interval;
    [ReadOnly] public int currentBeat = 0;
    
    public int BeatCount => beatSounds.Count;
    public bool IsBeatFinal => currentBeat == BeatCount - 1;

    private void Start()
    {
        interval = 60f / bpm;
    }

    private void Update()
    {
        timeSinceLastBeat += Time.deltaTime;
        if (timeSinceLastBeat >= interval)
        {
            timeSinceLastBeat -= interval;
            Beat();
        }
    }

    public void Beat()
    {
        if (beatSounds.Count <= 0) return;
        OnBeat?.Invoke(currentBeat);
        AudioClip clip = beatSounds[currentBeat];
        AudioSource.PlayClipAtPoint(clip, Vector3.zero);
        currentBeat++;
        if (currentBeat >= beatSounds.Count)
        {
            currentBeat -= beatSounds.Count;
            OnLoopComplete?.Invoke();
        }
    }
}