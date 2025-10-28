using System;
using System.Collections.Generic;
using DG.Tweening;
using QFramework;
using Unity;
using UnityEngine;

public class RhythmManager : MonoSingleton<RhythmManager>
{
    public int bpm = 80;
    public List<AudioClip> beatSounds = new List<AudioClip>();
    public Action<int> OnBeat;
    public Action OnLoopComplete;
    public LoopTask loopTask;
    public int beatCount => beatSounds.Count;
    public bool isBeatFinal => currentBeat == beatSounds.Count - 1;
    
    int currentBeat = 0;
    float interval;

    private void Start()
    {
        interval = 60f / bpm;
        loopTask = new LoopTask()
        {
            interval = interval,
            loop = -1,
            loopAction = Beat
        };
        loopTask.Start();
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