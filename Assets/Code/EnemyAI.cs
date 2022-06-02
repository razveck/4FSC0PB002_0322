using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAI : MonoBehaviour
{
     float speed = 0.5f;
    Rigidbody Rigidbody;
    CharacterController controller;



    void Start()
    {
        controller = GetComponent<CharacterController>();
    }



    void FixedUpdate()
    {
        
        //hits
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hitinfo, 1f))
        {
            //check if raycast hits
            Debug.Log("hit");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hitinfo.distance, Color.red);

            //actually what happens
            int Direction = Random.Range(1, 4);
            switch (Direction)
            {
                case 1:
                    transform.Rotate(new Vector3(0, -90, 0));
                    break;
                case 2:
                    transform.Rotate(new Vector3(0, 90, 0));
                    break;
                case 3:
                    transform.Rotate(new Vector3(0, 180, 0));
                    break;
            }
        }
        //doesnt hit
        else
        {
            controller.Move(transform.forward * speed);
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hitinfo2, 20f))
            {
                if(hitinfo2.collider.gameObject.CompareTag("player"))
                {
                    speed = 1;
                    
                }
                else
                {
                    speed = 0.5f;
                }
                
            }




        }


    }
}
