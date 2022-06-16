using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UnityIntro.Erik.Platformer
{
    public class Interactible_Volume : MonoBehaviour
    {
        public UnityEvent triggerEffect;
            private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<MovementController_Player>(out MovementController_Player mcp))
            {
                triggerEffect?.Invoke();
            }
        }
    }
}
