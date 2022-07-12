using UnityEngine;

namespace Richie.Simulation
{
    public class SlotBase : MonoBehaviour
    {
        [SerializeField] private Spawner _spawner;
        [SerializeField] private GameObject[] _selection;

        public void Select()
        {
            _spawner.selected = _selection[Random.Range(0, _selection.Length)];
        }
    }
}
