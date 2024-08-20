using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{

    public GameDirector gameDirector;

    public Vector3 rayStartOffset;
    public float touchRange;

    public bool isTouchingDoor;

    public Door touchingDoor;
    
   
    void Update()
    {

        Debug.DrawRay(transform.position + rayStartOffset, transform.forward * touchRange, Color.green);

        RaycastHit hit; //public private koymama gerek yok çünkü zaten pivate ın altında bu değişken
        if (Physics.Raycast(transform.position + rayStartOffset, transform.forward, out hit, touchRange))
        {
            //print(hit.transform.gameObject.name); başta ne olduğunu anlamak için böyle yazdık sonra sildik

            if (hit.transform.CompareTag("Door") && !hit.transform.GetComponentInParent<Door>().isDoorOpened)
            {
                isTouchingDoor = true;
                touchingDoor = hit.transform.GetComponentInParent<Door>();
                gameDirector.messageUI.ShowOpenDoorMessage();
            }
            else
            {
                isTouchingDoor = false;
                touchingDoor = null;
                gameDirector.messageUI.HideOpenDoorMessage();
                gameDirector.messageUI.HideDoorIsLockedMessage();
            }

        }

        else
        {
            isTouchingDoor = false;
            touchingDoor = null;
            gameDirector.messageUI.HideOpenDoorMessage();
            gameDirector.messageUI.HideDoorIsLockedMessage();
        }

    }


    public void OpenDoor()
    {

        touchingDoor.Open();
        if (touchingDoor.isDoorLocked)
        {
            UseKey();
        }
    }

    private void UseKey()
    {
        gameDirector.messageUI.HideKeyImage();
        gameDirector.playerHolder.isKeyCollected = false;
    }


}
