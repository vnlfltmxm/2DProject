using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeafController : MonoBehaviour
{

    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
       
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(GameManger.Instance.wind);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
