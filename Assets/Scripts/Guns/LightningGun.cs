using System;
using System.Collections;
using System.Collections.Generic;
using GunNameSpace;
using Spektr;
using Unity.Mathematics;
using UnityEngine;

public class LightningGun : MonoBehaviour
{
    [SerializeField] private GameObject lightning;
    [SerializeField] private Transform shootPoint;
    private Vector3 hitPoint;
    
    public List<Transform> lightningHits = new List<Transform>();
    
    // Shoots lightning (pow pow )
    public void ShootLightning(GunClass CurrentGunStruct, LayerMask weaponLayer, LayerMask enemyLayer)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f, ~weaponLayer))
        {
            CurrentGunStruct.MuzzleFlash.SetActive(true);
            CurrentGunStruct.Clip--;

            // visual effect at point hit
            
            // enemy damage
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                Collider[] enemies;
                // check area around enemy hit and find closest two enemies
                // TODO change this check
                if ((enemies = Physics.OverlapSphere(hit.transform.position, 7, enemyLayer)).Length > 1)
                {
                    lightningHits.Clear();
                    // TODO cap enemies hit
                    foreach (Collider c in enemies)
                    {
                        lightningHits.Add(c.transform);
                    }

                    hitPoint = hit.transform.position;
                    SpawnLightning(CurrentGunStruct.Damage);
                }
                // TODO calc damage correctly
                hitPoint = hit.transform.position;
                Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
                hit.transform.gameObject.GetComponent<EnemyDamageAndHealth>()
                    .DealDamage(CurrentGunStruct.Damage);
            }
        }
    }

    // sorts lightning hits for smoother lines
    private void SelectionSort()
    {
        int min;
        Transform temp;
        for (int i = 0; i < lightningHits.Count; i++)
        {
            min = i;
            for (int j = i; j < lightningHits.Count; j++)
            {
                // dist here
                if (Vector3.Distance(hitPoint, lightningHits[j].position) < Vector3.Distance(hitPoint, lightningHits[min].position))
                {
                    min = j;
                }
            }

            if (min != i)
            {
                // swap
                temp = lightningHits[i];
                lightningHits[i] = lightningHits[min];
                lightningHits[min] = temp;
            }
        }
    }

    // Spawns each lightning component then destroys in order
    private void SpawnLightning(int baseDamage)
    {
        // First sort the list 
        SelectionSort();
        // Add point to shoot lightning from gun
        lightningHits.Insert(0, shootPoint);
        // List of prefabs to spawn
        List<GameObject> prefabs = new List<GameObject>();
        
        // Loop through each lightning hit and spawn a prefab linking the hit and the next one
        // After that destroy each one after some time
        for (int i = 0; i < lightningHits.Count - 1; i++)
        {
            prefabs.Add(Instantiate(lightning, lightningHits[i].position, quaternion.identity));
            prefabs[i].GetComponent<LightningRenderer>().receiverTransform = lightningHits[i + 1];
            prefabs[i].GetComponent<LightningRenderer>().emitterTransform = lightningHits[i];
            
            float time = 0.35f + (i * 0.15f);
            // Destroy prefabs in order then deal damage
            StartCoroutine(DestroyLightning(time, prefabs[i]));

            int damage = baseDamage + (4 / (i + 1));
            StartCoroutine(LightningDamage(time - 0.15f, damage,
                lightningHits[i + 1].GetComponentInParent<EnemyDamageAndHealth>()));
        }
    }
    
    // Destroys lightning after some time
    IEnumerator DestroyLightning(float time, GameObject g)
    {
        yield return new WaitForSeconds(time);
        Destroy(g);
    }

    // Deals damage after some time
    IEnumerator LightningDamage(float time, int damage, EnemyDamageAndHealth enemy)
    {
        Debug.Log("Damage here");
        yield return new WaitForSeconds(time);
        enemy.DealDamage(damage);
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < lightningHits.Count; i++)
        {
            float colour = (float)i / (float)lightningHits.Count;
            Gizmos.color = new Color(colour, 0, 0);
            Gizmos.DrawSphere(lightningHits[i].position, 0.8f);
        }
    }
}
