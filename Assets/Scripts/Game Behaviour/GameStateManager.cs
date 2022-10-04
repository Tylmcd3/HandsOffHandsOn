using System;
using System.Collections;
using System.Collections.Generic;
using Q3Movement;
using TMPro;
using UnityEngine;

// Manages game state such as pausing, winning, losing
public class GameStateManager : MonoBehaviour
{
    // Other objects can reference this to know if they should act
    public bool gameActive = false; // Default false

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject endMenu;
    int totalGameKills; //Just used for score keeping/ end game screen
    private Guns playerGuns;
    public int weaponsOnGround;


    public int enemiesInScene; // Global count of all enemies
    WaveManager waveManager;
    
    // Start is called before the first frame update
    void Start()
    {
        playerGuns = GameObject.Find("Player").GetComponent<Guns>();
        enemiesInScene = 0;
        waveManager = GetComponent<WaveManager>();
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        // lose condition 1
        // if player is out of ammo and there are no weapon left on the ground
        //if (playerGuns.outOfAmmo && weaponsOnGround == 0)
        //{
        //    EndGame(false);
        //}
        // TODO change to escape, T is easier when debugging
        if (Input.GetKeyDown(KeyCode.T))
        {
            Pause();
        }
    }

    public void AddWeapon()
    {
        weaponsOnGround++;
    }

    public void RemoveWeapon()
    {
        weaponsOnGround--;
    }

    public void ClearWeapons()
    {
        GameObject[] weapons = (GameObject.FindGameObjectsWithTag("DroppedWeapon"));
        foreach (GameObject g in weapons)
        {
            Destroy(g);
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
        totalGameKills++;
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
        Cursor.visible = true;
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
    //Removed the book, we only end the game when they die
    public void EndGame()
    {
        StopGame();

        // Check if player has won or lost and handle cleanup
        // Load endgameMenu etc
        endMenu.SetActive(true);
        GameObject.Find("/Canvas/EndMenu/Buttons/Title").GetComponent<TextMeshProUGUI>().text = "You Have Died!\nYou Survived till round " + waveManager.currentWave + "\nand killed "+totalGameKills+ " Enemies";

       
    }
}
