using UnityEngine;

namespace Richie.TowerDefence
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] private GameObject _myElement;
        [SerializeField] private GameObject _otherElement;

        public void Activate()
        {
            _otherElement.SetActive(true);
            _myElement.SetActive(false);
        }
    }
}
