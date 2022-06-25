using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Richie.TowerDefence
{
    public class TowerSpin : MonoBehaviour
    {
        [SerializeField] private bool _rotate = false;
        [SerializeField] private float _speed = 0.25f;

        void Update()
        {
            RotateCore();
        }

        private void RotateCore()
        {
            if (!_rotate) return;
            transform.Rotate(0f, _speed * Time.deltaTime, 0f);
        }
    }
}
