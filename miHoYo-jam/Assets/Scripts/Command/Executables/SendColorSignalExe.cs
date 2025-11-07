using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity;
using UnityEngine;

public abstract class ColorReceiverCon : ConditionBehaviour
{
    public abstract void OnReceiveColorSignal(EColor color);
}

public class SendColorSignalExe : ExecutableBehaviour
{
    public List<EColor> color;

    public List<ColorReceiverCon> receivers = new();

    [ShowInInspector] [ReadOnly] private int _index = 0;

    protected override void OnStart()
    {
        base.OnStart();
        foreach (var receiver in receivers)
        {
            receiver.OnReceiveColorSignal(color[_index]);
        }

        _index = (_index + 1) % color.Count;
    }
}