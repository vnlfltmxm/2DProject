using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Move : BaseState<PlayerController>
{

    public float speed = 5.0f;

    Animator playerAnimator;
    public Move(PlayerController controller) : base(controller)
    {
        playerAnimator = controller.GetComponent<Animator>();
    }

    public override void OnEnterState()
    {
        Controller.enabled = true;
        Controller.moveGage = 100;
    }
    public  override void OnUpdateState()
    {
        PlayerDirx();

        if (Controller.moveGage > 0 && Controller.isGround) 
        {
            PlayerMove();

        }
        else
        {
            Controller.playerUI.DisabledMoveGage();
        }
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
            Controller.playerUI.DisabledMoveGage();
        }
        else
        {
            Controller.playerUI.EnabledMoveGage();
            Controller.moveGage -= 20f*Time.deltaTime;
        }

        
    }

    void PlayerDirx()
    {
        if (Controller.moveMoent.x != 0)
        {
            Controller.dirX = Controller.moveMoent.x;
        }
        if (Controller.moveMoent.x > 0)
        {
            Controller.spriteRenderer.flipX = false;
        }
        else if (Controller.moveMoent.x < 0)
        {
            Controller.spriteRenderer.flipX = true;
        }
    }

}
