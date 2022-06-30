using System.Collections;
using System.Collections.Generic;
using Shizounu.Library.Editor;
using UnityEngine;

namespace UnityIntro.Erik.HackAndSlash
{
    public class HackAndSlash_CameraController : MonoBehaviour
    {
        public Transform Player;
        public float DampTime;
        public Vector3 offSet = new Vector3(0, 10, 0);
        
        [SerializeField, ReadOnly] public Vector3 velocity;
        private void LateUpdate() {
            transform.position = Vector3.SmoothDamp(transform.position, Player.position + offSet, ref velocity, DampTime);
        }

        [Header("Shake")]
        public float ShakeAmplitude = 0.2f;
        public float ShakeFrequency = 69;
        public float ShakeTime = 0.5f;
        Vector3 originalPos;

        [ContextMenu("Shake")]
        public void ShakeCameraCall(){
            originalPos = transform.position;
            StartCoroutine(shakeCam());
        }
        IEnumerator shakeCam(){
            for (int i = 0; i < ShakeFrequency; i++)
            {
                transform.position = originalPos + Random.insideUnitSphere * ShakeAmplitude;
                yield return new WaitForSeconds(ShakeTime / ShakeFrequency);
            }
            
        }
    }
}
