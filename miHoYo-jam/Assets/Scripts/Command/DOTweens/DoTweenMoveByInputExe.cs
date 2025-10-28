using System;
using DG.Tweening;
using QFramework;
using Sirenix.OdinInspector;
using Unity;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoTweenMoveByInputExe : DoTweenBehaviour
{
    public float scale = 1f;
    [ShowInInspector] private Vector2 move;
    public InputActionReference moveAction;

    public override void Init()
    {
        base.Init();
        moveAction.action.performed += OnMove;
        moveAction.action.canceled += OnMove;
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        move = ctx.ReadValue<Vector2>();
        if (ctx.canceled) move = Vector2.zero;
    }
    
    protected override void SetUpTween()
    {
        if (move == Vector2.zero) return;
        Vector3 moveVector = new Vector3(move.x, 0, move.y).normalized;
        Vector3 endPos = target.position + moveVector * duration * scale;
        tween = DOTween.Sequence().Append(target.DOMove(endPos, duration).SetEase(ease));
    }
}