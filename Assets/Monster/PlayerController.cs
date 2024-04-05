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

    LineRenderer line;
    SpriteRenderer spriteRenderer;
    Animator playerAnimator;
    Rigidbody2D rigid;
    Vector2 moveMoent = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        line = GetComponent<LineRenderer>();

    }
    // Update is called once per frame
    void Update()
    {
        Vector2 move = moveMoent * speed * Time.deltaTime;
        if(isMove==true)
        {
            transform.Translate(move);
        }
        
        if (moveMoent.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveMoent.x < 0)
        {
            spriteRenderer.flipX= true;
        }
        dirY += y * 1.0f * Time.deltaTime;
        
        Throw();
        ThrowRay();
        
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

    void Throw()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            isMove = false;
            playerAnimator.SetTrigger("ThrowReady");
            
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            line.enabled = false;
            playerAnimator.SetTrigger("ThrowTr");
            isMove = true;
        }
    }

    void ThrowRay()
    {

        if (dirY > 1 || dirY < -1)
        {
            dirY = y;
        }

        throwRayPosX = math.sqrt(dirX * dirX - dirY * dirY);

        Vector2 dir = new Vector2(throwRayPosX * dirX, dirY);
        RaycastHit2D rayThrow = Physics2D.Raycast(transform.position, dir, dirX);

        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position + (Vector3)dir);
        Debug.DrawRay(transform.position, dir, Color.red);

        


    }

    void OnThrowRayDir(InputValue inputValue)
    {
        y = inputValue.Get<Vector2>().y;
       
    }
}
