using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Explosion : MonoBehaviour
{
    
    GameObject parent;
    CircleCollider2D circleCollider;

    private void Awake()
    {
        parent = transform.parent.GetComponent<Bomb>().Parent;
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
                Vector2 checkCellPos = new Vector2(transform.position.x + i, transform.position.y + k);
                float distance = Vector2.Distance(transform.position, checkCellPos) - 0.0001f;

                if (distance <= circleCollider.radius)
                {
                    ground.transform.gameObject.GetComponent<Brick>().RemoveTile(checkCellPos);
                }
            }
        }
    }

    private void OnEnable()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Unit"))) 
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerController>().Hit();//나중에 콜리전.게임오브젝트.겟컴포넌트<유닛>으로 뺄것 안되면 개인이 처리하게끔 할것
            }
            else if(collision.CompareTag("Enemy"))
            {
                collision.GetComponent<EnemyController>().Hit();
            }
            //타일맵파괴
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            DestroyTile(collision.gameObject);
        }
            transform.parent.gameObject.SetActive(false);
        //CameraController.target = player.gameObject;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (parent.CompareTag("Player"))
        {
            
            parent.GetComponent<PlayerController>().playerState.ChangeState(PlayerStateName.Move);
            GameManger.Instance.playerTurn = false;
            GameManger.Instance.Invoke("EnemyTurn", 2);
        }
        else if(parent.CompareTag("Enemy"))
        {
            parent.GetComponent<EnemyController>().state.ChangeState(EnemyStateName.Move);
            GameManger.Instance.enemyTurn = false;
            GameManger.Instance.isItem = true;
            GameManger.Instance.Invoke("SpawnItem", 2);
        }
        
        //GameManger.Instance.playerThrow = false;
    }
}
