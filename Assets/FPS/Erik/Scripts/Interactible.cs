using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UnityIntro.Erik.FPS
{
    public class Interactible : MonoBehaviour
    {
        public UnityEvent triggerEffect;
        public void Interact()
        {
            triggerEffect?.Invoke();
        }
    }
}