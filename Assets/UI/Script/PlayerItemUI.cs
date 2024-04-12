using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemUI : MonoBehaviour
{
    [SerializeField] List<Sprite> itemImage;
    Image image;
    public int index = 0;
    GameObject parent;
    private void Awake()
    {
        parent = GetComponentInParent<Canvas>().transform.parent.gameObject;
        image = GetComponent<Image>();
        image.sprite = itemImage[index];
        transform.position = parent.transform.position + new Vector3(0, 3, 0);
    }

    private void Update()
    {
        ChoseItem();
    }

    private void OnEnable()
    {
        transform.position = parent.transform.position + new Vector3(0, 3, 0);
    }

    private void OnDisable()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeColor(int[] item)
    {
        if (item[index] <= 0)
        {
            image.color = new Color(0.18f, 0.16f, 0.16f);
        }
        else
        {
            image.color = Color.white;
        }
    }

    void ChoseItem()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            index++;
            if (index >= itemImage.Count)
            {
                index = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            index--;
            if(index < 0)
            {
                index = itemImage.Count - 1;
            }
        }
        image.sprite = itemImage[index];
    }
}
