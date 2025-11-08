using System;
using Unity;
using UnityEngine;

public class RhythmCon : ConditionBehaviour, ITriggerable
{
    public override void SetUp()
    {
        base.SetUp();
        RhythmManager.Instance.OnLoopComplete += Trigger;
    }

    public void Trigger()
    {
        StartExecute();
    }
}