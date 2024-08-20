using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public GameDirector gameDirector;

    public List<Button> levelButtons;

    public void Show() 
    { 
        gameObject.SetActive(true);
        var lastFinishedLevel = PlayerPrefs.GetInt("LastFinishedLevel");

        //Bütün level butonları arasında gez ve last finished levelden yüksek olanları disable et
        for (int i =0; i < levelButtons.Count; i++)
        {

            if (i <= lastFinishedLevel)
            {
                levelButtons[i].interactable = true;
            }
            else
            {
                levelButtons[i].interactable = false;
            }
        }
    }


    public void Hide()
    {
        gameObject.SetActive(false);
    }


    public void StartLevelButtonPressed(int levelNo)
    {

        Hide();
        gameDirector.StartGame(levelNo);

    }


    public void QuitButtonPressed()
    {

        Application.Quit();

    }


}
