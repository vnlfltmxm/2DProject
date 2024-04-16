using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField] GameObject parent;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        transform.position = parent.transform.position;
        gameObject.transform.SetParent(null);
    }

    private void OnDisable()
    {
        gameObject.transform.SetParent(parent.transform);

    }
}
