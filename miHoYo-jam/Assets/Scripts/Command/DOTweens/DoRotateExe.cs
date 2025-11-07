using System;
using DG.Tweening;
using Unity;
using UnityEngine;

public class DoRotateExe : DoTweenBehaviour
{
    public Vector3 rotationAmount;
    
    protected override void SetUpTween()
    {
        base.SetUpTween();
        tween = DOTween.Sequence().Append(target.DORotate(target.eulerAngles + rotationAmount, duration).SetEase(ease));
    }
}