using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("Properties")]
    public float bulletSpeed;
    public float bulletTime;
    public int damage;
    public float pushPower;

    private float _bulletStartTime;

    [Header("Elements")]
    public MeshRenderer bulletMesh;
    public SphereCollider sphereCollider;



    private void Start()
    {
        _bulletStartTime = Time.time;
    }



    private void Update()
    {

        if (Time.time - _bulletStartTime > bulletTime) //başta 1 veya .25f demiştik ama bu oyun için önemli, editleyebilmek istiyorum
        {

            DestroyBullet();

        }
        else
        {

            Move();

        }



    }



    void DestroyBullet() //bir şeyi bir yerde birden fazla tekrar ediyorsan, hep bir metod oluştur.
    {

        bulletMesh.enabled = false;
        sphereCollider.enabled = false;
        Destroy(gameObject, 1f);

    }



    void Move()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
    }



    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().EnemyGotHit(damage, transform.forward, pushPower);
            DestroyBullet();
        }

        else if (other.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(other.gameObject);
            DestroyBullet();
        }

        else if (other.gameObject.CompareTag("MapObjects"))
        {

            DestroyBullet();
        }

    }


}
