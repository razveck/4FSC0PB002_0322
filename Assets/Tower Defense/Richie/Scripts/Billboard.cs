using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Richie.TowerDefence
{
    public class Billboard : MonoBehaviour
    {
        private Transform _camera;

        private void Start()
        {
            _camera = Camera.main.transform;
        }

        private void LateUpdate()
        {
            transform.LookAt(transform.position + _camera.forward);
        }
    }
}
