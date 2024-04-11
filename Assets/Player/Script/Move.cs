using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Move : BaseState
{

    public float speed = 5.0f;

    Animator playerAnimator;
    public Move(PlayerController controller) : base(controller)
    {
        playerAnimator = controller.GetComponent<Animator>();
    }

    public override void OnEnterState()
    {

    }
    public  override void OnUpdateState()
    {
        PlayerMove();
    }
    public  override void OnFixedUpdateState()
    {

    }
    public  override void OnExitState()
    {

    }

    void PlayerMove()
    {
        Vector2 move = Controller.moveMoent * speed * Time.deltaTime;
        Controller.transform.Translate(move);


        playerAnimator.SetBool("IsMove", true);

        if (Controller.moveMoent.x == 0)
        {
            playerAnimator.SetBool("IsMove", false);
        }

    }

    
}
