using System;
using System.Collections.Generic;
using Unity;
using UnityEngine;

public class LightSpike : MonoBehaviour
{
    public EColor color;
    public List<ColorState> conditions;
    public bool anyCondition = false;
    public Animator spikeAni;
    public Collider spikeCollider;

    private void Awake()
    {
        if (!spikeAni) spikeAni = GetComponent<Animator>();
        if (!spikeCollider) spikeCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        bool conditionMet = checkCondition();
        spikeAni.SetBool("isOpen", !conditionMet);
        spikeCollider.enabled = !conditionMet;
    }
    
    public bool checkCondition()
    {
        if (anyCondition)
        {
            foreach (var condition in conditions)
            {
                if (condition.color == color)
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
                if (condition.color != color)
                {
                    return false;
                }
            }
            return true;
        }
    }
}