using System;
using Unity;
using UnityEngine;

public class ColorState : ColorfulBehaviour
{
    public EColor color;
    public override EColor GetColor()
    {
        return color;
    }
}