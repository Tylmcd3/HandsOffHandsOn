using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages game state such as pausing, winning, losing
public class GameStateManager : MonoBehaviour
{
    // Other objects can reference this to know if they should act
    public bool gameActive = false; // Default false
    WaveManager waveManager;
    // Start is called before the first frame update
    void Start()
    {
        waveManager = GetComponent<WaveManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Sets game active to false, calls UI?
    void Pause()
    {
        gameActive = false;
    }

    // Lose state here
    void Lose()
    {

    }

    // Win state here
    void Win()
    {

    }
}
