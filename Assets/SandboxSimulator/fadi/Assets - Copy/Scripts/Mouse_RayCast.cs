using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_RayCast : MonoBehaviour
{
    Camera cam;
    public float Spawner;
    [SerializeField] LayerMask mask;
    Ray ray;
    RaycastHit hit;
    [SerializeField] List<GameObject> prefabs;
    Vector3 mousePos;
    private void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        //draw ray
        mousePos = Input.mousePosition;
        mousePos.z = 150f;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position, mousePos - transform.position, Color.red);
        Spawn();

    }

    void Spawn()
    {
        switch (Spawner)
        {
            case 1: //rabit
                if (Input.GetMouseButtonDown(0))
                {
                    ray = cam.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit)) ;
                    {
                        Instantiate(prefabs[0], hit.point, Quaternion.identity);

                    }
                }
                break;
            case 2: //fox
                if (Input.GetMouseButtonDown(0))
                {
                    ray = cam.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit)) ;
                    {
                        Instantiate(prefabs[1], hit.point, Quaternion.identity);


                    }
                }
                    break;
            case 3: //bear
                if (Input.GetMouseButtonDown(0))
                {
                    ray = cam.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit)) ;
                    {
                        Instantiate(prefabs[2], hit.point, Quaternion.identity);


                    }
                }
                break;
            case 4: //eagle
                if (Input.GetMouseButtonDown(0))
                {
                    ray = cam.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit)) ;
                    {
                        Instantiate(prefabs[3], hit.point, Quaternion.identity);


                    }
                }
                break;
            case 5: //roman
                if (Input.GetMouseButtonDown(0))
                {
                    ray = cam.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit)) ;
                    {
                        Instantiate(prefabs[4], hit.point, Quaternion.identity);
                    }
                }
                break;
            case 6:
                if (Input.GetMouseButtonDown(0))
                {
                    ray = cam.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit)) ;
                    {
                        Instantiate(prefabs[5], hit.point, Quaternion.identity);

                    }
                }
                break;
            case 7:
                if (Input.GetMouseButtonDown(0))
                {
                    ray = cam.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit)) ;
                    {
                        Instantiate(prefabs[6], hit.point, Quaternion.identity);
                    }
                }
                break;
        }
    }
}
