using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryDebug : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI slot1;
    [SerializeField] private TextMeshProUGUI slot2;
    [SerializeField] private TextMeshProUGUI slot3;

    [SerializeField] private Guns player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slot1.SetText("1: " + player.Inventory[0].ToString());
        slot2.SetText("2: " + player.Inventory[1].ToString());
        slot3.SetText("3: " + player.Inventory[2].ToString());
    }
}
