using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    Animator parentAnimator;

    private void Awake()
    {
        parentAnimator=GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            if (transform.parent.CompareTag("Player"))
            {
                transform.parent.GetComponent<PlayerController>().isGround = true;
                parentAnimator.SetBool("IsGround", true);
            }
            else 
            {
                transform.parent.GetComponent<EnemyController>().isGround = true;
            }
               
            
        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            if (transform.parent.CompareTag("Player"))
            {
                transform.parent.GetComponent<PlayerController>().isGround = false;
                parentAnimator.SetBool("IsGround", false);
            }
            else
            {
                transform.parent.GetComponent<EnemyController>().isGround = false;
            }
            
        }
    }
}
