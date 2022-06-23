using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityIntro.Erik.TowerDefense
{
    public class TowerDefense_InputManager : MonoBehaviour
    {
        public TowerDefense_Input Input;

        private Vector2 mousePos;
        private void Awake() {
            Input = new();

            Input.GameControl.PlaceTower.performed += ctx => placeTower();
            Input.GameControl.MousePos.performed += ctx => mousePos = ctx.ReadValue<Vector2>();
        }

        public TowerDefense_Tile getSelected(){
            RaycastHit rh;
            TowerDefense_Tile result = null;
            Vector3 castPosition = Camera.main.ScreenToWorldPoint(mousePos);
            if(Physics.Raycast(castPosition, Camera.main.transform.forward, out rh)){
                rh.collider.gameObject.TryGetComponent<TowerDefense_Tile>(out result);
            }
            return result;
        }

        public void placeTower(){

        }
    }
}
