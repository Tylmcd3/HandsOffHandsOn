using System;
using System.Collections;
using System.Collections.Generic;
using GunNameSpace;
using Spektr;
using Unity.Mathematics;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;

    private Vector3 hitPoint;
    private bool shooting = false;

    private float timer = 0;
    private float fireRate;

    // how many instances before dealing damage
    [SerializeField] private int damageCount = 25;
    private int damageCounter = 0;

    private GameObject currentLaser;
    private void Update()
    {
        // if shooting start ticking down based on fire rate
        // with this the laser will self destroy when the shootlaser function stops being called
        if(shooting) timer -= Time.deltaTime;
        if (timer <= 0)
        {
            DestroyLaser();
        }
    }

    public void ShootLaser(GunClass CurrentGunStruct, LayerMask weaponLayer, Transform shoot)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f, ~weaponLayer))
        {
            shooting = true;
            timer = CurrentGunStruct.FireRate + 0.01f; // slight fudge just in case

            //CurrentGunStruct.MuzzleFlash.SetActive(true);
            CurrentGunStruct.Clip--;

            // create laser prefab if not already shooting
            if (!currentLaser)
            {
                // TODO not happy with this location
                SpawnLaser(shoot);
            }
            // set end point of laser
            currentLaser.GetComponent<LightningRenderer>().receiverPosition = hit.point;
            //shoot.rotation = Camera.main.transform.rotation;

            // enemy damage
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                // deal damage
                Debug.Log("" + damageCounter);
                
                damageCounter++;
                if (damageCounter >= damageCount)
                {
                    hit.transform.gameObject.GetComponent<EnemyDamageAndHealth>().DealDamage(CurrentGunStruct.Damage);
                    damageCounter = 0;
                }
            }
            
        }
    }

    private void SpawnLaser(Transform spawnPoint)
    {
        currentLaser = Instantiate(laserPrefab, transform.position, quaternion.identity);
        currentLaser.GetComponent<LightningRenderer>().emitterTransform = spawnPoint;
        shooting = true; 
    }
    public void DestroyLaser()
    {
        damageCounter = 0;
        shooting = false;
        Destroy(currentLaser);
    }
}
