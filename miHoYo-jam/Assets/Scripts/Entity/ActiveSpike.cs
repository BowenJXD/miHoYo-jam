using System;
using Unity;
using UnityEngine;

public class ActiveSpike : MonoBehaviour
{
    public Animator spikeAni;
    public Collider spikeCollider;
    
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
        }
        else
        {
            spikeAni.SetBool("isOpen", true);
            spikeCollider.enabled = true;
        }
    }
}