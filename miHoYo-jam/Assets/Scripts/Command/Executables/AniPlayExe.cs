using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class AniPlayExe : ExecutableBehaviour
{
    public Animator ani;
    public List<string> stateNames;

    [ShowInInspector][ReadOnly] private int _index;
    private float _duration;

    public override void SetUp()
    {
        base.SetUp();
        if (ani == null)
        {
            ani = GetComponent<Animator>();
        }
    }

    protected override void OnStart()
    {
        base.OnStart();
        ani.Play(stateNames[_index]);
        _index++;
        _index %= stateNames.Count;
        _duration = ani.GetCurrentAnimatorStateInfo(0).length;
        if (!skip)
        {
            executing = true;
            Invoke(nameof(FinishExe), _duration);
        }
    }
}