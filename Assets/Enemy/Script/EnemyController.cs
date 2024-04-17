using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [HideInInspector] public Animator animator;


    GameObject bomb;
    public StateMachin<EnemyController> state;
    public Vector2 bombPos = Vector2.zero;
    public float moveGage = 100f;
    public float dirX = 1;
    public bool isGround = false;

    Rigidbody2D rigid;
    float moveSpeed = 5.0f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bomb = transform.GetChild(1).gameObject;
        InitState();
    }

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        FixGround();
    }

    private void Update()
    {
        if (GameManger.Instance.enemyTurn)
        {
            CameraController.instanse.FollowCamera(gameObject);
            state.UpdateState();
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            state.ChangeState(EnemyStateName.Attack);
        }
    }

    public void Hit()
    {
        animator.SetTrigger("Hit");
    }

    void FixGround()
    {
        if (isGround)
        {
            rigid.gravityScale = 0;
        }
        else
        {
            rigid.gravityScale = 1;
        }
    }
    void SetBomb()
    {
        bomb.SetActive(true);
    }
    void InitState()
    {
        state = new StateMachin<EnemyController>(EnemyStateName.Move, new EnemyMove(this));
        state.AddState(EnemyStateName.Attack,new EnemyAttack(this));
    }
}
