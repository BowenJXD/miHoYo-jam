using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity;
using UnityEngine;
using UnityEngine.InputSystem;

public class RhythmicControl : MonoBehaviour
{
    [Tooltip("Seconds allowed to be on-beat prior the beat (able to move)")]
    public float priorTolerance = 0.2f;

    [Tooltip("Seconds allowed to be on-beat after the beat (able to move)")]
    public float postTolerance = 0.1f;

    public float distance = 10f;
    public float moveDuration = 0.3f;
    public InputActionReference moveAction;
    public Vector2 moveVector;
    public Ease ease = Ease.OutQuad;
    public Animator ani;
    public Rigidbody rb;

    List<Vector3> _movePositions = new List<Vector3>();
    RhythmManager _rhythmManager;
    bool _movedThisBeat = false;

    private void Awake()
    {
        if (moveAction == null)
        {
            Debug.LogError("Move Action is not assigned.");
            return;
        }

        if (!ani) ani = GetComponent<Animator>();
        if (!rb) rb = GetComponent<Rigidbody>();

        moveAction.action.performed += OnMove;
        moveAction.action.canceled += OnMove;
    }

    private void Start()
    {
        _rhythmManager = RhythmManager.Instance;
    }

    private void OnMove(InputAction.CallbackContext obj)
    {
        moveVector = obj.ReadValue<Vector2>();
        if (obj.canceled) moveVector = Vector2.zero;
        if (moveVector.x > moveVector.y)
        {
            if (moveVector.x > -moveVector.y)
            {
                moveVector = Vector2.right;
            }
            else
            {
                moveVector = Vector2.down;
            }
        }
        else if (moveVector.x < moveVector.y)
        {
            if (moveVector.x < -moveVector.y)
            {
                moveVector = Vector2.left;
            }
            else
            {
                moveVector = Vector2.up;
            }
        }
        else
        {
            moveVector = Vector2.zero;
        }
    }

    private void Update()
    {
        if (!_rhythmManager) return;
        if (IsOnBeat() && moveVector != Vector2.zero && !_movedThisBeat)
        {
            Vector3 targetDirection = new Vector3(moveVector.x, 0, moveVector.y).normalized;
            if (targetDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
                transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
            }

            _movedThisBeat = true;
            if (TryMove(out Collider col))
            {
                Move();
            }
            else
            {
                if (col.CompareTag("Interactable"))
                {
                    col.GetComponent<RhythmCon>()?.Trigger();
                    ani.Play("Interact");
                }
            }

            new LoopTask { interval = moveDuration, finishAction = () => _movedThisBeat = false }.Start(this);

            /*if (_rhythmManager.timeSinceLastBeat <= postTolerance)
            {
                Debug.Log("Moved after the beat by " + _rhythmManager.timeSinceLastBeat + " seconds.");
            }

            if (_rhythmManager.timeSinceLastBeat >= _rhythmManager.interval - priorTolerance)
            {
                Debug.Log("Moved before the beat by " + (_rhythmManager.interval - _rhythmManager.timeSinceLastBeat) +
                          " seconds.");
            }*/
        }
    }

    public bool TryMove(out Collider col)
    {
        // Use raycast to check if the path is clear
        RaycastHit hit;
        col = null;
        if (Physics.Raycast(transform.position, new Vector3(moveVector.x, 0, moveVector.y).normalized, out hit,
                distance * 1.25f))
        {
            col = hit.collider;
            if (!col.isTrigger)
            {
                Debug.Log("Movement blocked by " + hit.collider.name);
                return false;
            }
        }

        return true;
    }

    public void Move()
    {
        ani?.Play("Jump");
        //change facing rotation (z only)
        Vector3 targetDirection = new Vector3(moveVector.x, 0, moveVector.y).normalized;
        Vector3 destination = transform.position + targetDirection * distance;
        destination.y = 0; // keep y level
        if (rb)
        {
            rb
                .DOMove(destination, moveDuration)
                .SetEase(ease)
                .OnComplete(() => { _movePositions.Add(transform.position); });
        }
        else
        {
            transform
                .DOMove(destination, moveDuration)
                .SetEase(ease)
                .OnComplete(() => { _movePositions.Add(transform.position); });
        }
    }

    bool IsOnBeat()
    {
        return _rhythmManager.timeSinceLastBeat <= postTolerance ||
               _rhythmManager.timeSinceLastBeat >= _rhythmManager.interval - priorTolerance;
    }
}