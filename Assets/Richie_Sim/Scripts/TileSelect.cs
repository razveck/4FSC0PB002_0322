using UnityEngine;

namespace Richie.Simulation
{
    public class TileSelect : MonoBehaviour
    {
        [Header("Material Settings")]
        [SerializeField] private Material _valid;
        [SerializeField] private Material _invalid;
        [SerializeField] private Material _highlight;
        [SerializeField] private Material _default;
        private SpriteRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _default = _renderer.material;
        }

        public void SetValid() => _renderer.material = _valid;

        public void SetInvalid() => _renderer.material = _invalid;

        public void SetHighlight() => _renderer.material = _highlight;

        public void SetDefault() => _renderer.material = _default;
    }
}