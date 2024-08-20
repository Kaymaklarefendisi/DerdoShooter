using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public GameDirector gameDirector;
    public Transform playerMesh;

    public float mouseSensitivity;

    public Vector2 turn;





    void Update()
    {


        if(!gameDirector.ingameControlsLocked) //değilse demek için başına ! koyuyoruz

        {

            if (gameDirector.isGameStarted)
            {
                turn.x += Input.GetAxis("Mouse X");

                //turn.y += Input.GetAxis("Mouse Y");
                //turn.y = Mathf.Clamp(turn.y, -15f, 40f);

                gameDirector.playerHolder.RotatePlayer(turn);
            }




            if (Input.GetKey(KeyCode.S))
            {
                var direction = -playerMesh.forward;
                direction.y = 0;
                gameDirector.playerHolder.MovePlayer(direction);
            }


            if (Input.GetKey(KeyCode.W))
            {
                var direction = playerMesh.forward;
                direction.y = 0;
                gameDirector.playerHolder.MovePlayer(direction);
            }


            if (Input.GetKey(KeyCode.A))
            {
                var direction = -playerMesh.right;
                direction.y = 0;
                gameDirector.playerHolder.MovePlayer(direction);
            }


            if (Input.GetKey(KeyCode.D))
            {
                var direction = playerMesh.right;
                direction.y = 0;
                gameDirector.playerHolder.MovePlayer(direction);
            }


            if (Input.GetKeyDown(KeyCode.Space) && gameDirector.playerHolder.isTouchingGround)
            {
                gameDirector.playerHolder.MakePlayerJump();
            }


            if (Input.GetMouseButtonDown(0))
            {
                gameDirector.playerHolder.weapon.StartLoadingShotgun();
                gameDirector.playerHolder.weapon.TrySpawnBullets();
            }


            if (Input.GetMouseButtonUp(0))
            {
                gameDirector.playerHolder.weapon.StopLoadShotgunCoroutine();
            }


            if (Input.GetKeyDown(KeyCode.K))
            {
                gameDirector.adminUI.Show();
                Cursor.lockState = CursorLockMode.None;
            }


            if (Input.GetKeyDown(KeyCode.L))
            {
                PlayerPrefs.SetInt("LastFinishedLevel", 0);
                SceneManager.LoadScene(0);
                Cursor.lockState = CursorLockMode.None;
            }


            if (Input.GetKeyDown(KeyCode.P))
            {
                gameDirector.enemyManager.SpawnWave();
            }


            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                gameDirector.playerHolder.speedMultiplier = 1.6f;
            }


            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                gameDirector.playerHolder.speedMultiplier = 1f;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {

                if (gameDirector.playerHolder.objectDetector.isTouchingDoor) //kapıya dokunuyorsa bir şeyler olacak
                {

                    if (!gameDirector.playerHolder.objectDetector.touchingDoor.isDoorLocked || gameDirector.playerHolder.isKeyCollected) //kapı kilitli değilse ya da oyuncunun anahtarı varsa
                    {
                        gameDirector.playerHolder.objectDetector.OpenDoor();
                    }
                    else //kapı kilitliyse veya oyuncunun anahtarı yoksa
                    {
                        gameDirector.messageUI.ShowDoorIsLockedMessage();
                    }

                }

            }
        }


    }
}
