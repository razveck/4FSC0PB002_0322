using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingStuff : MonoBehaviour
{
    public float speed;
    private int direction = 1;
    CharacterController controller;
    private void Start()
    {
        controller = GetComponent<CharacterController>();   
    }
    void FixedUpdate()
    {
            controller.Move(transform.forward * direction * speed);
        if (Physics.Raycast(transform.position, transform.forward * direction, out RaycastHit hitinfo, 4f))
        {
            Debug.DrawRay(transform.position, transform.forward * hitinfo.distance, Color.red);
            if (direction == 1)
                direction = -1;
            else
                direction = 1;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * hitinfo.distance, Color.white);
        }
    }
}
