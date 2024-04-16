using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeafController : MonoBehaviour
{

    Rigidbody2D rb;
    Vector2 nowWind;
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
        nowWind = GameManger.Instance.wind;
        rb.AddForce(nowWind);
    }


    private void FixedUpdate()
    {
        if(nowWind != GameManger.Instance.wind)
        {
            rb.velocity = Vector2.zero;
            nowWind = GameManger.Instance.wind;
            rb.AddForce(nowWind);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
    }
}
