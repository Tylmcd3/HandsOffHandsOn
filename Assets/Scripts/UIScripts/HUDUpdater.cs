using System.Collections;
using System.Collections.Generic;
using GunNameSpace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDUpdater : MonoBehaviour
{
    private GameStateManager gsManager;
    private WaveManager waveManager;

    private Guns playerGuns;
    private Health playerHealth;

    [SerializeField] private string waveText = "WAVE ";
    [SerializeField] private string enemyText = " enemies left";
    
    [SerializeField] private TextMeshProUGUI waveCount;

    [SerializeField] private TextMeshProUGUI enemyCount;

    [SerializeField] private Slider health;

    [SerializeField] private Slider ammo;

    [SerializeField] private Image[] slots;

    [SerializeField] private Color selectedColour;

    private Color defaultColour;

    [SerializeField] private Sprite[] guns;

    private Vector3 smallScale = new Vector3(0.8f, 0.8f, 0.8f);

    private Vector3 originalScale;
    // Start is called before the first frame update
    void Start()
    {
        gsManager = GameObject.Find("GameManager").GetComponent<GameStateManager>();
        waveManager = GameObject.Find("GameManager").GetComponent<WaveManager>();
        playerGuns = GameObject.Find("Player").GetComponent<Guns>();
        playerHealth = GameObject.Find("Player").GetComponent<Health>();

        defaultColour = slots[1].color;
        originalScale = slots[1].gameObject.transform.localScale;

        health.maxValue = playerHealth.MaxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        waveCount.text = waveText + waveManager.currentWave.ToString();
        enemyCount.text = (waveManager.waveActive)? (waveManager.EnemiesToSpawn - waveManager.currentKills).ToString() + enemyText : "";
        health.value = playerHealth.CurrHealth;
        ammo.maxValue = playerGuns.currentMaxAmmo;
        ammo.value = playerGuns.currentAmmo;
        
        // guns stuff
        // this is hardcoded as 3 becaused rushed
        for (int i = 0; i < 3; i++)
        {
            slots[i].sprite = guns[(int)playerGuns.Inventory[i]];
            
            if (i == playerGuns.currentSlot)
            {
                slots[i].gameObject.transform.localScale = originalScale;
                slots[i].color = selectedColour;
            }
            else
            {
                slots[i].gameObject.transform.localScale = smallScale;
                slots[i].color = defaultColour;
            }
        }
        
    }
}
