using System;
using System.Collections;
using System.Collections.Generic;
using Unity;
using Unity.Collections;
using UnityEngine;

public class SelectByColorCon : ColorReceiverCon
{
    public SerializableDictionary<EColor, ExecutableBehaviour> colorExes;
    
    public void Select(EColor color)
    {
        if (colorExes.TryGetValue(color, out var exe))
        {
            executables = new List<ExecutableBehaviour> { exe };
        }
        else
        {
            executables = new List<ExecutableBehaviour>();
        }
    }

    public override void OnReceiveColorSignal(EColor color)
    {
        Select(color);
    }
}