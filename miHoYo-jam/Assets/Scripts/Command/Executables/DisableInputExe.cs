using System;
using Unity;
using UnityEngine;

public class DisableInputExe : ExecutableBehaviour
{
    public float duration = 1f;
    public RhythmicControl inputControl;

    private void Awake()
    {
        inputControl ??= FindObjectOfType<RhythmicControl>();
    }

    protected override void OnStart()
    {
        base.OnStart();
        inputControl.enabled = false;
        StartExe();
        new LoopTask
        {
            interval = duration, 
            finishAction = FinishExe
        }.Start();
    }

    protected override void OnFinish()
    {
        base.OnFinish();
        inputControl.enabled = true;
    }
}