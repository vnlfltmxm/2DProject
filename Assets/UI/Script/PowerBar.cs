using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    GameObject parent;
    float dirX;
    float value;
    Vector2 adjustment = Vector2.zero;
    Slider slider;
    private void Awake()
    {
        parent = GetComponentInParent<Canvas>().transform.parent.gameObject;
        slider = GetComponent<Slider>();

    }

    private void Update()
    {
        transform.position = parent.transform.position + (Vector3)adjustment;
        value = parent.GetComponent<PlayerController>().throwPower;
        slider.value = value;
        ChangeColor();
    }

    private void OnEnable()
    {
        dirX = -parent.GetComponent<PlayerController>().dirX*5;
        adjustment = new Vector2(dirX, 0);
    }

    void ChangeColor()
    {
        if(value < 450)
        {
            slider.targetGraphic.color = Color.yellow;
        }
        else if(value < 750)
        {
            slider.targetGraphic.color = new Color(1, 0.5f, 0, 1);
        }
        else
        {
            slider.targetGraphic.color = Color.red;
        }
    }
}
