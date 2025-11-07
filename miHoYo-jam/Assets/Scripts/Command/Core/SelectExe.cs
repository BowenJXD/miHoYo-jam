using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public enum SelectMode {
    Random,
    Sequential,
}

public class SelectExe : ExecutableBehaviour
{
    public List<ExecutableBehaviour> executables = new List<ExecutableBehaviour>();

    [ReadOnly] public ExecutableBehaviour selectedExe;
    
    public SelectMode selectMode = SelectMode.Sequential;
    
    int sequentialIndex = 0;

    public void Select()
    {
        if (selectMode == SelectMode.Random)
        {
            selectedExe = executables[Random.Range(0, executables.Count)];
        }
        else if (selectMode == SelectMode.Sequential)
        {
            if (executables.Count == 0) return;
            selectedExe = executables[sequentialIndex];
            sequentialIndex = (sequentialIndex + 1) % executables.Count;
        }
    }
    
    public override void Init()
    {
        base.Init();
        foreach (var exe in executables)
        {
            exe.Init();
        }
    }

    protected override void OnStart()
    {
        base.OnStart();
        Select();
    }

    protected override IEnumerator Executing()
    {
        return selectedExe?.Execute(blackboard);
    }
}