using System.Collections;
using System.Collections.Generic;
using GunNameSpace;
using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
    private GameStateManager gsManager;
    
    private Guns playerGuns;
    private DetectWeapon detect;

    private bool pickedUp = false;

    private float timer = 0;

    [SerializeField] private float timeToPickup = 4;
    // Start is called before the first frame update
    void Start()
    {
        gsManager = GameObject.Find("GameManager").GetComponent<GameStateManager>();
        playerGuns = GetComponent<Guns>();
        detect = GetComponent<DetectWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        // capping how often player can pick up weapons
        if (pickedUp) timer += Time.deltaTime;
        if (timer >= timeToPickup)
        {
            timer = 0;
            pickedUp = false;
        }
        // Default key to pickup weapon is E
        if (!pickedUp && Input.GetKeyDown(KeyCode.E))
        {
            // if theres a weapon to pick up...
            if (detect.selected)
            {
                pickedUp = true;
                // ...
                GunEnum type = detect.selected.GetComponent<DroppedWeaponValues>().type;
                
                // check slots of inventory and find first slot we can put in
                int gunIndex = FindGunSlot(type);
                // if there is room in inventory then put gun in that slot and then switch to that weapon
                // if there is no room then swap the gun currently held for weapon on ground
                
                playerGuns.Inventory[gunIndex] = type;
                // and swap to that weapon
                playerGuns.ChangeWeapon(type);
                
                // reload weapon
                playerGuns.FullReload();
                
                // Finally destroy the picked up weapon
                detect.RemoveSelected();

                gsManager.RemoveWeapon();
            }
        }
    }
    
    // Find first empty gun slot in inventory or if the gun type is the same
    int FindGunSlot(GunEnum type)
    {
        for (int i = 0; i < playerGuns.maxGuns; i++)
        {
            if (playerGuns.Inventory[i] == GunEnum.NoGun || playerGuns.Inventory[i] == type) return i;
        }

        return playerGuns.currentSlot;
    }
}
