using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageAndHealth : MonoBehaviour
{
    public int baseHealth;
    int currHealth;
    private void Start()
    {
        currHealth = baseHealth;
    }
    void Death()
    {
        //Maybe Play animation
        Destroy(this.gameObject);
    }

    public void DealDamage(int DamageDealt)
    {
        currHealth -= DamageDealt;
        if (currHealth < 0) Death();
        
    }
}
