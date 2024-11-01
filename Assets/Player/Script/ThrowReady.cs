using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ThrowReady : BaseState<PlayerController>
{
    Animator playerAnimator;
    PlayerUI playerUI;

    public ThrowReady(PlayerController controller) : base(controller)
    {
        playerAnimator = controller.GetComponent<Animator>();
        playerUI = controller.GetComponent<PlayerUI>();
    }

    public override void OnEnterState()
    {
        playerAnimator.SetTrigger("ThrowReady");
        playerUI.EnabledSlider();
        
    }
    public override void OnUpdateState()
    {
        Throw();
    }
    public override void OnFixedUpdateState()
    {

    }
    public override void OnExitState()
    {
        Controller.throwPower = 0;
        playerUI.DisabledSlider();
    }

    void Throw()
    {
        if (Input.GetKey(KeyCode.Space))
        {

            Controller.throwPower += 500f * Time.deltaTime;
            if (Controller.throwPower >= 1000)
            {
                Controller.throwPower = 1000;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            playerAnimator.SetTrigger("ThrowTr");
            Controller.bombThrowPos = Controller.bombPos * Controller.throwPower;
        }

    }
}
