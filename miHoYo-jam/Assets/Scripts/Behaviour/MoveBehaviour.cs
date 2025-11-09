using System;
using Unity;
using UnityEngine;


public abstract class MoveBehaviour : MonoBehaviour
{
    public abstract void Move();

    public abstract void SetCanMove(bool canMove);
}
