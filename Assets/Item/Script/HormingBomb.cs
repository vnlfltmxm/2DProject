using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HormingBomb : BaseState<Bomb>
{
    GameObject target;
    Vector2 targetPos;
    bool isCheck;
    public HormingBomb(Bomb bomb) : base(bomb)
    {
        target =GameManger.Instance.enemy;
    }

    public override void OnEnterState()
    {
        isCheck = false;
    }
    public override void OnUpdateState()
    {
    }
    public override void OnFixedUpdateState()
    {
        if (Controller.rigid.velocity.y < 0 && !isCheck) 
        {
            Controller.rigid.velocity = Vector2.zero;
            targetPos = new Vector2(target.transform.position.x- Controller.transform.position.x,target.transform.position.y - Controller.transform.position.y);
            //bomb.rigid.AddForce( targetPos*3000*Time.deltaTime);
            isCheck = true;
        }
        
        if (isCheck)
        {
            Controller.rigid.velocity = Vector2.zero;
            Controller.rigid.AddForce(targetPos * 1000 * Time.deltaTime);
        }

    }
    public override void OnExitState()
    {
        Controller.Parent.GetComponent<PlayerController>().ItemUse(BombStateName.Horming);
    }
}
