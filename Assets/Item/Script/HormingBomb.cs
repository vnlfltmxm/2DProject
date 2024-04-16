using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HormingBomb : BombBaseState
{
    GameObject target;
    Vector2 targetPos;
    bool isCheck;
    public HormingBomb(Bomb bomb) : base(bomb)
    {
        target =GameManger.Instance.player;
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
        if (bomb.rigid.velocity.y < 0 && !isCheck) 
        {
            bomb.rigid.velocity = Vector2.zero;
            targetPos = new Vector2(target.transform.position.x- bomb.transform.position.x,target.transform.position.y - bomb.transform.position.y);
            //bomb.rigid.AddForce( targetPos*3000*Time.deltaTime);
            isCheck = true;
        }
        
        if (isCheck)
        {
            bomb.rigid.velocity = Vector2.zero;
            bomb.rigid.AddForce(targetPos * 1000 * Time.deltaTime);
        }

    }
    public override void OnExitState()
    {
        bomb.Parent.GetComponent<PlayerController>().ItemUse(BombStateName.Horming);
    }
}
