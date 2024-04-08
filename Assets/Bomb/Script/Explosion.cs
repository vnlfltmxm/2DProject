using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = transform.parent.GetComponent<Bomb>().Parent.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Unit"))) 
        {
            Animator animator = collision.gameObject.GetComponent<Animator>();
            animator.SetTrigger("Hit");
            //Å¸ÀÏ¸ÊÆÄ±«
        }


       



        

        transform.parent.gameObject.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {


         if (this.animator.gameObject.GetComponent<PlayerController>().isReThrow)
        {
            this.animator.gameObject.GetComponent<PlayerController>().ReThrow();
        }
    }
}
