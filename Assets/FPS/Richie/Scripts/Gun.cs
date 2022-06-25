using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS_Richie
{
    public class Gun : MonoBehaviour
    {
        private PlayerInput _controller;

        private delegate void Shoot();
        private Shoot _shoot;

        [Header("Hit Scan / Projectile")]
        [SerializeField] private bool _isHitScan;

        [Header("Hit Scan Settings")]
        [SerializeField] protected float hitScanDamage = 10f;
        //[SerializeField] private float fireRate = 15f;
        [SerializeField] private float range = 100f;

        [Header("Projectile Settings")]
        [SerializeField] private float _projectileDamage = 10f;
        [SerializeField] private float _velocity;
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _spread;

        [Header("Effects")]
        [SerializeField] ParticleSystem _muzzleFlash;
        [SerializeField] GameObject _impact;

        [Header("References")]
        [SerializeField] Camera _camera;
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _shootFrom;
        private GameObject _activeBullets;

        private void Awake()
        {
            _controller = new PlayerInput();
        }

        void Start()
        {
            _controller.player.shoot.performed += ctx => _shoot();
            _activeBullets = new GameObject("Bullets");
        }

        private void Update()
        {
            ShootState();
            Debug.DrawRay(_camera.transform.position, _camera.transform.forward * range, Color.red);
        }

        private void ShootState()
        {
            if (_isHitScan)
            {
                _shoot = HitScan;
            }
            else
            {
                _shoot = Projectile;
            }
        }

        private void Projectile()
        {
            Vector3 targetPoint;
            _muzzleFlash.Play();

            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hit, range))
            {
                targetPoint = hit.point;
            }
            else targetPoint = _camera.transform.position + _camera.transform.forward * range;

            Debug.DrawLine(_shootFrom.position, targetPoint, Color.cyan, 10f);

            // calculate spread
            float x = Random.Range(-_spread, _spread);
            float y = Random.Range(-_spread, _spread);
            Vector3 direction = (targetPoint - _shootFrom.position) + new Vector3(x, y, 0f);
            Debug.DrawLine(_shootFrom.position, _shootFrom.position + direction, Color.magenta, 10f);

            GameObject currentBullet = Instantiate(_bullet, _shootFrom.position, Quaternion.identity, _activeBullets.transform);
            currentBullet.transform.forward = direction.normalized;

            // set damage & apply force //
            currentBullet.transform.GetComponent<Bullet>().Damage = _projectileDamage;
            currentBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * _velocity, ForceMode.Impulse);

            Destroy(currentBullet, Vector3.Distance(_shootFrom.position, targetPoint) / _velocity);
        }

        private void HitScan()
        {
            _muzzleFlash.Play();

            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hit, range))
            {
                Debug.Log(hit.transform.name);
                if (hit.transform.TryGetComponent<Target>(out Target target))
                {
                    target.TakeDamage(hitScanDamage);
                }

                GameObject impact = Instantiate(_impact, hit.point, Quaternion.LookRotation(hit.normal), _activeBullets.transform);
                Destroy(impact, 1f);
            }
        }

        private void OnEnable()
        {
            _controller.Enable();
        }

        private void OnDisable()
        {
            _controller.Disable();
        }
    }
}
