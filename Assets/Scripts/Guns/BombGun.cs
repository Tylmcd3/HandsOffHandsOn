using System.Collections;
using System.Collections.Generic;
using GunNameSpace;
using Q3Movement;
using Unity.Mathematics;
using UnityEngine;

public class BombGun : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;

    [SerializeField] private float detonateTime = 2;
    
    [SerializeField] private float speed = 8;
    [SerializeField] private float upward = 0.8f;

    public void ShootBomb(GunClass CurrentGunStruct, LayerMask weaponLayer, LayerMask enemyLayer)
    {
        Vector3 direction = Camera.main.transform.rotation * Vector3.forward;
        
        direction.Normalize();
        direction.y += upward;
        
        CurrentGunStruct.MuzzleFlash.SetActive(true);
        CurrentGunStruct.Clip--;
        
        // setting up bomb
        GameObject bomb = Instantiate(bombPrefab, CurrentGunStruct.MuzzleFlash.transform.position, quaternion.identity);
        bomb.GetComponent<Bomb>().SetBomb(CurrentGunStruct.Damage, enemyLayer, detonateTime);
        bomb.GetComponent<Rigidbody>().AddForce(speed * direction, ForceMode.Impulse);
    }
}
