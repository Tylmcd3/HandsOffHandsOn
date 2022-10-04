using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHealth;
    public int CurrHealth;
    //How long after a hit will the player regen
    public float RegenWaitTime;
    //How long it would take to regen from 0 - maxHealth
    public float RegenDuration;
    float TimeOfLastHit = 0;
    float LastRegen = 0;
    // Start is called before the first frame update
    void Start()
    {
        CurrHealth = MaxHealth;
        TimeOfLastHit = Time.time;

        LastRegen = Time.time;
    }
    void Update()
    {
        if(TimeOfLastHit > 0)
            TimeOfLastHit -= Time.deltaTime;
        if(LastRegen >= 0)
            LastRegen -= Time.deltaTime;
        if (TimeOfLastHit <= 0 && CurrHealth < MaxHealth)
            RegenHealth();
    }

    // Update is called once per frame
    public void Heal(int amount)
    {
        CurrHealth += amount;
        if (CurrHealth > MaxHealth) CurrHealth = MaxHealth;
    }

    public void TakeDamage(int Amount)
    {
        GetComponent<PlayerDamageFlash>().FlashScreen();
        CurrHealth -= Amount;
        TimeOfLastHit = RegenWaitTime;
        Debug.Log(CurrHealth);
        if (CurrHealth <= 0) Death();
    }
    //Left this as well cos im not sure if we will have health regen
    public void RegenHealth()
    {
        if(LastRegen <= 0)
        {
            CurrHealth++;
            LastRegen = 1;
            Debug.Log("CurrHealth is " + CurrHealth);
        }
            
       
    }

    //Unsure what we are going to do on death so im leaving this
    public void Death()
    {
        GameStateManager gameState = GameObject.Find("/GameManager").GetComponent<GameStateManager>();
        gameState.EndGame();
    }
}
