using System;
using Unity;
using UnityEngine;

public class ActiveSpike : MonoBehaviour
{
    public Animator spikeAni;
    public Collider spikeCollider;
    
    LoopTask loopTask;
    
    private void Awake()
    {
        if (!spikeAni) spikeAni = GetComponent<Animator>();
        if (!spikeCollider) spikeCollider = GetComponent<Collider>();
        RhythmManager.Instance.OnLoopComplete += TriggerSpike;
    }

    public void TriggerSpike()
    {
        if (spikeAni.GetBool("isOpen"))
        {
            spikeAni.SetBool("isOpen", false);
            spikeCollider.enabled = false;
            if (loopTask is { isPlaying: true })
            {
                loopTask.Stop();
            }
            loopTask = null;
        }
        else
        {
            spikeAni.SetBool("isOpen", true);
            if (loopTask == null)
            {
                loopTask = new LoopTask
                {
                    interval = 0.2f,
                    finishAction = () => { spikeCollider.enabled = true; }
                };
                loopTask.Start();
            }
        }
    }
}