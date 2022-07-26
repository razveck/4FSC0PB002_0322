using UnityEngine;

namespace Richie.TowerDefence
{
    public class TileSelection : MonoBehaviour
    {
        [Header("Material Settings")]
        [SerializeField] private Material _validSelection;
        [SerializeField] private Material _invalidSelection;

        [Header("Selection Settings")]
        [SerializeField] private bool _offset;
        [SerializeField] private float _amount = 0.1f;

        private bool _update;
        private TileInfo _info;
        private Renderer _renderer;
        private Material _defaultMat;

        private bool test;

        private void Start()
        {
            _renderer = GetComponent<Renderer>();
            _info = GetComponent<TileInfo>();
            _defaultMat = _renderer.material;
        }

        private void Update()
        {
            if (!_update) return;

            if (_info.IsValid)
            {
                _renderer.material = _validSelection;
            }
            else _renderer.material = _invalidSelection;
        }

        private void OnMouseEnter()
        {
            if (GameManager.DisableHover) return;

            _update = true;
            if (!_offset) return;
            transform.position = new Vector3(transform.position.x, transform.position.y + _amount, transform.position.z);
            test = true;
        }

        private void OnMouseExit()
        {
            if (GameManager.DisableHover && !test) return;

            _update = false;
            _renderer.material = _defaultMat;

            if (!_offset || !test) return;
            transform.position = new Vector3(transform.position.x, transform.position.y - _amount, transform.position.z);
            test = false;
        }

        private void OnMouseDown()
        {
            if (GameManager.DisableHover) return;
            transform.position = new Vector3(transform.position.x, transform.position.y - _amount, transform.position.z);
        }

        private void OnMouseUp() 
        {
            if (GameManager.DisableHover) return;
            transform.position = new Vector3(transform.position.x, transform.position.y + _amount, transform.position.z); 
        }
    }
}
