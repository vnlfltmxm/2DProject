using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.TestTools;

public class EnemyMove : BaseState<EnemyController>
{
    public float moveSpeed = 5.0f;
    Vector2 dirX = new Vector2(1, 0);
    public EnemyMove(EnemyController controller) : base(controller)
    {
    }

    public override void OnEnterState()
    {
        //Controller.enabled = true;
        Controller.moveGage = 100;
    }
    public override void OnUpdateState()
    {
        Move();
    }
    public override void OnFixedUpdateState()
    {

    }
    public override void OnExitState()
    {

    }
    private void Move()
    {
        if (Controller.moveGage > 0)
        {
            Vector2 move = dirX * moveSpeed * Time.deltaTime;
            Controller.transform.Translate(move);
            Controller.moveGage -= 0.1f;
        }

    }
}
