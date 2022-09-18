using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    // Navigation
    [SerializeField]
    private Transform target;
    private NavMeshAgent enemy;

    // Start is called before the first frame update
    void Start()
    {
        // Will find gameObject with name "Player"
        target = GameObject.Find("Player").GetComponent<Transform>();
        if (!target) target = transform; // just in case
        
        enemy = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // lazy, run straight to player pos
        enemy.SetDestination(target.position);
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
}
