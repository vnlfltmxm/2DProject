using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Explosion : MonoBehaviour
{
    [SerializeField] GameObject effect;
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
        effect.SetActive(true);
        effect.transform.position = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Unit"))) 
        {
            player.Hit();//나중에 콜리전.게임오브젝트.겟컴포넌트<유닛>으로 뺄것 안되면 개인이 처리하게끔 할것

            //타일맵파괴
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            DestroyTile(collision.gameObject);
        }

        //CameraController.target = player.gameObject;
        transform.parent.gameObject.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.playerState.ChangeState(StateName.Move);
        GameManger.Instance.playerTurn = false;
        //GameManger.Instance.playerThrow = false;
    }
}
