using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    Rigidbody2D rd;
    float time;
    float maxTime;

    private void Awake()
    {
        rd = transform.parent.GetComponent<Rigidbody2D>();
        maxTime = 5;
    }
    private void OnEnable()
    {
        CameraController.instanse.FollowCamera(this.gameObject);
        rd.gravityScale = 0.3f;
        time = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > maxTime)
        {
            rd.gravityScale = 1.0f;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int random = Random.Range(1,(int)BombStateName.Last);
            if(collision.gameObject.GetComponent<PlayerController>() == null)
            {
                collision.gameObject.GetComponentInParent<PlayerController>().item[random]++;
            }
            else
            {
                collision.gameObject.GetComponent<PlayerController>().item[random]++;

            }
        }

        if (!collision.CompareTag("Ground"))
        {
            GameManger.Instance.itemBoxPool.Enqueue(transform.parent.gameObject);
            transform.parent.gameObject.SetActive(false);
        }

        if (!collision.CompareTag("Bomb"))
        {
            GameManger.Instance.playerTurn = true;
        }
    }

}
