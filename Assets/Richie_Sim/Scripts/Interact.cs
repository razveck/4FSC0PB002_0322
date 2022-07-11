using UnityEngine;

namespace Richie.Simulation
{
    public class Interact : MonoBehaviour
    {
        private Map _map;
        private PlayerInput _input;
        private bool _setValid, _canSelect = true;

        internal Vector2Int start, end;
        internal Vector2Int mousePosition;

        private void Awake()
        {
            _input = new();
        }

        private void Start()
        {
            _map = GetComponent<Map>();

            _input.sim.rightClick.performed += ctx => StartSelect();
            _input.sim.rightClick.canceled += ctx => BoxSelect();
        }

        private void Update()
        {
            MouseToGrid();
        }

        private void MouseToGrid()
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition = new(Mathf.RoundToInt(mouse.x), Mathf.RoundToInt(mouse.y));
        }

        private void StartSelect()
        {
            if (_map.worldTiles.ContainsKey(mousePosition))
            {
                start = mousePosition;
                _canSelect = true;
            }
            else _canSelect = false; 
        }

        private void BoxSelect()
        {
            if (!_canSelect || !_map.worldTiles.ContainsKey(mousePosition))
            {
                _canSelect = true;
                return;
            }

            end = mousePosition;
            if (start.x < end.x && start.y > end.y)
            {
                Vector2Int temp = start;
                start = new(start.x, end.y);
                end = new(end.x, temp.y);
            }
            else if (start.x > end.x && start.y < end.y)
            {
                Vector2Int temp = start;
                start = new(end.x, start.y);
                end = new(temp.x, end.y);
            }
            else
            {
                if (Vector2.SqrMagnitude(start) > Vector2.SqrMagnitude(end))
                {
                    Vector2Int temp = start;
                    start = end;
                    end = temp;
                }
            }

            int index;
            bool single = false;
            if (start == end) single = true;

            for (int x = start.x; x <= end.x; x++)
            {
                for (int y = start.y; y <= end.y; y++)
                {
                    if (!_map.PathFinder.AllNodes.ContainsKey(new(x, y))) continue;

                    if (_setValid)
                    {
                        float number = Random.Range(0f, 1f);

                        if (number < 0.9f) index = 0;
                        else index = Random.Range(1, _map.grassTiles.Length);

                        Destroy(_map.worldTiles[new(x, y)]);
                        _map.PathFinder.AllNodes[new(x, y)].IsValid = true;
                        GameObject newTile = Instantiate(_map.grassTiles[index], new Vector2(x, y), Quaternion.identity, _map._tileContainer.transform);
                        _map.worldTiles[new(x, y)] = newTile;
                    }
                    else
                    {
                        if (!single)
                        {
                            float number = Random.Range(0f, 1f);

                            if (number < 0.8f) index = 0;
                            else index = Random.Range(1, _map.waterTiles.Length);

                            Destroy(_map.worldTiles[new(x, y)]);
                            _map.PathFinder.AllNodes[new(x, y)].IsValid = false;
                            GameObject newTile = Instantiate(_map.waterTiles[index], new Vector2(x, y), Quaternion.identity, _map._tileContainer.transform);
                            _map.worldTiles[new(x, y)] = newTile;
                        }
                        else
                        {
                            Destroy(_map.worldTiles[new(x, y)]);
                            _map.PathFinder.AllNodes[new(x, y)].IsValid = false;
                            GameObject newTile = Instantiate(_map.treeTiles[Random.Range(0, _map.treeTiles.Length)], new Vector2(x, y), Quaternion.identity, _map._tileContainer.transform);
                            _map.worldTiles[new(x, y)] = newTile;
                        }
                    }
                }
            }
        }

        public void IsValidToggle()
        {
            if (_setValid) _setValid = false;
            else _setValid = true;
        }

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
