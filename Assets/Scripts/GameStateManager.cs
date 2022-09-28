using System;
using System.Collections;
using System.Collections.Generic;
using Q3Movement;
using UnityEngine;

// Manages game state such as pausing, winning, losing
public class GameStateManager : MonoBehaviour
{
    // Other objects can reference this to know if they should act
    public bool gameActive = false; // Default false

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject endMenu;

    // TODO which should we do for counting enemies?
    // Either have a list of all enemies (more costly)
    // Or just have an int that we increment/decrement (less overhead, less information)
    //public List<GameObject> enemies; // Global list of all enemies
    public int enemiesInScene; // Global count of all enemies
    WaveManager waveManager;
    // Start is called before the first frame update
    void Start()
    {
        enemiesInScene = 0;
        waveManager = GetComponent<WaveManager>();
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO change to escape, T is easier when debugging
        if (Input.GetKeyDown(KeyCode.T))
        {
            Pause();
        }
    }

    public void AddEnemy()
    {
        // ...
        enemiesInScene++;
    }

    public void RemoveEnemy()
    {
        waveManager.currentKills++;
        // ...
        enemiesInScene--;
    }

    public void StopGame()
    {
        Debug.Log("Paused!");
        // Disable most game logic
        gameActive = !gameActive;
        // Set time scale accordingly
        // Disables movement and physics checks
        Time.timeScale = Convert.ToInt32(gameActive);
        // Also disable player controller because of look at mouse
        GameObject.Find("Player").GetComponent<Q3PlayerController>().enabled = gameActive;
        // TODO still need to fully disable gunfire (tad buggy)
        Cursor.lockState = CursorLockMode.None;
    }

    // Sets game active to false, calls UI, etc
    public void Pause()
    {
        StopGame();
        // Load pause UI
        pauseMenu.SetActive(!gameActive);
        // ...
    }

    // Starts game off at first wave
    public void StartGame()
    {
        gameActive = true;
        Time.timeScale = 1;
        GameObject.Find("Player").GetComponent<Q3PlayerController>().enabled = true;
        waveManager.StartWave(1);
    }

    public void EndGame()
    {
        StopGame();
        // Check if player has won or lost and handle cleanup
        // Load endgameMenu etc
        endMenu.SetActive(true);
    }
}
