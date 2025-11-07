using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity;
using UnityEngine;
using UnityEngine.Serialization;

public class DoMoveBetweenExe : DoTweenBehaviour
{
    public List<Vector3> positions;
    
    public bool isRelative = true;
    
    int _currentIndex = 0;

    protected override void OnStart()
    {
        base.OnStart();
        if (blackboard.TryGet(BBKey.TARGET, out Transform bbTarget))
        {
            target = bbTarget.transform;
        }
    }

    protected override void SetUpTween()
    {
        base.SetUpTween();
        Vector3 destination = positions[_currentIndex];
        if (isRelative) destination += target.position;
        tween = DOTween.Sequence().Append(target.transform.DOMove(destination, duration).SetEase(ease));
        _currentIndex = (_currentIndex + 1) % positions.Count;
    }
}