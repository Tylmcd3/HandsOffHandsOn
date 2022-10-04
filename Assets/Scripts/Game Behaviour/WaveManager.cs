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
    public int totalKills = 10; // Default 10 enemies

    void Start()
    {
        // Default to 1st wave
        currentWave = 1;

        // Default to max time and 0 enemies killed
        currentTime = waveLength;
        currentKills = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Timer logic for time based waves
        // Only increment time if wave is currently active
        // if (waveActive) currentTime -= Time.deltaTime;
        // if (currentTime <= 0)
        // {
        //     waveActive = false;
        //     // Reset Timer
        //     currentTime = waveLength;
        //
        //     // End Wave
        //     EndWave();
        // }

        // Enemy spawn counter for enemy based waves
        // TODO Need some external method to increment currentKills
        if (currentKills >= totalKills)
        {
            waveActive = false;
            // Reset kill count
            currentKills = 0;

            // End Wave
            EndWave();
        }
    }

    public void PauseWave()
    {
        // ...
    }

    void Reset()
    {
        currentKills = 0;
        waveActive = false;
    }

    public void StartWave()
    {
        waveActive = true;
        // Whatever else needs to be done at start of wave
    }

    // Overload if we need to start at specific wave
    public void StartWave(int wave)
    {
        waveActive = true;
        currentWave = wave;
        // ...
    }
    public void EndWave()
    {
        // Grab enemy count from gamestatemanager
        // Don't continue until enemies == 0
        // only need to do this if doing time based

        // Finally, if currentWave == totalWaves, initiate win state (just ended last wave) No
        //if (currentWave == totalWaves)
        //{
        //    // ... 
        //    GetComponent<GameStateManager>().EndGame(true);
        //    Reset();
        //    return;
        //}
        // Otherwise start the break to move into next wave
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
        // TODO fix this
        //GetComponent<GameStateManager>().ClearWeapons();
        // Increment current wave and set wave to active
        currentWave++;
        StartWave();
    }
}
