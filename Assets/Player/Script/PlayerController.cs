using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.U2D.Path;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;

    float y = 0;
    float throwRayPosX = 0;
    float dirX = 0;
    float dirY = 0;
    bool isMove = true;
    public bool isReThrow = true;

    LineRenderer line;
    SpriteRenderer spriteRenderer;
    Animator playerAnimator;
    Rigidbody2D rigid;
    Vector2 moveMoent = Vector2.zero;
    GameObject Bomb;
    Vector2 bombPos = Vector2.zero;
    public Vector2 bombThrowPos = Vector2.zero;
    public float throwPower = 0.0f;


    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        line = GetComponent<LineRenderer>();
        Bomb = transform.GetChild(1).gameObject;

    }
    // Start is called before the first frame update
    void Start()
    {
       

    }
    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        
        ThrowRay();
        Throw();
        
    }

    void OnMove(InputValue inputValue)
    {
        line.enabled = true;
        moveMoent = inputValue.Get<Vector2>();
        if (moveMoent.x != 0)
        {
            dirX = moveMoent.x;
        }

    }

    void PlayerMove()
    {
        Vector2 move = moveMoent * speed * Time.deltaTime;
        if (isMove == true)
        {
            transform.Translate(move);
        }

        if (moveMoent.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveMoent.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        playerAnimator.SetBool("IsMove", true);

        if (moveMoent.x == 0)
        {
            playerAnimator.SetBool("IsMove", false);
        }
    }

    void Throw()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMove = false;
            playerAnimator.SetTrigger("ThrowReady");

        }
        else if (Input.GetKey(KeyCode.Space))
        {

            throwPower += 500f*Time.deltaTime;
            Debug.Log(throwPower);
            if (throwPower >= 1000)
            {
                throwPower = 1000;
            }
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            line.enabled = false;
            playerAnimator.SetTrigger("ThrowTr");
            bombThrowPos = bombPos * throwPower;

            isMove = true;
        }
    }

    public void ReThrow()
    {
        playerAnimator.SetTrigger("ThrowReady");
        playerAnimator.SetTrigger("Re");
        playerAnimator.SetTrigger("ThrowTr");
        isReThrow = false;
        playerAnimator.ResetTrigger("Re");

    }
   
    void ThrowRay()
    {
        dirY += y * 1.0f * Time.deltaTime;

        if (dirY > 1 || dirY < -1)
        {
            dirY = y;
        }

        throwRayPosX = math.sqrt(dirX * dirX - dirY * dirY);

        bombPos = new Vector2(throwRayPosX * dirX, dirY);
        RaycastHit2D rayThrow = Physics2D.Raycast(transform.position, bombPos, dirX);

        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position + (Vector3)bombPos);
        Debug.DrawRay(transform.position, bombPos, Color.red);


    }

    void OnThrowRayDir(InputValue inputValue)
    {
        y = inputValue.Get<Vector2>().y;
       
    }

    void SetBomb()
    {
        Bomb.SetActive(true);
        throwPower = 0;
    }
}
