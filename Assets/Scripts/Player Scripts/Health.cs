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
    float TimeOfLastHit;
    float LastRegen;
    // Start is called before the first frame update
    void Start()
    {
        CurrHealth = MaxHealth;
        TimeOfLastHit = Time.time;

        LastRegen = Time.time;
    }
    void Update()
    {
        if (Time.time - TimeOfLastHit > RegenWaitTime && CurrHealth < MaxHealth)
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
        CurrHealth -= Amount;
        TimeOfLastHit = Time.time;
        Debug.Log(CurrHealth);
        if (CurrHealth <= 0) Death();
    }
    //Left this as well cos im not sure if we will have health regen
    public void RegenHealth()
    {
        if(LastRegen +1 <Time.time)
        {
            CurrHealth++;
            LastRegen++;
            Debug.Log("CurrHealth is " + CurrHealth);
        }
            
       
    }

    //Unsure what we are going to do on death so im leaving this
    public void Death()
    {
        GameStateManager gameState = GameObject.Find("/GameManager").GetComponent<GameStateManager>();
        gameState.EndGame(false);
    }
}
