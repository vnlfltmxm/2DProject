using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveBar : MonoBehaviour
{
    GameObject parent;
    float value;
    Slider slider;

    private void Awake()
    {
        parent = transform.parent.GetComponent<Canvas>().transform.parent.gameObject;
        slider = GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = parent.transform.position + new Vector3(0, 5, 0);
        slider.value = parent.GetComponent<PlayerController>().moveGage;
    }
}
