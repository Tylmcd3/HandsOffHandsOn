using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageAndHealth : MonoBehaviour
{
    private GameStateManager gsManager;
    public int baseHealth;
    [SerializeField]
    int currHealth;
    private void Start()
    {
        gsManager = GameObject.Find("GameManager").GetComponent<GameStateManager>();
        currHealth = baseHealth;
    }
    void Death()
    {
        //Maybe Play animation
        Destroy(this.gameObject);
        // find wave manager and notify of kill
        gsManager.RemoveEnemy();
        // drop weapon
        GetComponent<EnemyDropWeapon>().DropWeapon();
    }

    public void DealDamage(int DamageDealt)
    {
        currHealth -= DamageDealt;
        if (currHealth < 0) Death();
        
        // make enemy flash on damage
        GetComponent<EnemyFlashOnHit>().FlashColour();
    }
}
