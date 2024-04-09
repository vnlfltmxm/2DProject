using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    PlayerController player;
    CircleCollider2D circleCollider;

    private void Awake()
    {
        player = transform.parent.GetComponent<Bomb>().Parent.GetComponent<PlayerController>();
        circleCollider = GetComponent<CircleCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void DestroyTile(GameObject ground)
    {
        int radiusInt = Mathf.RoundToInt(circleCollider.radius);
        for (int i = -radiusInt; i<= radiusInt; i++) 
        {
            for(int k=-radiusInt; k<= radiusInt; k++)
            {
                Vector3 checkCellPos = new Vector3(transform.position.x + i, transform.position.y + k, 0);
                float distance = Vector2.Distance(transform.position, checkCellPos) - 0.0001f;

                if (distance <= radiusInt)
                {
                    //Collider2D overCollider2d = Physics2D.OverlapCircle(checkCellPos, 0.01f);
                    ground.transform.gameObject.GetComponent<Brick>().RemoveTile(checkCellPos);
                }
            }
        }
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Unit"))) 
        {
            player.Hit();
            //Å¸ÀÏ¸ÊÆÄ±«
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            DestroyTile(collision.gameObject);
            //Debug.Log(collision.gameObject.name+"ssss");
            //if(Physics2D.OverlapCircle(this.transform.position, 1))
            //{
            //    Debug.Log(collision.gameObject.name+"aaaaa");
            //    DestroyTile();
            //}
        }



        transform.parent.gameObject.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {


         if (player.isReThrow)
        {
            player.ReThrow();
        }
    }
}
