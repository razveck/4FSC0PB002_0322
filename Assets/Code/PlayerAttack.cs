using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class PlayerAttack : Attack
{
    public override void doAttack(Transform target)
    {
        RaycastHit rh;
        Physics.Raycast(transform.position, target.position, out rh, 5);
        if(rh.transform == target){
            Debug.Log("Pew");
        }
    }
}
