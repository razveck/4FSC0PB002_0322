using UnityEngine;
using UnityEngine.EventSystems;

namespace Richie.TowerDefence
{
    public class Interactions : MonoBehaviour
    {
        private TileMap _tileMap;
        private PlayerInput _input;
        private PlaceTower _placeTower;
        private GameManager _gameManager;

        private bool _pointer, _sell;

        public event SellTower OnSellTower;
        public delegate void SellTower();

        private void Awake()
        {
            _input = new PlayerInput();
            _input.defence.left.performed += ctx => LeftClick();

            _tileMap = GetComponent<TileMap>();
            _placeTower = GetComponent<PlaceTower>();
            _gameManager = GetComponent<GameManager>();

            _placeTower.OnCancel += _placeTower_OnCancel;
        }

        private void Update()
        {
            _pointer = (EventSystem.current.IsPointerOverGameObject());

            if (_sell && _placeTower.Selected != null)
            {
                RefundActive();
            }
        }

        private void LeftClick()
        {
            if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo) || _pointer) return;
            if (!hitInfo.transform.gameObject.TryGetComponent(out TileInfo tile)) return;

            if (!_sell)
            {
                SpawnTower(tile);
            }
            else TowerRefund(tile);
        }

        private void SpawnTower(TileInfo tile)
        {
            if (_tileMap.WorldTiles[tile.Coordinates].IsPathTile == false && tile.IsValid)
            {
                if (_placeTower.Selected != null && _placeTower.Selected.GetComponent<TowerBase>().Cost <= _gameManager._currentMoney)
                {
                    tile.SetValid(false);
                    GameObject tower = _placeTower.PlantTower(tile.transform);
                    tower.GetComponent<TowerBase>().Coordinates = new Vector2(tile.transform.position.x, tile.transform.position.z);
                }
                else print("No Tower Selected");
            }
            else print("Tile Invalid");
        }

        private void TowerRefund(TileInfo tile)
        {
            if (_tileMap.WorldTiles[tile.Coordinates].IsPathTile == false && !tile.IsValid)
            {
                if (tile.Tower != null)
                {
                    int refund = (int)tile.Tower.GetComponent<TowerBase>().Cost / 2;

                    Destroy(tile.Tower);
                    tile.SetValid(true);
                    tile.SetTower(null);

                    _gameManager._sellAmount = refund;
                    _gameManager._refund = false;
                    OnSellTower?.Invoke();

                    _gameManager._refund = false;
                    _sell = false;
                }
            }
        }

        public void RefundActive()
        {
            if (!_sell)
            {
                _gameManager._showRefund.SetActive(true);
                _gameManager._refund = true;
                _placeTower.Selected = null;
                _sell = true;
            }
            else
            {
                _gameManager._showRefund.SetActive(false);
                _gameManager._refund = false;
                _sell = false;
            }
        }

        private void _placeTower_OnCancel()
        {
            if (!_sell) return;
            RefundActive();
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
