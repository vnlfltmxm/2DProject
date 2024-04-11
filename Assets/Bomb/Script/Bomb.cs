using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject Parent;
    Rigidbody2D rigid;
    Vector2 destination = Vector2.zero;

    // Start is called before the first frame update
    private void Awake()
    {
        Parent = transform.parent.gameObject;
        rigid = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        

    }

    private void OnEnable()
    {
        transform.position = Parent.transform.position;
        destination = GetComponentInParent<PlayerController>().bombThrowPos;
        transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.SetParent(null);
        rigid.AddForce(destination);
    }

    // Update is called once per frame
    void Update()
    {
    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            return;
        }
        gameObject.transform.SetParent(Parent.transform);
        transform.GetChild(0).gameObject.SetActive(true);
        //this.gameObject.SetActive(false);
        
    }

}
