using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject ExplosionParticle;

    private BombFlash bombFlash;
    private LayerMask enemyLayer;
    private int damage;
    private float timer;

    private bool primed;
    
    // Start is called before the first frame update
    void Start()
    {
        bombFlash = GetComponent<BombFlash>();
        //SetBomb(25, LayerMask.GetMask("Enemy"), 3);
    }

    public void SetBomb(int d, LayerMask enemy, float detonateTime)
    {
        damage = d;
        enemyLayer = enemy;
        timer = detonateTime;
        primed = true;
    }

    private void DealDamage()
    {
        Collider[] enemies;
        // check area around enemy hit and find closest enemies
        if ((enemies = Physics.OverlapSphere(transform.position, 4, enemyLayer)).Length > 0)
        {
            // deal damage to each enemy
            foreach (Collider e in enemies)
            {
                e.gameObject.GetComponentInParent<EnemyDamageAndHealth>().DealDamage(damage);
            }
        }
    }

    IEnumerator FlashBomb()
    {
        float time1 = timer / 4; // do this one twice
        float time2 = time1 / 2; // do this one four times

        StartCoroutine(bombFlash.Flash());
        yield return new WaitForSeconds(time1);

        StartCoroutine(bombFlash.Flash());
        yield return new WaitForSeconds(time1);

        StartCoroutine(bombFlash.Flash());
        yield return new WaitForSeconds(time2);

        StartCoroutine(bombFlash.Flash());
        yield return new WaitForSeconds(time2);

        StartCoroutine(bombFlash.Flash());
        yield return new WaitForSeconds(time2);

        StartCoroutine(bombFlash.Flash());
        yield return new WaitForSeconds(time2);
    }
    IEnumerator Explode()
    {
        StartCoroutine(FlashBomb());
        yield return new WaitForSeconds(timer);
        // play particle
        GameObject explosion = Instantiate(ExplosionParticle, transform.position, Quaternion.identity);
        Destroy(explosion, 3);
        // deal damage to enemies in radius
        DealDamage();
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (primed)
        {
            StartCoroutine(Explode());
            primed = false;
        }
    }
}
