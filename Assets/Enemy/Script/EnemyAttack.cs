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
