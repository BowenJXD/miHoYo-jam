using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using Unity;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MoveBehaviour
{
    [FormerlySerializedAs("rhythmicControl")] public RhythmicControl rc;
    public int delay = 3;
    Vector3 destination;
    public Animator ani;
    [ShowInInspector] [ReadOnly] private int delayCount;
    [ShowInInspector] [ReadOnly] private int movedIndex = 0;
    public Rigidbody rb;
    public GameObject deathScreen;

    private bool canMove = true;

    private void Start()
    {
        if (!rc) rc = FindFirstObjectByType<RhythmicControl>();
        if (!ani) ani = GetComponent<Animator>();
        if (!rb) rb = GetComponent<Rigidbody>();
        if (!deathScreen)
        {
            deathScreen = FindAnyObjectByType<DeathScreen>(FindObjectsInactive.Include)?.gameObject;
        }
        delayCount = -delay - 1;
    }

    private void OnEnable()
    {
        RhythmManager.Instance.OnBeat += Trigger;
        if (delayCount > 0)
        {
            movedIndex = rc.GetMovedPositionCount() - 1 - delay;
            transform.position = rc.GetHistoryPosition(movedIndex);
            movedIndex++;
        }
    }

    private void OnDisable()
    {
        RhythmManager.Instance.OnBeat -= Trigger;
    }

    public void Trigger(int beat)
    {
        if (delayCount <= 0)
        {
            if (delayCount == 0)
            {
                movedIndex = rc.GetMovedPositionCount() - 1 - delay;
                transform.position = rc.GetHistoryPosition(movedIndex);
                movedIndex++;
            }
            delayCount++;
            return;
        }
        if (!canMove) return;

        SetDestination();
        Vector3 diff = destination - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(diff, Vector3.up);
        transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
        if (destination != Vector3.zero)
        {
            Move();
        }
        movedIndex++;
    }

    public bool TryMove(out Collider col)
    {
        // Use raycast to check if the path is clear
        RaycastHit hit;
        col = null;
        if (Physics.Raycast(transform.position, new Vector3(destination.x, 0, destination.y).normalized, out hit,
                rc.distance * 1.25f))
        {
            col = hit.collider;
            if (!col.isTrigger && !col.CompareTag("Player"))
            {
                Debug.Log("Movement blocked by " + hit.collider.name);
                return false;
            }
        }

        return true;
    }

    public override void Move()
    {
        ani?.Play("Jump");
        //change facing rotation (z only)
        if (rb)
        {
            rb
                .DOMove(destination, rc.moveDuration)
                .SetEase(rc.ease);
        }
        else
        {
            transform
                .DOMove(destination, rc.moveDuration)
                .SetEase(rc.ease);
        }
    }

    public override void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    public Vector3 SetDestination()
    {
        Vector3 diff = rc.GetHistoryPosition(movedIndex - 1);
        destination = new Vector3(diff.x, rc.yValue, diff.z);
        return destination;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            deathScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }
}