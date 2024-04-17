using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : BaseState<EnemyController>
{
    Animator animator;

    public EnemyAttack(EnemyController controller) : base(controller)
    {
        animator = controller.GetComponent<Animator>();
    }

    public override void OnEnterState()
    {
        float targetDis = Vector2.Distance(GameManger.Instance.player.transform.position, GameManger.Instance.player.transform.position);

        Controller.bombPos = new Vector2(Controller.dirX, Controller.transform.position.y + 4) * targetDis - GameManger.Instance.wind;
        animator.SetTrigger("Attack");
    }
    public override void OnUpdateState()
    {
    }
    public override void OnFixedUpdateState()
    {

    }
    public override void OnExitState()
    {

    }
}
