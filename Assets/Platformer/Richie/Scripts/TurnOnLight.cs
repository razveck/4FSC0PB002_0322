using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Richie.Platformer
{
    public class TurnOnLight : MonoBehaviour
    {

        public GameObject Objects;

        private void Start()
        {
            Objects.SetActive(false);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Objects.SetActive(true);
        }
    }
}
