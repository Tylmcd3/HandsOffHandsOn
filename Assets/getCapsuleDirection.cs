using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Q3Movement;
public class getCapsuleDirection : MonoBehaviour
{
    Q3PlayerController move;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("X", move.m_PlayerVelocity.x);
        anim.SetFloat("Y", move.m_PlayerVelocity.y);
        anim.SetFloat ("Z", move.m_PlayerVelocity.z);
    }
}
