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
    private RangedEnemyAttack RangedAttackScript;
    private EnemyMeleeAttack ChaseAttackScript;
    public bool ChasingEnemy = false;
        
    // Attack logic
    [SerializeField] private float attackTime = 1;
    private bool attacked;

    public float attackRange;
    public bool targetInRange;
    
    private LayerMask playerLayer;
    
    // Start is called before the first frame update
    void Start()
    {
        playerLayer = LayerMask.GetMask("Player");
        // Will find gameObject with name "Player"
        target = GameObject.Find("Player").GetComponent<Transform>();
        if (!target) target = transform; // just in case
        
        enemy = GetComponent<NavMeshAgent>();
        if(ChasingEnemy)
            ChaseAttackScript = GetComponent<EnemyMeleeAttack>();
        else
            RangedAttackScript = GetComponent<RangedEnemyAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        targetInRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        
        if(!targetInRange) Chase();
        if (targetInRange)
            if (ChasingEnemy)
                AttackChase();
            else
                AttackRanged();

    }
    
    // chase target
    void Chase()
    {
        // lazy, run straight to player pos
        enemy.SetDestination(target.position);
    }
    
    // attack target
    void AttackRanged()
    {
        // stop enemy in place
        enemy.SetDestination(transform.position);
        
        transform.LookAt(target);
        // attack from range
        if (!attacked)
        {
            // Attack logic
            RangedAttackScript.RangedAttack(target.position);
            // ...
            attacked = true;
            Invoke(nameof(ResetAttack), attackTime);
        }
    }
    void AttackChase()
    {
        // attack from range
        if (!attacked)
        {
            // Attack logic
            ChaseAttackScript.ChaseAttack();
            // ...
            attacked = true;
            Invoke(nameof(ResetAttack), attackTime);
        }
    }

    private void ResetAttack()
    {
        attacked = false;
    }

    // stops enemy for time
    public IEnumerator Stop(float time)
    {
        float temp = enemy.speed;
        enemy.speed = 0;
        yield return new WaitForSeconds(time);
        enemy.speed = temp;
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
    
    // private void OnDrawGizmosSelected()
    // {
    //     // Gizmos.color = Color.red;
    //     // Gizmos.DrawWireSphere(transform.position, attackRange);
    // }
}
