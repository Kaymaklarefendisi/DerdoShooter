using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameDirector gameDirector;

    public DiamondManager diamondManager;

    public Transform placeHoldersParent;


    public Enemy enemyPrefab;

    public PowerUp healPowerUpPrefab;




    //public int enemyCount; - yerlerini designate eden placeholderlar oluşturduk artık buna ihtiyacımız yok

    public List<Enemy> activeEnemies = new List<Enemy>(); //arrayler de kullanılır listlerde. Listeler manipüle edilmesi daha kolay. Sürekli bir şey sokup çıkarmak için liste daha kolay

    //public int waveCount;

    public void GateTriggered()
    {

        foreach (Enemy enemy in activeEnemies)
        {
            enemy.StartMoving();
        }

    }



    public void SpawnWaveDelayed(float delay) //bunu tutabiliriz şimdilik
    {
        Invoke(nameof(SpawnWave), delay);
        //Invoke("SpawnWave", 3) de diyebilirdik
    }


    public void SpawnWave()
    {
        foreach (Transform ph in placeHoldersParent)
        {

            SpawnEnemy(ph.position);
            ph.GetComponent<MeshRenderer>().enabled = false;

        }

    }




    public void SpawnEnemy(Vector3 position)
    {
        var newEnemy = Instantiate(enemyPrefab);
        newEnemy.transform.position = position;
        newEnemy.StartEnemy(gameDirector.playerHolder.transform, this);
        activeEnemies.Add(newEnemy); 
    }


    public void EnemyDied(Enemy e)
    {
        // böyle takip edebilirim kaç tane enemy var sahnede print(activeEnemies.Count);

        activeEnemies.Remove(e); // önce remove et sonra say demek istiyoruz o yüzden if'in dışında yaşıyor

        if (activeEnemies.Count == 0)
        {
            gameDirector.diamondManager.SpawnDiamonds();
        }

        if (Random.value < gameDirector.settings.healSpawnChance)
        {

            SpawnPowerUp(e);

        }

    }


    private void SpawnPowerUp(Enemy e)
    {

        var newPowerUp = Instantiate(healPowerUpPrefab);
        newPowerUp.transform.position = e.transform.position + Vector3.up;
        newPowerUp.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-200, 200), 200, Random.Range(-200, 200)));
        newPowerUp.StartPowerUp(gameDirector.playerHolder);

    }


}
