﻿using DG.Tweening;

/// <summary>
/// Delay a task for a certain amount of time and certain amount of loops.
/// Based on DOTween.
/// </summary>
public class LoopTask
{
    public float interval;
    
    public int loop = 1;
    
    public System.Action loopAction;
    
    public System.Action finishAction;
    
    public bool isActive => sequence != null;
    
    public bool isPlaying = false;
    
    Sequence sequence;

    public void Start(bool playImmediately = true)
    {
        sequence = DOTween.Sequence();
        sequence.AppendInterval(interval);
        sequence.OnStepComplete(FinishLoop);
        sequence.OnComplete(() =>
        {
            finishAction?.Invoke();
            sequence = null;
            isPlaying = false;
        });
        sequence.SetLoops(loop);
        if (playImmediately)
        {
            sequence.Play();
            isPlaying = true;
        }
        else
        {
            sequence.Pause();
        }
    }

    public void Pause()
    {
        if (sequence != null && sequence.IsPlaying())
        {
            sequence.Pause();
            isPlaying = false;
        }
    }

    public void Resume()
    {
        if (sequence != null && !sequence.IsPlaying())
        {
            sequence.Play();
            isPlaying = true;
        }
    }
    
    public void Stop()
    {
        if (sequence != null && sequence.IsPlaying())
        {
            sequence.Kill();
            sequence = null;
            isPlaying = false;
        }
    }

    public void FinishLoop()
    {
        loopAction?.Invoke();
    }
}