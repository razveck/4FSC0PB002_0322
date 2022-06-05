using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactible : MonoBehaviour
{
    public UnityEvent triggerEffect;
    public void Interact(){
        triggerEffect?.Invoke();
    }
}
