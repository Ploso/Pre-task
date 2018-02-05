using System.Collections.Generic;
using UnityEngine;

public class AstarAlgorithm {

    public List<Node> CalculatePath(Node start, Node goal) {
        AstarStack openSet;
        List<Node> result = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet = new AstarStack(start);

        while (!openSet.IsEmpty()) {

            Node currentNode = openSet.Retrieve();
            closedSet.Add(currentNode);
            if (currentNode.NodeState != Node.State.TargetNode && currentNode.NodeState != Node.State.StartNode) currentNode.NodeState = Node.State.Closed;

            if (currentNode == goal) return PathResult(start, goal);

            foreach (Node n in currentNode.GetNeighbors()) {
                if (n.NodeState == Node.State.Obstacle || closedSet.Contains(n)) continue;
                int newDistanceCost = currentNode.GCost + GetDistance(currentNode, n);
                if (newDistanceCost < n.GCost || !openSet.Search(n)) {
                    n.GCost = newDistanceCost;
                    n.HCost = GetDistance(n, goal);
                    n.Parent = currentNode;
                    if (!openSet.Search(n)) {
                        openSet.Insert(n);
                        if (n.NodeState != Node.State.StartNode && n.NodeState != Node.State.TargetNode) n.NodeState = Node.State.Open;
                    }
                }
            }
        }
        return result;
    }

    private int GetDistance(Node start, Node goal)
    {
        int xDist = Mathf.Abs(start.Xpos - goal.Xpos);
        int yDist = Mathf.Abs(start.Ypos - goal.Ypos);

        if (xDist > yDist) return 14 * yDist + 10 * (xDist - yDist);
        return 14 * xDist + 10 * (yDist - xDist);
    }

    private List<Node> PathResult(Node start, Node goal) {
        List<Node> path = new List<Node>();
        Node current = goal;
        while (current != start){
            path.Add(current);
            current = current.Parent;
            if (current.NodeState != Node.State.StartNode && current.NodeState != Node.State.TargetNode) current.NodeState = Node.State.Path;
        }
        return path;
    }

}
