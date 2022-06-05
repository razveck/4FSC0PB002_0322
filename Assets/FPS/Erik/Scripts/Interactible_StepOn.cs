using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shizounu.Library;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class Interactible_StepOn : MonoBehaviour
{
    public float lowerDistance;
    public Transform buttonPortion;
    public UnityEvent triggerEffect;
    private void Awake() {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    public void Lower(){
        buttonPortion.transform.position += new Vector3(0, -lowerDistance, 0);
    }
    public void Raise(){
        buttonPortion.transform.position += new Vector3(0, lowerDistance, 0);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.HasComponent<PlayerController>()){
            triggerEffect?.Invoke();
            Lower();
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.HasComponent<PlayerController>()){
            Raise();
        }
    }
}
