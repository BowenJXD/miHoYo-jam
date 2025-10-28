﻿using DG.Tweening;
using UnityEngine;

public class DoTweenScale3Behaviour : DoTweenBehaviour
{
    public Vector3 targetScale;
    
    protected override void SetUpTween()
    {
        tween.Append(target.DOScale(targetScale, duration).SetEase(ease));
    }
}