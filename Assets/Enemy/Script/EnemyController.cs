using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyController : MonoBehaviour
{
    [HideInInspector] public Animator animator;

    GameObject bomb;
    public StateMachin<EnemyController> state;
    public Vector2 bombPos = Vector2.zero;
    public Vector2 bombThrowPos = Vector2.zero;
    public Vector2 targetPos = Vector2.zero;
    public Rigidbody2D rigid;
    public float throwPower;
    public float moveGage = 100f;
    public float dirX = 1;
    public float hp = 3;
    public bool isGround = false;
    public bool isMoving = false;

    SpriteRenderer spriteRenderer;
    float moveSpeed = 5.0f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        bomb = transform.GetChild(1).gameObject;
        InitState();
        state.ChangeState(EnemyStateName.Move);
    }

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        //FixGround();
    }

    private void Update()
    {
        if (GameManger.Instance.enemyTurn)
        {
            CameraController.instanse.FollowCamera(gameObject);
            DirX();
            state.UpdateState();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            state.ChangeState(EnemyStateName.Attack);
        }
        if (hp <= 0)
        {
            GameManger.Instance.isGameClear = true;
        }
    }

    public void Hit()
    {
        animator.SetTrigger("Hit");
        hp--;
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

    void DirX()
    {
        if (GameManger.Instance.player.transform.position.x - transform.position.x < 0)
        {
            dirX = -1;
        }
        else if(GameManger.Instance.player.transform.position.x - transform.position.x > 0)
        {
            dirX = 1;
        }
    }

    void SetBomb()
    {
        bomb.SetActive(true);
        GameManger.Instance.enemyTurn = false;
    }
    void InitState()
    {
        state = new StateMachin<EnemyController>(EnemyStateName.Move, new EnemyMove(this));
        state.AddState(EnemyStateName.Attack,new EnemyAttack(this));
    }
    public void MoveRender(float dirX)
    {
        if (dirX < 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (dirX > 0)
        {
            spriteRenderer.flipX = true;
        }
    }
}
