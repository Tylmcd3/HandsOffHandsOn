using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemy;
    public float timeBetweenSpawns = 1f;
    public Transform player;

    float timeUntilNextSpawn = 0f;

    new BoxCollider collider;

	void Awake()
    {
        collider = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (timeUntilNextSpawn <= Time.timeSinceLevelLoad)
        {
            timeUntilNextSpawn = Time.timeSinceLevelLoad + timeBetweenSpawns;

            Vector3 startingPosition = collider.bounds.center + new Vector3(collider.bounds.extents.x * Random.Range(-1f, 1f), GetComponent<Collider>().bounds.extents.y * Random.Range(-1f, 1f), 0);

            GameObject enemyGO = GameObject.Instantiate(enemy);
            enemyGO.transform.position = startingPosition;

            Vector2 directionToPlayer = (player.transform.position - startingPosition).normalized;

            enemyGO.GetComponent<Rigidbody>().velocity = directionToPlayer;

        }		
	}
}
