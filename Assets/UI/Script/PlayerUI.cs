using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUI : MonoBehaviour
{
    [SerializeField] Slider powerBar;
    [SerializeField] public Image playerItem;
    [SerializeField] Slider moveGage;

    void Start()
    {
        DisabledSlider();
        DisabledItem();
        DisabledMoveGage();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void EnabledSlider()
    {
        powerBar.gameObject.SetActive(true);
    }
    public void EnabledItem()
    {
        playerItem.gameObject.SetActive(true);
    }
    public void EnabledMoveGage()
    {
        moveGage.gameObject.SetActive(true);
    }

    public void DisabledMoveGage()
    {
        moveGage.gameObject.SetActive(false);
    }
    public void DisabledSlider()
    {
        powerBar.gameObject.SetActive(false);
    }

    public void DisabledItem()
    {
        playerItem.gameObject.SetActive(false);
    }


}
