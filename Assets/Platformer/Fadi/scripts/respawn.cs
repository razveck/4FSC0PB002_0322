using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            collision.transform.position = new Vector3(28.53041f, 11.64815f, -41.2795f);

        }
    }



}
