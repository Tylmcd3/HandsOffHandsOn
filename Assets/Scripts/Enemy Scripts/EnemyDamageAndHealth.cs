using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageAndHealth : MonoBehaviour
{
    private GameStateManager gsManager;
    private SpawnDamageText damageText;
    public int baseHealth;
    [SerializeField]
    int currHealth;
    private void Start()
    {
        GameObject manager = GameObject.Find("GameManager");
        gsManager = manager.GetComponent<GameStateManager>();

        damageText = GetComponent<SpawnDamageText>();
        
        currHealth = baseHealth;
    }
    void Death()
    {
        //Maybe Play animation
        // Delayed death to help deal with aftermath
        gameObject.SetActive(false);
        // find wave manager and notify of kill
        gsManager.RemoveEnemy();
        
        Invoke(nameof(Delayed), 4);

        // drop weapon
        GetComponent<EnemyDropWeapon>().DropWeapon();
    }

    void Delayed()
    {
        Destroy(gameObject);
    }

    // returns true if enemy has died
    public void DealDamage(int DamageDealt)
    {
        currHealth -= DamageDealt;
        damageText.SpawnText(DamageDealt);
        if (currHealth < 0) Death();
        
        // make enemy flash on damage
        GetComponent<EnemyFlashOnHit>().FlashColour();

    }
}
