using UnityEngine;

namespace Richie.Simulation
{
    public class Node
    {
        public int GCost; // distance from start //
        public int HCost; // distance from end //
        public int FCost { get => GCost + HCost; }

        public Node Previous; // previous node before reaching this node // 
        public bool IsValid; // able to be in evaluated //

        public Vector2Int Position; // position on grid //

        public Node(Vector2Int origin)
        {
            IsValid = true;
            Position = origin;
        }

        public void SetValid(bool toggle) => IsValid = toggle;
    }
}
