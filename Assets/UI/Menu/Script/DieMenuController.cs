using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DieMenuController : MonoBehaviour
{
    public bool isMouse = false;
    int childIndex = 0;

    private void Awake()
    {
        SetAlpha();
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        SetAlpha();
        if (!isMouse)
        {
            ChoseChild();
            if (Input.GetKeyUp(KeyCode.KeypadEnter))
            {
                Selectchild();
            }
        }
    }

    public void ReturnStartMenu()
    {
        SceneManager.LoadScene("StartScene");
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

        if (childIndex >= transform.childCount - 1)
        {
            childIndex = 0;
        }
        
        if (childIndex < 0)
        {
            childIndex = transform.childCount - 2;
        }
    }

    void SetAlpha()
    {
        for (int i = 0; i < transform.childCount-1; i++)
        {
            if (i != childIndex)
            {
                transform.GetChild(i).GetComponent<Image>().canvasRenderer.SetAlpha(0);
            }
        }
        transform.GetChild(childIndex).GetComponent<Image>().canvasRenderer.SetAlpha(1);
    }

    void Selectchild()
    {
        switch (childIndex)
        {
            case 0:
                RetryClick();
                break;
            case 1:
                ReturnStartMenu();
                break;
            case 2:
                ExitGame();
                break;

        }
    }
    public void SetMouse()
    {
        isMouse = true;
    }
    public void UnSetMouse()
    {
        isMouse = false;
    }

    public void Index(int index)
    {
        childIndex = index;
    }
}
