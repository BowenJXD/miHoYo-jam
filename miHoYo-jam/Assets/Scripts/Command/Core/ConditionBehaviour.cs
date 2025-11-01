using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// Contains a list of <see cref="ExecutableBehaviour"/> that will be executed in order, when the condition is met.
/// </summary>
public abstract class ConditionBehaviour : MonoBehaviour
{
    public List<ExecutableBehaviour> executables;
    [ReadOnly] public ExecutableBehaviour currentExecutable;

    public Blackboard blackboard;

    /// <summary>
    /// Reset on trigger
    /// </summary>
    public Action OnFinish;
    
    public virtual IEnumerator Execute()
    {
        foreach (var executable in executables)
        {
            currentExecutable = executable;
            if (executable.enabled) yield return executable.Execute(blackboard);
        }
        OnFinish?.Invoke();
        OnFinish = null;
    }

    public bool IsSet { get; set; }
    public virtual void SetUp()
    {
        IsSet = true;
        blackboard = new();
        blackboard.Set(BBKey.CON, this);
        if (executables == null || executables.Count == 0)
        {
            executables = new ();
            executables.AddRange(GetComponents<ExecutableBehaviour>());
        }
        executables.ForEach(e => e.Init());
    }
    
    private void OnEnable()
    {
        if (!IsSet) SetUp();
        Init();
    }

    private void OnDisable()
    {
        Deinit();
    }

    protected virtual void Init() { }

    protected virtual void Deinit() { }
}