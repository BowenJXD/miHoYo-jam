using System;
using System.Collections.Generic;
using Unity;
using UnityEngine;

public class ReceiveColorSignalCon : ColorReceiverCon
{
    public EColor color;
    
    [HideInInspector] 
    public List<ExecutableBehaviour> trueExecutables = new();
    public List<ExecutableBehaviour> falseExecutables = new();

    protected override void Init()
    {
        base.Init();
        trueExecutables = executables;
    }

    public override void OnReceiveColorSignal(EColor receivedColor)
    {
        executables = receivedColor == color ? trueExecutables : falseExecutables;
        StartExecute();
    }
}