using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    public int Damage;
    public float HitRadius;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChaseAttack()
    {
        Collider[] hits;

        hits = Physics.OverlapSphere(this.gameObject.transform.position, HitRadius, LayerMask.GetMask("Player"));

        if(hits.Length > 0)
        {
            GameObject HitPlayer = hits[0].gameObject;
            HitPlayer.GetComponent<Health>().TakeDamage(1);
            
        }
    }
}
