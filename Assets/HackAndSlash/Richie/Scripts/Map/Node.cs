using System.Collections.Generic;
using UnityEngine;

namespace Richie
{
    public class Node
    {
        public Node RightNode;
        public Node LeftNode;

        public Vector2Int Origin;
        public Vector2Int Range;

        public Node(Vector2Int start, Vector2Int range)
        {
            RightNode = null;
            LeftNode = null;

            Origin = start;
            Range = range;
        }

        public void Split(Vector2Int minRoomSize)
        {
            if (LeftNode == null || RightNode == null)
            {
                Vector2Int newRangeL;
                Vector2Int newRangeR;
                Vector2Int newOrigin;

                if (Range.x >= Range.y)
                {
                    if (Range.x / 3 < minRoomSize.x) return;
                    int x = Random.Range(minRoomSize.x, Range.x - minRoomSize.x);

                    newRangeL = new(x, Range.y);
                    newRangeR = new((Range.x - x), Range.y);
                    newOrigin = new((Origin.x + x), Origin.y);
                }
                else
                {
                    if (Range.y / 3 < minRoomSize.y) return;
                    int y = Random.Range(minRoomSize.y, Range.y - minRoomSize.y);

                    newRangeL = new(Range.x, y);
                    newRangeR = new(Range.x, (Range.y - y));
                    newOrigin = new(Origin.x, (Origin.y + y));
                }

                LeftNode = new Node(Origin, newRangeL);
                RightNode = new Node(newOrigin, newRangeR);
            }
            else
            {
                LeftNode.Split(minRoomSize);
                RightNode.Split(minRoomSize);
            }
        }

        public List<Node> GetLeaves(List<Node> leaves)
        {
            if (LeftNode == null || RightNode == null)
            {
                leaves.Add(this);
            }
            else
            {
                leaves = LeftNode.GetLeaves(leaves);
                leaves = RightNode.GetLeaves(leaves);
            }

            return leaves;
        }
    }
}
