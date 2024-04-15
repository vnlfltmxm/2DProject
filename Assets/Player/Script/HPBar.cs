using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{

    GameObject parent;
    SpriteRenderer size;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
        size = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(parent.transform.position.x, parent.transform.position.y - 5, 0);
        size.size = new Vector2(0.1f * parent.GetComponent<PlayerController>().hp, size.size.y);
    }
}
