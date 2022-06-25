using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Richie.Platformer
{
    public class Winner : MonoBehaviour
    {
        public GameObject _winText;

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.TryGetComponent<PlayerMovement>(out _))
            {
                _winText.SetActive(true);
            }
        }
    }
}
