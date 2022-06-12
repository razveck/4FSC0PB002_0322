using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS_Richie
{
    public class FollowCam : MonoBehaviour
    {
        [SerializeField] Transform target;

        void Update()
        {
            transform.position = target.position;
        }
    }
}
