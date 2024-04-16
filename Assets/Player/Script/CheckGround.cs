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
            transform.parent.GetComponent<PlayerController>().isGround = true;
            parentAnimator.SetBool("IsGround", true);
        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            transform.parent.GetComponent<PlayerController>().isGround = false;
            parentAnimator.SetBool("IsGround", false);
        }
    }
}
