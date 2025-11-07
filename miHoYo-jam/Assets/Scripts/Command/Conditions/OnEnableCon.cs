using System.Collections;
using UnityEngine;

public class OnEnableCon : ConditionBehaviour
{
    public bool loop;
    
    protected override void Init()
    {
        base.Init();
        StartExecute();
    }

    public override IEnumerator Execute()
    {
        do
        {
            yield return base.Execute();
        }
        while (loop && Application.isPlaying);
    }
}