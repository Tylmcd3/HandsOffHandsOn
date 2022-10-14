using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGlow : MonoBehaviour
{
    private Outline outline;

    private WaveManager waveManager;
    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<Outline>();
        waveManager = GameObject.Find("GameManager").GetComponent<WaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waveManager.EnemiesToSpawn - waveManager.currentKills <= 3)
        {
            outline.enabled = true;
        }
        else
        {
            outline.enabled = false;
        }
    }
}
