using System;
using System.Collections.Generic;
using Unity;
using UnityEngine;

public class LightSpike : MonoBehaviour
{
    public EColor color;
    public List<ColorfulBehaviour> conditions;
    public bool anyCondition = false;
    public Animator spikeAni;
    public Collider spikeCollider;
    public LoopTask loopTask;

    private void Awake()
    {
        if (!spikeAni) spikeAni = GetComponent<Animator>();
        if (!spikeCollider) spikeCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        bool conditionMet = CheckCondition();
        if (conditionMet)
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

    public bool CheckCondition()
    {
        if (anyCondition)
        {
            foreach (var condition in conditions)
            {
                if (condition.GetColor() == color)
                {
                    return true;
                }
            }

            return false;
        }
        else
        {
            foreach (var condition in conditions)
            {
                if (condition.GetColor() != color)
                {
                    return false;
                }
            }

            return true;
        }
    }
}