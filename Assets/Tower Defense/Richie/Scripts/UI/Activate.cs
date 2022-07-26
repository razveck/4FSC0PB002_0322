using UnityEngine.UI;
using UnityEngine;

namespace Richie.TowerDefence
{
    public class Activate : MonoBehaviour
    {
        [SerializeField] private GameObject _active;
        
        private void Start()
        {
            _active.SetActive(false);    
        }

        public void Activated()
        {
            if (!GetComponent<Button>().interactable) return;
            _active.SetActive(true);
        }

        public void Deactivated()
        {
            _active.SetActive(false);
        }
    }
}
