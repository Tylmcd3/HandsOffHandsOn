using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyDropWeapon : MonoBehaviour
{
    // assign in inspector for now
    // TODO assign in method
    [SerializeField] private GameObject weaponToDrop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DropWeapon()
    {
        Rigidbody rb = Instantiate(weaponToDrop, transform.position, Quaternion.Euler(0, 0, 90)).GetComponent<Rigidbody>();
        //GameObject weapon = Instantiate(weaponToDrop, transform.position, Quaternion.Euler(0, 0, 90));
        // give it a bit of force (and a bit of spin)
        // TODO make this spin more exciting
        rb.AddForce(0, 100, 0);
        rb.AddTorque(50, 0, 200);
    }

    public void AssignWeaponToDrop(GameObject type)
    {
        weaponToDrop = type;
    }
}
