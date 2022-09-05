using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // TODO Right now weights is static across waves
    // Need to change per wave
    public GameObject[] enemies; // List of possible enemy prefabs
    public float[] weights; // Weight charts for enemy spawns

    // Timer for enemy spawns, currently only spawns one enemy every interval
    public float timeToSpawn = 1;
    float timer;

    GameStateManager gsManager;
    WaveManager waveManager;

    // Start is called before the first frame update
    void Start()
    {
        // Find global game manager objects and assign components
        waveManager = GameObject.Find("GameManager").GetComponent<WaveManager>();
        gsManager = GameObject.Find("GameManager").GetComponent<GameStateManager>();

        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Only spawn enemies if a wave is active & game active 
        if (gsManager.gameActive && waveManager.waveActive)
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
        else
        {
            // Reset timer while wave/game is inactive
            timer = 0;
        }
    }

    void SpawnEnemy()
    {
        // Select a random enemy from enemies list based on weights
        float weight = Random.Range(0.0f, 1.0f);
        Debug.Log(weight);
        int toSpawn = 0;

        for (int i = 0; i < weights.Length; i++)
        {
            if (weight > weights[i])
            {
                weight -= weights[i];
            }
            else
            {
                // Spawn this enemy
                toSpawn = i;

                // Spawning logic here
                GameObject enemy = Instantiate(enemies[toSpawn], transform.position, transform.rotation);

                // For demo...
                enemy.GetComponent<Rigidbody>().AddForce(new Vector3(500, 0, 300));

                // Finally increment global enemy count
                gsManager.AddEnemy();
            }
        }

    }

    // Draw Enemy Spawn location for debugging
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position, 0.15f);
    }
}
