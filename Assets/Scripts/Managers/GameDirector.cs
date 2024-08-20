using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    [Header("Managers")]
    public InputManager inputManager;
    public EnemyManager enemyManager;
    public DiamondManager diamondManager;
    public AudioManager audioManager;
    public FXManager fxManager;
    public Settings settings;
    public Player playerHolder;
    public LevelManager levelManager;


    [Header("UI")]
    public MainUI mainUI;
    public WinUI winUI;
    public FailUI failUI;
    public HealthBarUI healthBarUI;
    public GetHitUI getHitUI;
    public MessageUI messageUI;
    public AdminUI adminUI;


    public bool isGameStarted;
    public bool ingameControlsLocked;
    public Transform cameraTransform;

    public int desiredLevel;

    public int currentLevel;


    private void Start()
    {        

        ingameControlsLocked = true;
        mainUI.Show();
        winUI.Hide(); //önem sırasına göre yazabilirsin
        failUI.Hide();

    }


    public void StartGame(int levelNo)
    {

        currentLevel = levelNo;

        levelManager.ClearCurrentLevel();
        levelManager.CreateLevel(levelNo);

        Cursor.lockState = CursorLockMode.Locked;
        isGameStarted = true;

        enemyManager.SpawnWave();
        ingameControlsLocked = false;
        playerHolder.StartPlayer();
        healthBarUI.Show();

        diamondManager.StartDiamondManager();
        playerHolder.ResetRigidBodyConstraints();

    }




    public void LevelCompleted()
    {

        ingameControlsLocked = true;
        Cursor.lockState = CursorLockMode.None;
        winUI.Show();
        healthBarUI.Hide();
        playerHolder.FreezePlayerYAxis();

        //Son ulaşılmış levelı al eğer şu an biten level son ulaşılmış leveldan büyük ise son ulaşılmış levelı şu an biten levela eşitle

        if (currentLevel > PlayerPrefs.GetInt("LastFinishedLevel"))
        {

            PlayerPrefs.SetInt("LastFinishedLevel", currentLevel); //playerprefs dediğimiz resetlenmeyen bir değer - windowsun içinde registry nin içine yazıyor

            //normal bir değer girseydik sahne yeniden yüklenince değer kaybolurdu. Ama playerprefs sabit kalıyor.

        }

    }


    public void LevelFailed()
    {

        ingameControlsLocked = true;
        Cursor.lockState = CursorLockMode.None;
        failUI.Show();
        healthBarUI.Hide();
        playerHolder.FreezePlayerYAxis();

    }



    public void DiamondCollected()
    {
       
        LevelCompleted();
        
    }

        
}


