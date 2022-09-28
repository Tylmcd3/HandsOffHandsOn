using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAttack : MonoBehaviour
{
    private Vector3 hitPoint;
    private Vector3 direction;

    [SerializeField] private float attackDelay = 0.075f;

    // Calc position to shoot at and call shoot function
    public void RangedAttack(Vector3 target)
    {
        // TODO add muzzle effect and possibly draw line
        // Direction to shoot raycast
        direction = (target - transform.position).normalized;
        // TODO tweak the delay
        Invoke(nameof(Shoot), attackDelay); // delay shoot
    }
    // Invoked shoot function
    private void Shoot()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, direction);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            //Debug.Log("Hit " + hit.transform.gameObject.name);
            hitPoint = hit.point;
            // TODO damage player and do whatever else
            // ...
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                // TODO REMOVE THIS 
                hit.transform.gameObject.GetComponent<PlayerDamageFlash>().FlashScreen();
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
}
