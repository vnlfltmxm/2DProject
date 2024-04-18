using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Bomb : MonoBehaviour
{
    [SerializeField] GameObject effect;
    public GameObject Parent;
    public Rigidbody2D rigid;
    Vector2 destination = Vector2.zero;
    public StateMachin<Bomb> state;

    // Start is called before the first frame update
    private void Awake()
    {
        Parent = transform.parent.gameObject;
        rigid = GetComponent<Rigidbody2D>();
        InitState();
    }
    void Start()
    {
        


    }

    private void OnEnable()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        
        if (Parent.CompareTag("Player"))
        {
            GameManger.Instance.CancelInvoke("EnemyTurn");
            transform.position = Parent.transform.position + (Vector3)Parent.GetComponent<PlayerController>().bombPos * 3;
            destination = GetComponentInParent<PlayerController>().bombThrowPos * 1.5f;
            state.ChangeState((BombStateName)GetComponentInParent<PlayerController>().bombIndex);
        }
        else
        {
            transform.position =(Vector3)GetComponentInParent<EnemyController>().bombPos;
            //transform.position = destination;
            //destination = (GetComponentInParent<EnemyController>().bombThrowPos -GetComponentInParent<EnemyController>().bombPos);
            if (GameManger.Instance.wind.x / 50 <= 0)
            {
                destination = (GetComponentInParent<EnemyController>().bombThrowPos - (Vector2)transform.parent.position);
                destination *= (GetComponentInParent<EnemyController>().throwPower);
            }
            else 
            {
                float rd = Random.Range(GetComponentInParent<EnemyController>().bombThrowPos.y / 2, GetComponentInParent<EnemyController>().bombThrowPos.y);
                Vector2 t = new Vector2(GetComponentInParent<EnemyController>().bombThrowPos.x, rd);
                destination = (t - (Vector2)transform.parent.position);
                //destination *= GetComponentInParent<EnemyController>().bombThrowPos;
                float r = Random.Range(1, 4);
                destination *= (GetComponentInParent<EnemyController>().throwPower* r);
            }
            
            //float throwPower = transform.parent.transform.position.x- GameManger.Instance.player.transform.position.x;
            //Vector2 throwPower = (GameManger.Instance.player.transform.position + (Vector3)GetComponentInParent<EnemyController>().bombThrowPos);
            //Vector2 throwPower = transform.parent.transform.position + (Vector3)GetComponentInParent<EnemyController>().bombThrowPos;
            //Vector2 throwPower= transform.parent.position+(Vector3)GetComponentInParent<EnemyController>().bombThrowPos;
            // destination =  (Vector3)GetComponentInParent<EnemyController>().bombThrowPos- transform.parent.position;
            // destination *= (transform.parent.transform.position.x+GameManger.Instance.player.transform.position.x);
            //float t=Vector2.Distance(GetComponentInParent<EnemyController>().bombThrowPos, transform.parent.position);
            //destination *=throwPower;
            //destination *= (Vector2.Distance(GameManger.Instance.player.transform.position, GetComponentInParent<EnemyController>().bombThrowPos));
            //destination *= Vector3.Distance(GameManger.Instance.player.transform.position , transform.parent.transform.position);
            //destination *= (Vector3.Distance(GameManger.Instance.player.transform.position, GetComponentInParent<EnemyController>().bombPos) -Vector3.Distance(GetComponentInParent<EnemyController>().bombThrowPos, GetComponentInParent<EnemyController>().bombPos));
            //destination *= GameManger.Instance.wind.x / 10;
            ////중력을 가져옴
            //Vector2 gravity = Physics2D.gravity;

            ////각도 구하기(라디안
            //float angle = /*Mathf.Cos(GetComponentInParent<EnemyController>().bombThrowPos.y)*/45f * Mathf.Deg2Rad;
            ////수평성분
            //float h = gravity.magnitude * Mathf.Cos(angle);
            ////질량
            //float mass = rigid.mass;
            ////힘
            //float force = mass * h;
            //destination = (GetComponentInParent<EnemyController>().bombThrowPos.x > 0) ? Vector2.right : Vector2.left;//new Vector2(GetComponentInParent<EnemyController>().bombThrowPos.x, 0);
            //destination *= force;

        }
        gameObject.transform.SetParent(null);
        CameraController.instanse.FollowCamera(this.gameObject);
        rigid.AddForce(destination);
    }

    private void FixedUpdate()
    {
        state.FixedUpdateState();
    }

    // Update is called once per frame
    void Update()
    {
        //rigid.AddForce(GameManger.Instance.wind * Time.deltaTime);
        state.UpdateState();
    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.transform.SetParent(Parent.transform);
        effect.SetActive(true);
        effect.transform.position = transform.position;
        transform.GetChild(0).gameObject.SetActive(true);
        //this.gameObject.SetActive(false);

    }


    private void OnDisable()
    {
        state.ChangeState(BombStateName.Normal);
        if (Parent.CompareTag("Player"))
        {
            Parent.GetComponentInParent<PlayerController>().bombIndex = 0;
        }
        else
        {
            return;
        }
    }


    void InitState()
    {
        state = new StateMachin<Bomb>(BombStateName.Normal, new NormalBomb(this));
        state.AddState(BombStateName.Plus, new PlusBomb(this));
        state.AddState(BombStateName.Horming, new HormingBomb(this));
    }

}
