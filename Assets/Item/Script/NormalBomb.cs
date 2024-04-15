using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBomb : BombBaseState
{

    public NormalBomb(Bomb bomb) : base(bomb)
    {

    }

    public override void OnEnterState()
    {

    }
    public  override void OnUpdateState()
    {
       bomb.rigid.AddForce(GameManger.Instance.wind * Time.deltaTime);
    }
    public  override void OnFixedUpdateState()
    {

    }
    public  override void OnExitState()
    {

    }
}
