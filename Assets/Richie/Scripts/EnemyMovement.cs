using UnityEngine;
using Richie.DoublyLinkedList;

namespace Richie.TowerDefence
{
    public class EnemyMovement : MonoBehaviour
    {
        private PathCreation _path;
        private Node<Vector3> node;

        public Vector3 Position;
        [SerializeField] private float _speed;

        private void Start()
        {
            _path = FindObjectOfType<PathCreation>();
            node = _path.Path.Head;
        }

        private void Update()
        {
            GridPosition();
            Move();
        }

        private void Move()
        {
            if (Vector3.Distance(node.Data, transform.position) < 0.01f && node.Next != null)
                node = node.Next;

            transform.Translate((node.Data - transform.position).normalized * _speed * Time.deltaTime);
        }

        private void GridPosition()
        {
            if (node.Next == null) return;
            float NextData = Vector3.Distance(node.Data, node.Next.Data);
            float myData = Vector3.Distance(transform.position, node.Data);

            if (myData <= NextData / 2f)
            {
                Position = node.Data;
            }
        }
    }
}
