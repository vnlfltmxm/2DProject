using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.TestTools;

public class EnemyMove : BaseState<EnemyController>
{
    float moveSpeed = 5.0f;
    Vector2 moveDir = Vector2.zero;
    public EnemyMove(EnemyController controller) : base(controller)
    {
    }

    public override void OnEnterState()
    {
        Controller.rigid.velocity = Vector2.zero;
        Controller.moveGage = 100;
        moveDir = ChoseDir() ;
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
        Controller.rigid.velocity = Vector2.zero;
    }
    private void Move()
    {
        float moveRange = Random.Range(0, Controller.moveGage + 1);
        if (moveRange > 0)
        {
            Vector2 move = moveDir * moveSpeed * Time.deltaTime;
            Controller.MoveRender(move.x);
            Controller.transform.Translate(move);
            Controller.moveGage -= 20f*Time.deltaTime;
        }
        else
        {
            Controller.MoveRender(Controller.dirX);
            Controller.state.ChangeState(EnemyStateName.Attack);
        }
    }

    private Vector2 ChoseDir()
    {
        int dir = Random.Range(-1, 2);

        return new Vector2(dir, 0);
    }
    

}
