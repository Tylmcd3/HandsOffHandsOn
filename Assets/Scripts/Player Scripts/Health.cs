using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHealth;
    public int CurrHealth;
    // Start is called before the first frame update
    void Start()
    {
        CurrHealth = MaxHealth;
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
        Debug.Log(CurrHealth);
        if (CurrHealth <= 0) Death();
    }
    //Left this as well cos im not sure if we will have health regen
    public void RegenHealth(int TimeToRegen)
    {

    }
    //Unsure what we are going to do on death so im leaving this
    public void Death()
    {
        GameStateManager gameState = GameObject.Find("/GameManager").GetComponent<GameStateManager>();
        gameState.EndGame();
    }
}
