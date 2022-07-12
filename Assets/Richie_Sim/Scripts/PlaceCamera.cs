using UnityEngine;

namespace Richie.Simulation
{
    public class PlaceCamera : MonoBehaviour
    {
        [SerializeField] private Map _map;

        void Start()
        {
            Camera camera = GetComponent<Camera>();
            transform.position = new(_map.range.x / 2f, _map.range.y / 2f, -10f);

            if (_map.range.x / 2f > _map.range.y / 2f)
            {
                camera.orthographicSize = (_map.range.x / 2f) + 1f;
            } 
            else camera.orthographicSize = (_map.range.y / 2f) + 1f;
        }
    }
}
