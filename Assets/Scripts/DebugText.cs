using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoBehaviour
{
    public GameStateManager gsManager;
    public WaveManager waveManager;

    public Text currentWave;
    public Text waveTimer;
    public Text breakDebug;
    // Update is called once per frame
    void Update()
    {
        currentWave.text = "Wave: " + waveManager.currentWave.ToString();
        waveTimer.text = "Time: " + waveManager.currentTime.ToString("0.00");
        breakDebug.text = "In Break: " + (!waveManager.waveActive).ToString();
    }
}
