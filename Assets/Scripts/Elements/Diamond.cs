using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{

    private GameDirector _gameDirector; //oyunsektöründe yapıldığı gibi ypalım bu sefer - private çünkü access edilmesin istiyoruz?


    public void StartDiamond(GameDirector gameDirector)
    {

        _gameDirector = gameDirector;


    }








    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetCollected();
        }

    }



    void GetCollected()
    {
        gameObject.SetActive(false);
        _gameDirector.DiamondCollected();




    }
}
