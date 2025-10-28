﻿using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
///  DoTween移动节点
/// </summary>
public class DoTweenMoveToBehaviour : DoTweenBehaviour
{
    [HideIf("randomLocation")]
    public Vector3 moveVector;
    [HideIf("randomLocation")]
    public float speed = 1;
    public bool randomLocation = false;
    [ShowIf("randomLocation")]
    public Vector2 range = new Vector2(0, 0);

    public Animator ani;
    public bool isRunning = false;
    public SpriteRenderer sprite;
    public bool flipSpriteX = false;

    protected override void OnStart()
    {
        if (blackboard.TryGet(BBKey.TARGET, out Transform target))
        {
            moveVector = (target.position - this.target.position).normalized;
        }
        base.OnStart();
    }

    protected override void SetUpTween()
    {
        Vector3 endPos = randomLocation 
            ? new Vector2(Random.Range(-range.x, range.x), Random.Range(-range.y, range.y)) 
            : target.position + moveVector * speed;
        tween = DOTween.Sequence().Append(target.DOMove(endPos, duration).SetEase(ease));
        if (ani)
        {
            ani.SetFloat("moveX", moveVector.x);
            ani.SetFloat("moveY", moveVector.y);
            ani.SetBool(isRunning ? "isRunning" : "isMoving", true);
        }
        if (sprite)
        {
            sprite.flipX = flipSpriteX ? moveVector.x < 0 : sprite.flipX;
        }
    }

    public override void FinishExe()
    {
        base.FinishExe();
        if (ani)
        {
            ani.SetBool(isRunning ? "isRunning" : "isMoving", false);
        }
    }
}