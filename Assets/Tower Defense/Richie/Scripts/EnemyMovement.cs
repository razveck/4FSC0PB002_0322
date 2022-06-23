using UnityEngine;
using Richie.DoublyLinkedList;

namespace Richie.TowerDefence
{
    public class EnemyMovement : MonoBehaviour
    {
        private PathCreation _path;
        private Node<Vector3> _node;

        public Vector3 Position;
        [SerializeField] private float _speed;

        private void Start()
        {
            _path = FindObjectOfType<PathCreation>();
            _node = _path.Path.Head;
        }

        private void Update()
        {
            GridPosition();
            Move();
        }

        private void Move()
        {
            if (Vector3.Distance(_node.Data, transform.position) < 0.01f && _node.Next != null)
                _node = _node.Next;

            transform.Translate((_node.Data - transform.position).normalized * _speed * Time.deltaTime);
        }

        private void GridPosition()
        {
            if (_node.Next == null) return;
            float NextData = Vector3.Distance(_node.Data, _node.Next.Data);
            float myData = Vector3.Distance(transform.position, _node.Data);

            if (myData <= NextData / 2f)
            {
                Position = _node.Data;
            }
        }
    }
}
