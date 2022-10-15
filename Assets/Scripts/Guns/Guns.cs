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

    // tracks how many guns still have ammo in them
    public bool outOfAmmo;

    // Very lazy
    [HideInInspector] public int currentAmmo;
    [HideInInspector] public int currentMaxAmmo;
    [HideInInspector] public int currentReserve;
    [HideInInspector] public float reloadTime;
    
    [HideInInspector] public bool reloading = false;
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
        
        currentAmmo = CurrentGunStruct.Clip;
        currentMaxAmmo = CurrentGunStruct.ClipSize;
        reloadTime = CurrentGunStruct.ReloadTime;
    }
    public void ChangeWeapon(GunEnum weaponToChange)
    {
        // disable muzzle
        CurrentGunStruct.MuzzleFlash.SetActive(false);
        for (int i = 0; i < Inventory.Count; i++)
        {
            if (Inventory[i] == weaponToChange)
            {
                currentSlot = i;
                break;
            }
        }

        reloading = false;
        reloadTime = CurrentGunStruct.ReloadTime;
        CurrentGunStruct  = GunStore.SwapGun(CurrentGun, CurrentGunStruct, weaponToChange);
        CurrentGun = weaponToChange;
        currentMaxAmmo = CurrentGunStruct.ClipSize;
    }
    void ChangeToNextGun()
    {
        // check ammo
        for (int i = 0; i < Inventory.Count; i++)
            if (Inventory[i] != CurrentGun && Inventory[i] != GunEnum.NoGun)
            {
                currentSlot = i;
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
            StartCoroutine(setReloadBool(CurrentGunStruct.ReloadTime));
            //TODO: Play reload anim (Might just move gun down idk)
        }
        else if (CurrentGunStruct.CurrReserveAmmo > 0)
        {
            TimeTillNextFire = CurrentGunStruct.ReloadTime;
            CurrentGunStruct.Clip = CurrentGunStruct.CurrReserveAmmo;
            CurrentGunStruct.CurrReserveAmmo = 0;
            StartCoroutine(setReloadBool(CurrentGunStruct.ReloadTime));
        }
        else
        {
            outOfAmmo = CheckAmmo();
            ChangeToNextGun();
        }
    }

    IEnumerator setReloadBool(float time)
    {
        reloading = true;
        yield return new WaitForSeconds(time);
        reloading = false;
    }

    // returns true if all guns are out of ammo
    private bool CheckAmmo()
    {
        int comp = 0;
        foreach (GunEnum gun in Inventory)
        {
            if (gun != GunEnum.NoGun)
            {
                if (GunStore.guns[(int)gun - 1].Clip == 0)
                {
                    comp++;
                }
            }
            else
            {
                comp++;
            }
        }
        return comp == Inventory.Count;
    }

    // Fully reloads weapons clip to clipsize + reserve
    public void FullReload()
    {
        outOfAmmo = false;
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
                    case GunEnum.LaserGun:
                        GetComponent<LaserGun>().ShootLaser(CurrentGunStruct, weaponLayer, GunStore.LaserGunModel.transform);
                        break;
                    case GunEnum.BombGun:
                        GetComponent<BombGun>().ShootBomb(CurrentGunStruct, weaponLayer, enemyLayer);
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
        // mouse scrolling
        // VERY LAZY NO JUDGE AM VERY TIRED
        int mouseScroll = (int)Input.mouseScrollDelta.y;

        if (mouseScroll != 0)
        {
            if (currentSlot == 0)
            {
                if (mouseScroll <= -1)
                {
                    if (Inventory[2] != GunEnum.NoGun)
                    {
                        currentSlot = 2;
                        ChangeWeapon(Inventory[2]);
                    }
                    else if (Inventory[1] != GunEnum.NoGun)
                    {
                        currentSlot = 1;
                        ChangeWeapon(Inventory[1]);
                    }
                }

                if (mouseScroll >= 1 && Inventory[1] != GunEnum.NoGun)
                {
                    currentSlot = 1;
                    ChangeWeapon(Inventory[1]);
                }

                mouseScroll = 0;
            }
            else if (currentSlot == 1)
            {
                if (mouseScroll <= -1)
                {
                    currentSlot = 0;
                    ChangeWeapon(Inventory[0]);
                }

                if (mouseScroll >= 1)
                {
                    if (Inventory[2] != GunEnum.NoGun)
                    {
                        currentSlot = 2;
                        ChangeWeapon(Inventory[2]);
                    }
                    else
                    {
                        currentSlot = 0;
                        ChangeWeapon(Inventory[0]);
                    }
                }

                mouseScroll = 0;
            }
            else if (currentSlot == 2)
            {
                if (mouseScroll <= -1)
                {
                    currentSlot = 1;
                    ChangeWeapon(Inventory[1]);
                }
                
                if (mouseScroll >= 1)
                {
                    currentSlot = 0;
                    ChangeWeapon(Inventory[0]);
                }

                mouseScroll = 0;
            }
        }
            
        TimeTillNextFire -= Time.deltaTime; 
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (Inventory[0] != GunEnum.NoGun)
            {
                ChangeWeapon(Inventory[0]);
                currentSlot = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (Inventory[1] != GunEnum.NoGun)
            {
                ChangeWeapon(Inventory[1]);
                currentSlot = 1;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (Inventory[2] != GunEnum.NoGun)
            {
                ChangeWeapon(Inventory[2]);
                currentSlot = 2;
            }
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if(TimeTillNextFire <= 0)
            {
                TimeTillNextFire = CurrentGunStruct.FireRate;

                FireCurrWeapon();
            }
           
        }

        currentAmmo = CurrentGunStruct.Clip;
        currentReserve = CurrentGunStruct.CurrReserveAmmo;
    }

}

