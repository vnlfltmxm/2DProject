using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.U2D.Path;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Image image;
    public PlayerItemUI playerItemUI;
    public float speed = 5.0f;
    public int hp = 3;
    public float moveGage = 100;

    float y = 0;
    float throwRayPosX = 0;
    public float dirX = 0;
    float dirY = 0;
    public float bombIndex = 0;
    public int[] item;

    public PlayerUI playerUI;
    public bool isGround = false;

    public PlayerState playerState;
    LineRenderer line;
    public SpriteRenderer spriteRenderer;
    Animator playerAnimator;
    Rigidbody2D rigid;
    public Vector2 moveMoent = Vector2.zero;
    GameObject Bomb;
    public Vector2 bombPos = Vector2.zero;
    public Vector2 bombThrowPos = Vector2.zero;
    public float throwPower = 0.0f;


    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        line = GetComponent<LineRenderer>();
        playerUI = GetComponent<PlayerUI>();
        Bomb = transform.GetChild(1).gameObject;
        playerState = new PlayerState(StateName.Move, new Move(this));
        item = new int[(int)BombStateName.Last];
        item[0] = 1;
        item[1] = 1;
        item[2] = 1;
        dirX = 1;
        playerItemUI =image.GetComponent<PlayerItemUI>();
        InitState();
    }
    // Start is called before the first frame update
    void Start()
    {
        //playerState = GetComponent<PlayerState>();
        //InitState();
    }
    // Update is called once per frame
    void Update()
    {
        //PlayerMove();
        if (GameManger.Instance.playerTurn)
        {
            CameraController.instanse.FollowCamera(this.gameObject);
            playerState.UpdateState();

            Throw();


            ItemSelect();



            ThrowRay();
            //Throw();
        }

        if (hp <= 0)
        {
            playerAnimator.SetBool("Die", true);
        }
    }

    void Throw()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerState.ChangeState(StateName.ThrowReady);

        }
    }
    void ItemSelect()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            playerUI.EnabledItem();
        }
        else if (Input.GetKey(KeyCode.Tab))
        {
            playerItemUI.ChangeColor(item);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            playerUI.DisabledItem();
            if (item[playerItemUI.index] > 0) 
            {
                bombIndex = playerItemUI.index;
            }
        }
    }

    public void ItemUse(BombStateName stateName)
    {
        item[(int)stateName]--;
    }
    void OnMove(InputValue inputValue)
    {
        moveMoent = inputValue.Get<Vector2>();

    }

    //void PlayerMove()
    //{
    //    Vector2 move = moveMoent * speed * Time.deltaTime;
    //    if (isMove == true)
    //    {
    //        transform.Translate(move);
    //    }

    //    if (moveMoent.x > 0)
    //    {
    //        spriteRenderer.flipX = false;
    //    }
    //    else if (moveMoent.x < 0)
    //    {
    //        spriteRenderer.flipX = true;
    //    }
    //    playerAnimator.SetBool("IsMove", true);

    //    if (moveMoent.x == 0)
    //    {
    //        playerAnimator.SetBool("IsMove", false);
    //    }
    //}

    void InitState()
    {
        playerState.AddState(StateName.ThrowReady, new ThrowReady(this));
    }


    //void Throw()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        isMove = false;
    //        playerAnimator.SetTrigger("ThrowReady");

    //    }
    //    else if (Input.GetKey(KeyCode.Space))
    //    {

    //        throwPower += 500f*Time.deltaTime;
    //        if (throwPower >= 1000)
    //        {
    //            throwPower = 1000;
    //        }
    //    }
    //    else if(Input.GetKeyUp(KeyCode.Space))
    //    {
    //        line.enabled = false;
    //        playerAnimator.SetTrigger("ThrowTr");
    //        bombThrowPos = bombPos * throwPower;

    //        isMove = true;
    //    }
    //}

    //public void ReThrow()
    //{
    //    playerAnimator.SetTrigger("ThrowReady");
    //    playerAnimator.SetTrigger("Re");
    //    playerAnimator.SetTrigger("ThrowTr");
    //    isReThrow = false;
    //    playerAnimator.ResetTrigger("Re");

    //}
   
    void ThrowRay()
    {
        line.enabled = true;
        dirY += y * 1.0f * Time.deltaTime;

        if (dirY > 1 || dirY < -1)
        {
            dirY = y;
        }

        throwRayPosX = math.sqrt(dirX * dirX - dirY * dirY);

        bombPos = new Vector2(throwRayPosX * dirX, dirY);

        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position + (Vector3)bombPos*4);


    }

    void OnThrowRayDir(InputValue inputValue)
    {
        y = inputValue.Get<Vector2>().y;
       
    }

    void SetBomb()
    {
        Bomb.SetActive(true);
        line.enabled = false;
        enabled = false;
    }
    public void Hit()
    {
        playerAnimator.SetTrigger("Hit");
        hp--;
    }
}
