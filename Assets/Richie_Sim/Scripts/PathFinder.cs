using System.Collections.Generic;
using UnityEngine;

namespace Richie.Simulation 
{
    public class PathFinder
    {
        // look at all neighboring nodes  
        // choose the node with the lowest f cost //

        public Dictionary<Vector2Int, Node> AllNodes = new(); // collection of all nodes //

        private const int Diagnal = 14; // cost moving diagnal //
        private const int Straight = 10; // cost moving straight //

        private Vector2Int _grid = new();

        public PathFinder(Vector2Int grid)
        {
            _grid = grid;
            for (int x = 0; x < grid.x; x++)
            {
                for (int y = 0; y < grid.y; y++)
                {
                    Vector2Int position = new(x, y);
                    Node node = new(position);
                    AllNodes.Add(position, node);
                }
            }
        }

        public List<Vector2> FindPath(Vector2Int start, Vector2Int end)
        {
            Node startNode = AllNodes[start];
            Node endNode = AllNodes[end];

            List<Node> OpenNodes = new() { startNode };
            List<Node> ClosedNodes = new();

            startNode.GCost = 0;
            startNode.HCost = DistanceTo(startNode, endNode);

            foreach (var item in AllNodes)
            {
                item.Value.GCost = int.MaxValue;
                item.Value.Previous = null;
            }

            while (OpenNodes.Count > 0)
            {
                Node currentNode = LowestIn(OpenNodes);
                if (currentNode.Position == endNode.Position)
                {
                    return GetPath(endNode.Position);
                }

                OpenNodes.Remove(currentNode);
                ClosedNodes.Add(currentNode);

                foreach (Node neighbor in GetNeighbors(currentNode))
                {
                    if (!neighbor.IsValid) ClosedNodes.Add(neighbor);
                    if (ClosedNodes.Contains(neighbor)) continue;

                    int gCost = currentNode.GCost + DistanceTo(currentNode, neighbor);
                    if (gCost < neighbor.GCost)
                    {
                        neighbor.Previous = currentNode;
                        neighbor.GCost = gCost;
                        neighbor.HCost = DistanceTo(neighbor, endNode);

                        if (!OpenNodes.Contains(neighbor)) OpenNodes.Add(neighbor);
                    }
                }
            }

            return null;
        }

        public void InvalidTiles(List<Vector2Int> invalidTiles)
        {
            foreach (Vector2Int node in invalidTiles)
            {
                AllNodes[node].IsValid = false;
            }
        }

        public Node GetNode(Vector2Int position) => AllNodes[position];

        private List<Vector2> GetPath(Vector2Int end)
        {
            Node node = AllNodes[end];
            List<Vector2> path = new();
            path.Add(node.Position);

            while (node.Previous != null)
            {
                path.Add(node.Previous.Position);
                node = node.Previous;
            }
            path.Reverse();
            return path;
        }

        private List<Node> GetNeighbors(Node node)
        {
            List<Node> neighbors = new();

            if (node.Position.x - 1 >= 0)
            {
                Vector2Int left = new(node.Position.x - 1, node.Position.y);
                neighbors.Add(AllNodes[left]);

                if (node.Position.y - 1 >= 0)
                {
                    Vector2Int upLeft = new(node.Position.x - 1, node.Position.y - 1);
                    neighbors.Add(AllNodes[upLeft]);
                }

                if (node.Position.y + 1 < _grid.y)
                {
                    Vector2Int downLeft = new(node.Position.x - 1, node.Position.y + 1);
                    neighbors.Add(AllNodes[downLeft]);
                }
            }

            if (node.Position.x + 1 < _grid.x)
            {
                Vector2Int right = new(node.Position.x + 1, node.Position.y);
                neighbors.Add(AllNodes[right]);

                if (node.Position.y - 1 >= 0)
                {
                    Vector2Int upRight = new(node.Position.x + 1, node.Position.y - 1);
                    neighbors.Add(AllNodes[upRight]);
                }

                if (node.Position.y + 1 < _grid.y)
                {
                    Vector2Int downRight = new(node.Position.x + 1, node.Position.y + 1);
                    neighbors.Add(AllNodes[downRight]);
                }
            }

            if (node.Position.y - 1 >= 0)
            {
                Vector2Int down = new(node.Position.x, node.Position.y - 1);
                neighbors.Add(AllNodes[down]);
            }

            if (node.Position.y + 1 < _grid.y)
            {
                Vector2Int up = new(node.Position.x, node.Position.y + 1);
                neighbors.Add(AllNodes[up]);
            }

            return neighbors;
        }

        private int DistanceTo(Node a, Node b)
        {
            int xDistance = Mathf.Abs(a.Position.x - b.Position.x);
            int yDistance = Mathf.Abs(a.Position.y - b.Position.y);

            if (xDistance > yDistance)
            {
                return Diagnal * yDistance + Straight * (xDistance - yDistance);
            }
            else return Diagnal * xDistance + Straight * (yDistance - xDistance);
        }

        private Node LowestIn(List<Node> nodeList)
        {
            Node lowest = nodeList[0];
            for (int i = 0; i < nodeList.Count; i++)
            {
                if (nodeList[i].FCost < lowest.FCost)
                    lowest = nodeList[i];
            }

            return lowest;
        }
    }
}
