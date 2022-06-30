using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rigidbody;
    private Vector2 moveDirection;
    private Vector3 startPosition;
    Vector3 gameObject;
    public Camera camera;



    void Start()
    {
        gameObject = transform.position;
    }


    void Update()
    {

        ProcessInput();
    }
    private void FixedUpdate()
    {
        Vector3 mouse_pos;
        Transform target = transform; //Assign to the object you want to rotate
        Vector3 object_pos;
        float angle;
        mouse_pos = Input.mousePosition;
        mouse_pos.z = 5.23f; //The distance between the camera and object
        gameObject = camera.WorldToScreenPoint(target.position);
        mouse_pos.x = mouse_pos.x - gameObject.x;
        mouse_pos.y = mouse_pos.y - gameObject.y;
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        target.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        //gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        Move();
    }
    void ProcessInput()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(inputX, inputY).normalized;

    }
    void Move()
    {
        rigidbody.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    }


}
