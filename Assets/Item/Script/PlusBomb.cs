using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusBomb : BaseState<Bomb>
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
    }
    public override void OnFixedUpdateState()
    {
        Controller.rigid.AddForce(GameManger.Instance.wind * Time.deltaTime);

    }
    public override void OnExitState()
    {
        ReThrow();
        Controller.Parent.GetComponent<PlayerController>().ItemUse(BombStateName.Plus);
    }

    public void ReThrow()
    {
        playerAnimator.SetTrigger("ThrowReady");
        playerAnimator.SetTrigger("Re");
        playerAnimator.SetTrigger("ThrowTr");
        playerAnimator.ResetTrigger("Re");

    }
}
