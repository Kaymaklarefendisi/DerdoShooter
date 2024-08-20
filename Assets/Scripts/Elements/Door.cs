using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{

    [Header("Properties")]
    public bool isDoorLocked;
    public bool isDoorOpened; //oyun başlarken bool lar hep false döner


    [Header("Elements")]
    public Transform leftDoor;
    public Transform rightDoor;

    public void Open()
    {

        leftDoor.DOLocalMoveZ(1, .3f);
        rightDoor.DOLocalMoveZ(-2.5f, .3f);
        isDoorOpened = true;

    }



}
