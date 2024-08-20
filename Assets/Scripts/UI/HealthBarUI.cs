using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBarUI : MonoBehaviour
{

    public Image fillBar;
    public Image borderImage;


    public void Show()
    {
        gameObject.SetActive(true);

    }


    public void Hide()
    {
        gameObject.SetActive(false);

    }



    public void SetPlayerHealthBar(float healthRatio)
    {

        fillBar.fillAmount = healthRatio;

    }


    public void FlashBorder()
    {
        borderImage.DOKill();
        borderImage.color = Color.black;
        borderImage.DOColor(Color.red, .2f).SetLoops(2, LoopType.Yoyo);

    }

}
