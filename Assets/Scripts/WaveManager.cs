using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages wave count, current wave, and state/conditions for wave advancement
public class WaveManager : MonoBehaviour
{
    // Break time in between waves
    public float timeBetweenWaves = 10; // Default 10 seconds
    public int totalWaves = 5; // Default 5 waves
    public bool waveActive = false;
    public int currentWave;

    // Used if doing time based waves
    public float currentTime;
    public float waveLength = 60; // Default 60 seconds 

    // Used if doing enemy spawn based waves
    public int currentKills;
    public int totalKills = 30; // Default 50 enemies

    void Start()
    {
        // Default to 0th wave
        currentWave = 0;

        // Default to 0 time and 0 enemies killed
        currentTime = 0;
        currentKills = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Timer logic for time based waves
        // Only increment time if wave is currently active
        if (waveActive) currentTime += Time.deltaTime;
        if (currentTime >= waveLength)
        {
            // End Wave
            EndWave();

            waveActive = false;
            // Reset Timer
            currentTime = 0;
        }

        // Enemy spawn counter for enemy based waves
        // Need some external method to increment currentKills
        if (currentKills >= totalKills)
        {
            // End Wave
            EndWave();

            waveActive = false;
            // Reset kill count
            currentKills = 0;
        }
    }
    public void EndWave()
    {
        // Need to ensure all enemies are killed, and deactivate spawners

        // First deactivate spawners 
        // To deactivate spawners have each spawner reference the wave manager and check for waveactive

        // Then either kill all active enemies or create a list of enemies that need be 
        // Killed before wave can advance

        StartCoroutine(Break());
    }

    // Coroutine for the break between waves
    IEnumerator Break()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        AdvanceWave();
    }

    // Advance wave
    public void AdvanceWave()
    {
        // Increment current wave and set wave to active
        currentWave++;
        waveActive = true;
    }
}
