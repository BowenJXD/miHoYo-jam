using System;
using System.Collections.Generic;
using Unity;
using Unity.Collections;
using UnityEngine;

public class LightCone : ColorfulBehaviour
{
    public List<EColor> colors;
    int _index = 0;
    
    public Light light = null;
    
    private void Start()
    {
        if (!light) light = GetComponent<Light>();
        RhythmManager.Instance.OnLoopComplete += Trigger;
    }

    private void Trigger()
    {
        SwitchColor();
    }

    private void SwitchColor()
    {
        _index = (_index + 1) % colors.Count;
        light.color = ColorManager.Instance.GetColor(GetColor());
    }

    public override EColor GetColor()
    {
        if (colors.Count == 0) return 0;
        return colors[_index % colors.Count];
    }
}