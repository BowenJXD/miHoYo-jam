using System;
using Unity;
using UnityEngine;

public class SpikeCollider : MonoBehaviour
{
    public float duration = 1.5f;
    
    private void OnTriggerEnter(Collider other)
    {
        MoveBehaviour moveBehaviour = other.GetComponent<MoveBehaviour>();
        if (moveBehaviour)
        {
            moveBehaviour.SetCanMove(false);
        }
        Animator animator = other.GetComponent<Animator>();
        if (animator)
        {
            animator.Play("Faint");
        }

        new LoopTask
        {
            interval = duration,
            finishAction = () =>
            {
                if (moveBehaviour)
                {
                    moveBehaviour.SetCanMove(true);
                }
            }
        }.Start();
    }
}