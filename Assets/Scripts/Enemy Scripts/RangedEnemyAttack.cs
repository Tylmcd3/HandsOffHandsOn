using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RangedEnemyAttack : MonoBehaviour
{
    private Vector3 hitPoint;
    private Vector3 direction;
    public int enemyDamage;

    [SerializeField] private float attackDelay = 0.075f;
    [SerializeField] private TrailRenderer trailRenderer;

    // Calc position to shoot at and call shoot function
    public void RangedAttack(Vector3 target)
    {
        // TODO add muzzle effect
        // Direction to shoot raycast
        direction = (target - transform.position).normalized;
        Invoke(nameof(Shoot), attackDelay); // delay shoot
    }
    // Invoked shoot function
    private void Shoot()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, direction);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            TrailRenderer trail = Instantiate(trailRenderer, transform.position, quaternion.identity);
            if(gameObject.activeSelf) StartCoroutine(SpawnTrail(hit, trail));
            //Debug.Log("Hit " + hit.transform.gameObject.name);
            hitPoint = hit.point;
            // TODO damage player and do whatever else
            // ...
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                // TODO REMOVE THIS 
                hit.transform.gameObject.GetComponent<Health>().TakeDamage(enemyDamage);
            }
        }
    }
    // debug gizmos!
    private void OnDrawGizmos()
    {
        // Gizmos.color = Color.red;
        // Gizmos.DrawSphere(hitPoint, 0.3f);
        // Gizmos.DrawLine(transform.position, hitPoint);
    }
    private IEnumerator SpawnTrail(RaycastHit hit, TrailRenderer trail)
    {
        float time = 0;
        Vector3 startPos = trail.transform.position;

        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPos, hit.point, time);
            time += Time.deltaTime / trail.time;

            yield return null;
        }

        trail.transform.position = hit.point;
        
        Destroy(trail.gameObject, trail.time);
    }
}
