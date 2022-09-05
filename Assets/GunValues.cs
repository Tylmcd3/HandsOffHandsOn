using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct GunStruct
{
    public int Damage { get; set; }
    public int Clip { get; set; }
    public int ReserveAmmo { get; set; }
    public string AnimatorName { get; set; }
    public GunStruct(int damage, int clip, int reserve, string anim)
    {
        Damage = damage;
        Clip = clip;
        ReserveAmmo = reserve;
        AnimatorName = anim;
    }

}
public enum GunEnum
{
    GoldenGun, AK47
}
public class GunValues : MonoBehaviour
{
    public GunStruct GoldenGun;
    public GunStruct AK47;
    public GunStruct GetGun(GunEnum Gun)
    {
        return Gun switch
        {
            GunEnum.GoldenGun => GoldenGun,
            GunEnum.AK47 => AK47,
            _ => GoldenGun

        };
    }
}
