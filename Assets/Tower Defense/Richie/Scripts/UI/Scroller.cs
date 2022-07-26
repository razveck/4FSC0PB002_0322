using UnityEngine;

namespace Richie.TowerDefence
{
    public class Scroller : MonoBehaviour
    {
        [SerializeField] private GameObject _clouds;
        [SerializeField] private Transform _spawner;
        [SerializeField] private float _scrollSpeed = 10f;

        private GameObject temp00;
        private RectTransform rect00;

        private GameObject temp01;
        private RectTransform rect01;

        private void Start()
        {
            temp00 = Instantiate(_clouds, transform.position, Quaternion.identity, transform);
            rect00 = temp00.GetComponent<RectTransform>();

            temp01 = Instantiate(_clouds, _spawner.position, Quaternion.identity, transform);
            rect01 = temp01.GetComponent<RectTransform>();
        }

        private void FixedUpdate()
        {
            Scroll();
        }

        private void Scroll()
        {
            if (temp01 == null) return;
            temp01.transform.position = new(temp01.transform.position.x + _scrollSpeed * Time.fixedDeltaTime, temp01.transform.position.y, temp01.transform.position.z);

            if (temp00 == null) return;
            temp00.transform.position = new(temp00.transform.position.x + _scrollSpeed * Time.fixedDeltaTime, temp00.transform.position.y, temp00.transform.position.z);

            if (rect00.offsetMax.x >= 800)
            {
                Destroy(temp00);
                temp00 = Instantiate(_clouds, _spawner.position, Quaternion.identity, transform);
                rect00 = temp00.GetComponent<RectTransform>();
            }

            if (rect01.offsetMax.x >= 800)
            {
                Destroy(temp01);
                temp01 = Instantiate(_clouds, _spawner.position, Quaternion.identity, transform);
                rect01 = temp01.GetComponent<RectTransform>();
            }
        }
    }
}
