using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity;
using UnityEngine;

public class LightChangeExe : ExecutableBehaviour
{
    public List<Color> colors;
    [ReadOnly] public int currentColorIndex = 0;

    public Light light;
    
    private void Start()
    {
        if (light == null) light = GetComponent<Light>();
    }
    
    protected override void OnStart()
    {
        base.OnStart();
        if (colors.Count == 0) return;
        light.color = colors[currentColorIndex];
        currentColorIndex = (currentColorIndex + 1) % colors.Count;
    }
}