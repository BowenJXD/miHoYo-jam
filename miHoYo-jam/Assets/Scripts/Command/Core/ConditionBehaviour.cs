using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Contains a list of <see cref="ExecutableBehaviour"/> that will be executed in order, when the condition is met.
/// </summary>
public abstract class ConditionBehaviour : MonoBehaviour
{
    [HorizontalGroup] [Tooltip("If true, will not start another execution until the current one is finished.")]
    public bool lockOnExecuting = false;
    [HorizontalGroup] public bool log = false;
    public List<ExecutableBehaviour> executables;
    [ReadOnly] public ExecutableBehaviour currentExecutable;

    public Blackboard blackboard;
    bool _isExecuting = false;

    /// <summary>
    /// Reset on trigger
    /// </summary>
    public Action OnFinish;

    public void StartExecute()
    {
        if (lockOnExecuting && _isExecuting)
        {
            if (log) Debug.Log($"[ConditionBehaviour] {name} is already executing. Ignoring new execution request.");
            return;
        }
        StartCoroutine(Execute());
        if (log) Debug.Log($"[ConditionBehaviour] {name} started execution.");
    }
    
    public virtual IEnumerator Execute()
    {
        _isExecuting = true;
        foreach (var executable in executables)
        {
            currentExecutable = executable;
            if (executable.enabled) yield return executable.Execute(blackboard);
        }
        OnFinish?.Invoke();
        OnFinish = null;
        _isExecuting = false;
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