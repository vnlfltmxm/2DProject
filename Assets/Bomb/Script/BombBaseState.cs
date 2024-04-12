using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BombBaseState 
{
    protected Bomb bomb { get; set; }

    public BombBaseState(Bomb bomb)
    {
        this.bomb = bomb;
    }

    public abstract void OnEnterState();
    public abstract void OnUpdateState();
    public abstract void OnFixedUpdateState();
    public abstract void OnExitState();
}
