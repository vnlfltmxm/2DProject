using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    private void Awake()
    {
        transform.GetChild(0).transform.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0)
        {
            if (GameManger.Instance.isGameOver)
            {
                transform.GetChild(0).transform.gameObject.SetActive(true);
                text.text = "GameOver";
            }
            else if (GameManger.Instance.isGameClear)
            {
                transform.GetChild(0).transform.gameObject.SetActive(true);
                text.text = "GameClear";
            }
        }
        else if (Input.GetKeyUp(KeyCode.Escape))
        {
            OpenMenu();
        }

    }

    void OpenMenu()
    {
        if (transform.GetChild(0).transform.gameObject.activeSelf)
        {
            Time.timeScale = 1;
            transform.GetChild(0).transform.gameObject.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            transform.GetChild(0).transform.gameObject.SetActive(true);
            text.text = "Stop";

        }
    }


}
