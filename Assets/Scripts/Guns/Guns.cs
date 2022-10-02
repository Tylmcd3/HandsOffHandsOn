using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunNameSpace;

public class Guns : MonoBehaviour
{
    GunValues GunStore;
    // max number of guns in inventory
    public int maxGuns = 3;
    public List<GunEnum> Inventory;
    public GunEnum StartingWeapon;
    public int currentSlot = 0;
    GunEnum CurrentGun;
    GunClass CurrentGunStruct = null;

    float TimeTillNextFire = 0;
    
    private Vector3 hitPoint;
    private LayerMask weaponLayer;
    private LayerMask enemyLayer;

    // Start is called before the first frame update

    void Start()
    {
        Inventory = new List<GunEnum>(maxGuns);
        // initialize inventory
        for (int i = 0; i < maxGuns; i++)
        {
            Inventory.Insert(i, GunEnum.NoGun);
        }
        Inventory.Insert(0, StartingWeapon);

        // need to ignore dropped weapons for raycast
        weaponLayer = LayerMask.GetMask("Weapon");
        enemyLayer = LayerMask.GetMask("Enemy");
        GunStore = GetComponent<GunValues>();

        CurrentGun = StartingWeapon;
        CurrentGunStruct = GunStore.SwapGun(GunEnum.NoGun, null, StartingWeapon);
    }
    public void ChangeWeapon(GunEnum weaponToChange)
    {
        // disable muzzle
        CurrentGunStruct.MuzzleFlash.SetActive(false);

        CurrentGunStruct  = GunStore.SwapGun(CurrentGun, CurrentGunStruct, weaponToChange);
        CurrentGun = weaponToChange;
    }
    void ChangeToNextGun()
    {
        for (int i = 0; i < Inventory.Count; i++)
            if (Inventory[i] != CurrentGun && Inventory[i] != GunEnum.NoGun)
            {
                ChangeWeapon(Inventory[i]);
                break;
            }
    }
    void Reload()
    {
        //Debug.Log(CurrentGunStruct.CurrReserveAmmo);
        if (CurrentGunStruct.CurrReserveAmmo > CurrentGunStruct.ClipSize)
        {
            TimeTillNextFire = CurrentGunStruct.ReloadTime;
            CurrentGunStruct.Clip = CurrentGunStruct.ClipSize;
            CurrentGunStruct.CurrReserveAmmo -= CurrentGunStruct.Clip;
            //TODO: Play reload anim (Might just move gun down idk)
        }
        else if (CurrentGunStruct.CurrReserveAmmo > 0)
        {
            TimeTillNextFire = CurrentGunStruct.ReloadTime;
            CurrentGunStruct.Clip = CurrentGunStruct.CurrReserveAmmo;
            CurrentGunStruct.CurrReserveAmmo = 0;
        }
        else
        {
            ChangeToNextGun();
        }
    }

    // Fully reloads weapons clip to clipsize + reserve
    public void FullReload()
    {
        CurrentGunStruct.Clip = CurrentGunStruct.ClipSize;
        CurrentGunStruct.CurrReserveAmmo = CurrentGunStruct.ReserveAmmoStart;
    }
    void FireCurrWeapon()
    {
        //If Have Ammo in the clip
        if (CurrentGunStruct.Clip > 0)
        {
            // If its a normal gun continue
            if (!CurrentGunStruct.SpecialBehaviour)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100.0f, ~weaponLayer))
                {
                    CurrentGunStruct.MuzzleFlash.SetActive(true);
                    CurrentGunStruct.Clip--;
                    //Debug.Log(CurrentGunStruct.Clip);

                    // visual effect at point hit

                    // enemy damage
                    if (hit.transform.gameObject.CompareTag("Enemy"))
                    {
                        //Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
                        hit.transform.gameObject.GetComponent<EnemyDamageAndHealth>()
                            .DealDamage(CurrentGunStruct.Damage);
                    }
                }
            }
            else
            {
                switch (CurrentGun)
                {
                    case GunEnum.LightningGun:
                        GetComponent<LightningGun>().ShootLightning(CurrentGunStruct, weaponLayer, enemyLayer);
                        break;
                }
            }
            // reload
            if (CurrentGunStruct.Clip <= 0)
            {
                Reload();
            }
        }
        //If Picked up Amnmo
        else if(CurrentGunStruct.CurrReserveAmmo > 0)
        {
            Reload();
        }
        else
        {
            ChangeToNextGun();
        }
    }
    // Update is called once per frame
    // Update is called once per frame
    void Update()
    {
        TimeTillNextFire -= Time.deltaTime; 
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeWeapon(Inventory[0]);
            currentSlot = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeWeapon(Inventory[1]);
            currentSlot = 1;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeWeapon(Inventory[2]);
            currentSlot = 2;
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if(TimeTillNextFire <= 0)
            {
                TimeTillNextFire = CurrentGunStruct.FireRate;

                FireCurrWeapon();
            }
           
        }
    }

}

