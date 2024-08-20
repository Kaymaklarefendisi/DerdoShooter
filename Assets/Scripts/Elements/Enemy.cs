using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{

    public bool isEnemyStarted;
    private int _curHealth;
    private Rigidbody _rb;
    public EnemyState enemyState;



    [Header("Elements")]
    public EnemyManager enemyManager;
    public Transform playerTransform;
    public Transform leftWheel;
    public Transform rightWheel;
    public EnemyHealthBar enemyHealthBar;
    public EnemyWeapon enemyWeapon;

    [Header("Properties")]
    public float enemySpeed;
    public float attackRate;
    public float wheelRotationSpeed;
    public int startHealth;
    public float shootDistance;




    public void StartEnemy(Transform pTransform, EnemyManager eManager)
    {

        playerTransform = pTransform;
        enemyManager = eManager;
        _curHealth = startHealth;
        _rb = GetComponent<Rigidbody>();


        enemyHealthBar.StartEnemyHealthBar(enemyManager.gameDirector);
        enemyHealthBar.Hide();

        enemyWeapon.StartEnemyWeapon(this);

    }



    void Update()
    {

        var distanceToPlayer = (transform.position - playerTransform.position).magnitude;
        if (isEnemyStarted && distanceToPlayer > shootDistance)
        {

            enemyState = EnemyState.WalkingTowardsPlayer;

        }
        else if (isEnemyStarted)
        {

            enemyState = EnemyState.Shooting;

        }


        if (enemyState == EnemyState.WalkingTowardsPlayer)
        {

            MoveTowardsPlayer();

        }
        else if (enemyState == EnemyState.Shooting)
        {

            transform.LookAt(playerTransform.position);
            enemyWeapon.TryShoot();

        }

    }




    public void StartMoving()
    {

        isEnemyStarted = true;

    }


    private void MoveTowardsPlayer()
    {

        var direction = playerTransform.position - transform.position;
        var directionNormalized = direction.normalized;

        transform.position += directionNormalized * enemySpeed * Time.deltaTime;
        transform.LookAt(playerTransform.position);

        rightWheel.Rotate(0, -wheelRotationSpeed, 0);
        leftWheel.Rotate(0, wheelRotationSpeed, 0);

    }





    public void EnemyGotHit(int damage, Vector3 pushDirection, float pushPower)
    {

        ReduceHealth(damage);

        PushEnemy(pushDirection, pushPower);

        enemyManager.gameDirector.audioManager.PlayMetalImpactSFX();

        UpdateHealthBar();


    }

    private void PushEnemy(Vector3 pushDirection, float pushPower)
    {

        _rb.AddForce(pushDirection * pushPower);


    }




    private void ReduceHealth(int damage)
    {

        _curHealth -= damage;
        //_curHealth = _curHealth - damage; demek


        if (_curHealth <= 0)
        {

            KillEnemy();

        }


    }


    private void KillEnemy()

    {
        enemyManager.gameDirector.fxManager.PlayExplosionFX(transform.position + Vector3.up);
        enemyManager.EnemyDied(this);

        enemyHealthBar.Hide();

        gameObject.SetActive(false);


    }



    private void UpdateHealthBar()
    {
        var healthRatio = GetHealthRatio();
       if (healthRatio > 0)
        {

            enemyHealthBar.Show();
            enemyHealthBar.SetHealthRatio(healthRatio);

        }


    }




    public float GetHealthRatio() //bu metodu çağırdığımda bana bir sayı geri dönecek, void demektense float dememizin sebebi
    {

        return (float)_curHealth / startHealth;

    }
}


public enum EnemyState
{

    Idle,
    WalkingTowardsPlayer,
    Shooting,

}