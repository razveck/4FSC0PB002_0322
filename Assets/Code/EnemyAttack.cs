using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : Attack
{
    public override void doAttack(Transform target)
    {
        RaycastHit rh;
        Physics.SphereCast(transform.position, 3, Vector3.zero, out rh);
        if(rh.transform == target){
            Debug.Log("Boom");
        }
    }
    
}
