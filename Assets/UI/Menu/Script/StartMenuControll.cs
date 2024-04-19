using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuControll : MonoBehaviour
{
    int childIndex = 0;

    private void Awake()
    {
        childIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ChoseChild();
        SetColor();

        if (Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            Selectchild();
        }
    }

    void ChoseChild()
    {


        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            childIndex++;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            childIndex--;
        }

        if (childIndex >= transform.childCount)
        {
            childIndex = 0;
        }

        if (childIndex < 0)
        {
            childIndex = transform.childCount;
        }
    }

    void SetColor()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i != childIndex)
            {
                transform.GetChild(i).GetComponent<Image>().color = Color.white;
            }
        }
        transform.GetChild(childIndex).GetComponent<Image>().color = Color.red;
    }

    void Selectchild()
    {
        switch (childIndex)
        {
            case 0:
                RetryClick();
                break;
            case 1:
                ExitGame();
                break;
        }
    }
    public void RetryClick()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else

        Application.Quit();

#endif
    }
}
