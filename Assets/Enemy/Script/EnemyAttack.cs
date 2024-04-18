using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        float targetPosX = (GameManger.Instance.player.transform.position.x + Controller.transform.position.x) / 2;
        float targetPosY = (GameManger.Instance.player.transform.position.y + Controller.transform.position.y) / 2;
        Vector2 circlie= new Vector2(targetPosX, targetPosY);
        float dirx = 0;
        if (targetPosX < Controller.transform.position.x)
        {
            dirx = -1;
        }
        else
        {
            dirx = 1;
        }
        //Controller.targetPos = new Vector2(targetPosX,targetPosY);
        float radiousX = Vector2.Distance(circlie, Controller.transform.position);
        Vector2 circlie2= new Vector2(targetPosX+(radiousX*dirx), targetPosY+radiousX);
        Controller.bombThrowPos = Controller.transform.position + (Vector3)circlie2;
        //Controller.throwPower= Vector2.Distance(circlie, Controller.transform.position);
        Controller.throwPower = Random.Range(radiousX, radiousX * 1.5f);
        //Controller.bombThrowPos = Vector3.Slerp(GameManger.Instance.player.transform.position, Controller.transform.position, 0.5f);
        //Controller.bombPos = Vector3.Slerp(targetPos, Controller.transform.position, 0.95f);

        //Controller.bombThrowPos = Controller.targetPos;
        //Controller.bombThrowPos = Vector3.Slerp(GameManger.Instance.player.transform.position, Controller.transform.position, 0.5f);
        Controller.bombPos = new Vector2(Controller.transform.position.x+dirx, Controller.transform.position.y + 3.5f);
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
