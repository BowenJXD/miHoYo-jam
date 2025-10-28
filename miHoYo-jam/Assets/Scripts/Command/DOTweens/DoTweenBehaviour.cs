using DG.Tweening;
using UnityEngine;

/// <summary>
///  DoTween行为节点
/// </summary>
public class DoTweenBehaviour : ExecutableBehaviour
{
    public Ease ease = Ease.Linear;
    public float duration;
    public Sequence tween;
    public Transform target;

    public override void Init()
    {
        base.Init();

        if (blackboard != null && blackboard.TryGet(BBKey.ENTITY, out Entity bbEntity))
        {
            target = bbEntity.transform;
        }
        if (!target) target = transform;
        if (blackboard != null && blackboard.TryGet(BBKey.DURATION, out float bbDuration))
        {
            duration = bbDuration;
        }
    }

    protected override void OnStart()
    {
        base.OnStart();
        if (!skip) StartExe();
        tween = DOTween.Sequence();
        SetUpTween();
        if (!skip) tween.OnComplete(FinishExe);
        tween.Play();
    }

    protected virtual void SetUpTween()
    {
    }

    public override void Deinit()
    {
        base.Deinit();
        tween?.Kill();
    }
}