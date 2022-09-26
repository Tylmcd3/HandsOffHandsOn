using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/*
 * Code adapted from https://www.youtube.com/watch?v=UjkSFoLxesw
 */
public class RangedEnemyNavigation : MonoBehaviour
{
    // Navigation
    [SerializeField]
    private Transform target;
    private NavMeshAgent enemy;
        
    // Attack logic
    private float attackTime;
    private bool attacked;

    public float attackRange;
    public bool targetInRange;
    [SerializeField]
    private LayerMask playerLayer;
    // Start is called before the first frame update
    void Start()
    {
        playerLayer = LayerMask.GetMask("Player");
        // Will find gameObject with name "Player"
        target = GameObject.Find("Player").GetComponent<Transform>();
        if (!target) target = transform; // just in case
        
        enemy = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        targetInRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        
        if(!targetInRange) Chase();
        if (targetInRange) Attack();

    }
    
    // chase target
    void Chase()
    {
        // lazy, run straight to player pos
        enemy.SetDestination(target.position);
    }
    
    // attack target
    void Attack()
    {
        // stop enemy in place
        enemy.SetDestination(transform.position);
        
        transform.LookAt(target);
        // attack from range
        if (!attacked)
        {
            // Attack logic
            // ...
            attacked = true;
            Invoke(nameof(ResetAttack), attackTime);
        }
    }

    private void ResetAttack()
    {
        attacked = false;
    }

    public void SetSpeed(float speed)
    {
        enemy.speed = speed;
    }
    
    public void SetSpeed(float speed, float accel)
    {
        enemy.speed = speed;
        enemy.acceleration = accel;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
