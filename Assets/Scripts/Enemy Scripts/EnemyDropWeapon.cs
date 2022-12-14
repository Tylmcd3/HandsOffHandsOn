using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyDropWeapon : MonoBehaviour
{
    // assign in inspector for now
    [SerializeField] private GameObject weaponToDrop;
    private GameStateManager gsManager;
    private bool dropWeapon = true;

    private void Start()
    {
        gsManager = GameObject.Find("GameManager").GetComponent<GameStateManager>();
    }
    public void DropWeapon()
    {
        // guarentees drop if no weapons on ground
        if (gsManager.weaponsOnGround == 0 || (dropWeapon && gsManager.weaponsOnGround < 4))
        {
            Rigidbody rb = Instantiate(weaponToDrop, transform.position, Quaternion.Euler(0, 0, 90)).GetComponent<Rigidbody>();
            //GameObject weapon = Instantiate(weaponToDrop, transform.position, Quaternion.Euler(0, 0, 90));
            // give it a bit of force (and a bit of spin)
            // TODO make this spin more exciting
            rb.AddForce(0, 150, 0);
            rb.AddTorque(50, 0, 200);
            
            gsManager.AddWeapon();
        }
    }

    public void AssignWeaponToDrop(bool drop, GameObject type)
    {
        dropWeapon = drop;
        weaponToDrop = type;
    }
}
