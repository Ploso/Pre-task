using System.Collections.Generic;
using UnityEngine;

public class Node {

    public Node N0, N1, N2, N3, N4, N5, N6, N7;
    public int Xpos;
    public int Ypos;
    public int GCost;
    public int HCost;
    public Node Parent;
    public State NodeState;


    public Node(int xpos, int ypos) {
        Xpos = xpos;
        Ypos = ypos;
        NodeState = State.Unvisited;
}

    public Vector2 NodeLocation()
    {
        return new Vector2(Xpos, Ypos);
    }

    public List<Node> GetNeighbors() {
        List<Node> temp = new List<Node>();
        if (N0 != null) temp.Add(N0);
        if (N1 != null) temp.Add(N1);
        if (N2 != null) temp.Add(N2);
        if (N3 != null) temp.Add(N3);
        if (N4 != null) temp.Add(N4);
        if (N5 != null) temp.Add(N5);
        if (N6 != null) temp.Add(N6);
        if (N7 != null) temp.Add(N7);
        return temp;
    }

    public int FCost { get { return HCost + GCost; } }

    public void Reset() {
        GCost = 0;
        HCost = 0;
        Parent = null;
        NodeState = State.Unvisited;
    }

    public enum State {
        Unvisited,
        StartNode,
        TargetNode,
        Open,
        Closed,
        Obstacle,
        Path
    }

}
