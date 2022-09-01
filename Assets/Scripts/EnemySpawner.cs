using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies; // List of possible enemy prefabs
    public float[][] weights; // Weight charts for enemy spawns

    GameStateManager gsManager;
    WaveManager waveManager;
    // Start is called before the first frame update

    public struct Enemy
    {
        public Enemy(GameObject p, float[] w) : this()
        {
            this.prefab = p;
            this.weights = w;
        }
        GameObject prefab { get; set; }
        float[] weights { get; set; }
    }
    public Enemy[] help;

    void Start()
    {
        // Find global game manager objects and assign components
        waveManager = GameObject.Find("GameManager").GetComponent<WaveManager>();
        gsManager = GameObject.Find("GameManager").GetComponent<GameStateManager>();

        help = new Enemy[enemies.Length];

        // Weights chart init
        for (int i = 0; i < enemies.Length; i++)
        {
            help[i] = new Enemy(enemies[i], weights[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Only spawn enemies if a wave is active & game active
        if (gsManager.gameActive && waveManager.waveActive)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        // Spawning logic here
    }
}
