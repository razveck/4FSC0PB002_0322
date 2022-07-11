using System.Collections.Generic;
using UnityEngine;

namespace Richie.Simulation
{
    public class Spawner : MonoBehaviour
    {
        private PlayerInput _input;

        internal GameObject selected;
        private Interact _interact;
        private Map _map;

        private bool _random;
        private bool _isClickable = true;

        internal List<GameObject> rabbits = new();
        internal List<GameObject> foxes = new();

        private void Awake()
        {
            _input = new();
        }

        private void Start()
        {
            _map = GetComponent<Map>();
            _interact = GetComponent<Interact>();
            _input.sim.leftClick.performed += ctx => Spawn();
        }

        private void Spawn()
        {
            if (selected == null || !_isClickable) return;

            if (!_random)
            {
                SelectSpawn();
            }
            else RandomSpawn();
        }

        private void SelectSpawn()
        {
            if (!_map.PathFinder.AllNodes.ContainsKey(_interact.mousePosition) || !_map.PathFinder.AllNodes[_interact.mousePosition].IsValid) return;
            if (_interact.mousePosition.x < 0 || _interact.mousePosition.y < 0 || _interact.mousePosition.x > _map.range.x || _interact.mousePosition.y > _map.range.y) return;

            GameObject animal = Instantiate(selected, (Vector2)_interact.mousePosition, Quaternion.identity, transform);
            AnimalList(animal.GetComponent<AnimalBase>().type).Add(animal);
            animal.GetComponent<AnimalBase>().spawner = this;
            animal.GetComponent<AnimalBase>().map = _map;
        }

        private void RandomSpawn()
        {
            Vector2Int position = new();
            position.x = Random.Range(0, _map.range.x);
            position.y = Random.Range(0, _map.range.y);

            while (!_map.PathFinder.AllNodes[position].IsValid)
            {
                position.x = Random.Range(0, _map.range.x);
                position.y = Random.Range(0, _map.range.y);
            }

            GameObject animal = Instantiate(selected, (Vector2)position, Quaternion.identity, transform);
            AnimalList(animal.GetComponent<AnimalBase>().type).Add(animal);
            animal.GetComponent<AnimalBase>().spawner = this;
            animal.GetComponent<AnimalBase>().map = _map;
        }

        public void IsRandomToggle()
        {
            if (_random) _random = false; 
            else _random = true;
        }

        private List<GameObject> AnimalList(AnimalType.Type type)
        {
            switch (type)       
            {
                case AnimalType.Type.rabbit:
                    return rabbits;

                case AnimalType.Type.fox:
                    return foxes;

                default:
                    return null;
            }
        }
        public void CanClick() => _isClickable = true;

        public void CantClick() => _isClickable = false;

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }
    }
}
