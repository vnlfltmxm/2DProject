using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyAttack : BaseState<EnemyController>
{
    Animator animator;

    public EnemyAttack(EnemyController controller) : base(controller)
    {
        animator = controller.GetComponent<Animator>();
    }

    public override void OnEnterState()
    {
        //float rd = Random.Range(0, 6);
        //float targetPosX = (GameManger.Instance.player.transform.position.x + Controller.transform.position.x) / 2;
        //float targetPosY = (GameManger.Instance.player.transform.position.y + Controller.transform.position.y) / 2;
        //Vector2 circlie= new Vector2(targetPosX, targetPosY);
        ////Controller.targetPos = new Vector2(targetPosX,targetPosY);
        ////float radiousX = Vector2.Distance(circlie, Controller.transform.position);
        //float radiousX = (circlie - (Vector2)Controller.transform.position).x;
        //Vector2 circlie2= new Vector2(targetPosX*Controller.dirX, targetPosY+(radiousX*Controller.dirX)/2);
        //Controller.bombThrowPos = /*Controller.transform.position + */(Vector3)circlie2 + new Vector3(0, rd);
        ////Controller.throwPower= Vector2.Distance(circlie, Controller.transform.position);
        //Controller.throwPower = Vector2.Distance(GameManger.Instance.player.transform.position, Controller.transform.position)/2 + GameManger.Instance.wind.x / 50;
        ////Controller.bombThrowPos = Vector3.Slerp(GameManger.Instance.player.transform.position, Controller.transform.position, 0.5f);
        ////Controller.bombPos = Vector3.Slerp(targetPos, Controller.transform.position, 0.95f);

        ////Controller.bombThrowPos = Controller.targetPos;
        ////Controller.bombThrowPos = Vector3.Slerp(GameManger.Instance.player.transform.position, Controller.transform.position, 0.5f);
        //Controller.bombPos = new Vector2(Controller.transform.position.x+Controller.dirX, Controller.transform.position.y + 3.5f);

        float f = UnityEngine.Random.Range(0.3f, 1f);
        int i= UnityEngine.Random.Range(2, 11);
        float v = Mathf.Sqrt(Controller.dirX * Controller.dirX - f * f);
        float x = (GameManger.Instance.wind.x * -1) / 50;
        Controller.bombPos = new Vector2(v * Controller.dirX, f)*4 + (Vector2)Controller.transform.position;
        Controller.bombThrowPos = Controller.bombPos - (Vector2)Controller.transform.position;
        Controller.throwPower = Vector2.Distance(GameManger.Instance.player.transform.position , Controller.transform.position)*i;

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
