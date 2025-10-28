using UnityEngine;

public class TargetExe : ExecutableBehaviour
{
    public Transform target;

    public override void Init()
    {
        base.Init();
    }

    protected override void OnStart()
    {
        base.OnStart();
        blackboard.Set(BBKey.TARGET, target);
    }
}