using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusBomb : BombBaseState
{
    Animator playerAnimator;
    public PlusBomb(Bomb bomb) : base(bomb)
    {
        playerAnimator=bomb.Parent.GetComponent<Animator>();
    }

    public override void OnEnterState()
    {

    }
    public override void OnUpdateState()
    {
        bomb.rigid.AddForce(GameManger.Instance.wind * Time.deltaTime);
    }
    public override void OnFixedUpdateState()
    {

    }
    public override void OnExitState()
    {
        ReThrow();
    }

    public void ReThrow()
    {
        playerAnimator.SetTrigger("ThrowReady");
        playerAnimator.SetTrigger("Re");
        playerAnimator.SetTrigger("ThrowTr");
        playerAnimator.ResetTrigger("Re");

    }
}
