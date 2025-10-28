using System;
using Unity;
using UnityEngine;

public interface IReceiver
{
    public void Do();
}

public class ReceiverCon : ConditionBehaviour, IReceiver
{
    public void Do()
    {
        StartCoroutine(Execute());
    }
}