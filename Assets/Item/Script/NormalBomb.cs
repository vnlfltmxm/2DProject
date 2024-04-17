using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBomb : BaseState<Bomb>
{

    public NormalBomb(Bomb bomb) : base(bomb)
    {

    }

    public override void OnEnterState()
    {

    }
    public  override void OnUpdateState()
    {
    }
    public  override void OnFixedUpdateState()
    {
        Controller.rigid.AddForce(GameManger.Instance.wind * Time.deltaTime);

    }
    public  override void OnExitState()
    {

    }
}
