using System;
using System.Collections.Generic;
using System.Linq;
using QFramework;
using Sirenix.OdinInspector;
using Unity;
using UnityEngine;
using UnityEngine.InputSystem;

public class CommandManager : MonoSingleton<CommandManager>
{
    public List<ReceiverCon> receivers;
    
    [ReadOnly] public List<ReceiverCon> runningReceivers = new List<ReceiverCon>();

    public bool isLocked = false;

    private void Update()
    {
        if (Input.anyKeyDown && !isLocked)
        {
            Do();
        }
    }

    public void Do()
    {
        isLocked = true;
        runningReceivers = receivers.ToList();
        foreach (var receiver in receivers)
        {
            receiver.Do();
            receiver.OnFinish += () =>
            {
                runningReceivers.Remove(receiver);
                if (runningReceivers.Count == 0)
                {
                    isLocked = false;
                }
            };
        }
    }
}