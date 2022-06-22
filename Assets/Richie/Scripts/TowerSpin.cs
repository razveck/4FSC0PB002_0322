using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Richie.TowerDefence
{
    public class TowerSpin : MonoBehaviour
    {
        [SerializeField] private float _speed = 0.25f;

        void Update()
        {

            transform.Rotate(0f, _speed * Time.deltaTime, 0f);

        }
    }
}
