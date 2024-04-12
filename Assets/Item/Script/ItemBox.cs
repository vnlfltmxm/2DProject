using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int random = Random.Range(1,(int)BombStateName.Last);
            collision.gameObject.GetComponent<PlayerController>().item[random]++;
            Debug.Log(collision.gameObject.GetComponent<PlayerController>().item[random]);
        }

        if (!collision.CompareTag("Ground"))
        {
            gameObject.SetActive(false);

        }

    }

}
