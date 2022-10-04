using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

[Serializable]
public class weightChart
{
    public float[] weights2;
}
public class EnemySpawner : MonoBehaviour
{
    // TODO Right now weights is static across waves
    // Need to change per wave
    public GameObject[] enemies; // List of possible enemy prefabs
    public float[] weights; // Weight charts for enemy spawns

    [Range(0, 1.0f)] [SerializeField] private float chanceToDropWeapon = 0.6f;

    // Timer for enemy spawns, currently only spawns one enemy every interval
    public float timeToSpawn = 1;
    private float timer;

    private GameStateManager gsManager;
    private WaveManager waveManager;
    
    private List<GameObject> weaponsList;

    public weightChart[] weightChart;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameManager = GameObject.Find("GameManager");
        // Find global game manager objects and assign components
        waveManager = gameManager.GetComponent<WaveManager>();
        gsManager = gameManager.GetComponent<GameStateManager>();
        weaponsList = gameManager.GetComponent<EnemyManager>().allDroppedWeapons;

        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // // Only spawn enemies if a wave is active & game active 
        if (gsManager.gameActive && waveManager.waveActive)
        {
            // only want to spawn if wavemanager.totalkills - enemiesInScene > numberSpawned
            if ((waveManager.EnemiesToSpawn - gsManager.enemiesInScene - waveManager.currentKills) >= 1)
            {
                // TODO this should probably be switched to coroutines so that we can do multiple per interval
                // Also only increment timer if need be
                timer += Time.deltaTime;
                if (timer >= timeToSpawn)
                {
                    // Reset timer
                    timer = 0;
                    SpawnEnemy();
                }  
            }
        }
        else
        {
            // Reset timer while wave/game is inactive
            timer = 0;
        }
        
        // TEMP DEBUG
        // timer += Time.deltaTime;
        // if (timer >= timeToSpawn)
        // {
        //     // Reset timer
        //     timer = 0;
        //     SpawnEnemy();
        // }  
    }

    void SpawnEnemy()
    {
        EnemyDropWeapon DropWeaponScript;
        // Select a random enemy from enemies list based on weights
        float weight = Random.Range(0.0f, 1.0f);
        int EnemyToSpawn = Random.Range(0, enemies.Length);

        // Random weapon (evenly distributed)
        int weapon = Random.Range(0, weaponsList.Count);
        
        // now need chance to drop weapon, not always true! (currently 60% chance to drop
        bool dropWeapon = Random.Range(0, 1.0f) < chanceToDropWeapon;
        
        //Debug.Log(weight);
        int toSpawn = 0;
        //Commented this out just for setting up, might leave like this just so its a random spawn
        //for (int i = 0; i < weights.Length; i++)
       // {
            //if (weight > weights[i])
            //{
            //    weight -= weights[i];
            //}
            //else
            //{
                // Spawn this enemy
                toSpawn = EnemyToSpawn;

                // Spawning logic here
                GameObject enemy = Instantiate(enemies[toSpawn], transform.position, transform.rotation);
                
                enemy.GetComponent<EnemyDropWeapon>().AssignWeaponToDrop(dropWeapon, weaponsList[weapon]);

                // Finally increment global enemy count
                gsManager.AddEnemy();
            //}
        //}

    }

    // Draw Enemy Spawn location for debugging
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position, 0.3f);
    }
}
