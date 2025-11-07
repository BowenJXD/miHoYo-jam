using System;
using System.Collections.Generic;
using Unity;
using UnityEngine;

public class ChangeColorExe : ExecutableBehaviour
{
    public List<EColor> colors;
    public ColorState colorState;
    int _index = 0;

    private void Start()
    {
        if (!colorState) colorState = GetComponent<ColorState>();
    }

    protected override void OnStart()
    {
        base.OnStart();
        if (colors.Count == 0) return;
        var color = colors[_index % colors.Count];
        colorState.color = color;
        _index++;
    }
}