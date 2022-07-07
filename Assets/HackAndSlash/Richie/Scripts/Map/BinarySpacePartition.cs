using System.Collections.Generic;
using UnityEngine;

namespace Richie
{
    public class BinarySpacePartition
    {
        public Node Root;
        public List<Node> leafNodes = new();

        public void AddRoot(Vector2Int data, Vector2Int range)
        {
            if (Root != null) return;
            Root = new Node(data, range);
        }

        public void Split(int iterations, Vector2Int minRoomSize)
        {
            if (Root == null) return;

            for (int i = 0; i < iterations; i++)
            {
                Root.Split(minRoomSize);
            }
        }

        public void GetLeaves()
        {
            if (Root == null) return;
            leafNodes = Root.GetLeaves(leafNodes);
        }
    }
}
