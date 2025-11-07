using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EnabledExe : ExecutableBehaviour
{
    public List<Behaviour> components;
    public List<Collider> colliders;

    protected override void OnStart()
    {
        base.OnStart();
        components.ForEach(c => c.enabled = true);
        colliders.ForEach(c => c.enabled = true);
    }
}