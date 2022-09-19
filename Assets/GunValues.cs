using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunNameSpace;

namespace GunNameSpace
{
    public class GunClass
    {
        public bool Usable { get; set; }
        public int Damage { get; set; }
        public int ClipSize { get; set; }
        public int Clip { get; set; }
        public int ReserveAmmoStart { get; set; }
        public int CurrReserveAmmo { get; set; }
        public string AnimatorName { get; set; }
        public float FireRate { get; set; }
        public GameObject MuzzleFlash { get; set; }
        public float ReloadTime { get; set; }
        public GunClass(int damage, int clip, int reserve, string anim, float fireRate, float reloadTime, bool usable)
        {
            Damage = damage;
            ClipSize = clip;
            Clip = clip;
            ReserveAmmoStart = reserve;
            CurrReserveAmmo = reserve;
            AnimatorName = anim;
            FireRate = fireRate;
            ReloadTime = reloadTime;
            Usable = usable;
        }

    }
    public enum GunEnum
    {
        NoGun, GoldenGun, AK47
    }
    
}
public class GunValues : MonoBehaviour
{
    public GameObject AKMuzzleFlash;
    public GameObject GGMuzzleFlash;
    public GameObject GoldenGunModel;
    public GunClass newGun;
    public GunClass GoldenGun = new GunClass(10, 1, 10, "GoldenGun", 0.3f, 2);
    
    public GameObject AK47Model;
    public GunClass AK47 = new GunClass(2, 25, 50, "AK47",0.1f, 2);
    private void Start()
    {
        GoldenGun.MuzzleFlash = GGMuzzleFlash;
        AK47.MuzzleFlash = AKMuzzleFlash;
    }
    public GunClass SwapGun(GunEnum GunToRemoveEnum, GunClass GunToRemoveStruct, GunEnum Get)
    {
        GunClass GetGunStruct;
        switch (GunToRemoveEnum) 
        {
            case GunEnum.GoldenGun:
                GoldenGun = GunToRemoveStruct;
                GoldenGunModel.SetActive(false);
                break;
            case GunEnum.AK47:
                AK47 = GunToRemoveStruct;
                AK47Model.SetActive(false);
                break;
            case GunEnum.NoGun:
                break;
        }

        switch (Get)
        {
            case GunEnum.GoldenGun:
                GetGunStruct = GoldenGun;
                GoldenGunModel.SetActive(true);
                break;
            case GunEnum.AK47:
                GetGunStruct = AK47;
                AK47Model.SetActive(true);
                break;
            default:
                GetGunStruct = null;
                break;
        }
        return GetGunStruct;
    }
}