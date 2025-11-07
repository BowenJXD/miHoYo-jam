using DG.Tweening;
using UnityEngine;

public class DoScale3Exe : DoTweenBehaviour
{
    public Vector3 targetScale;
    
    protected override void SetUpTween()
    {
        tween.Append(target.DOScale(targetScale, duration).SetEase(ease));
    }
}