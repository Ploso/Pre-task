using UnityEngine;

public class AstarGrid : MonoBehaviour {

    public IntVector2[] Obstacles;
    public IntVector2 StartNode;
    public IntVector2 TargetNode;
    public GameObject[,] Grid;
    private int _gridX;
    private int _gridY;
    public GameObject NodeObject;
    private AstarAlgorithm _algo;

    public void Awake()
    {
        ConstructGrid(15, 15);
        _algo = new AstarAlgorithm();
        _algo.CalculatePath(Grid[StartNode.x, StartNode.y].GetComponent<NodeHolder>().ThisNode, Grid[TargetNode.x, TargetNode.y].GetComponent<NodeHolder>().ThisNode); 
    }

    public void ConstructGrid(int sizeX, int sizeY) {
        Grid = new GameObject[sizeX, sizeY];
        _gridX = sizeX;
        _gridY = sizeY;
        Vector3 location;
        Node temp;
        for (int x = 0; x < sizeX; x++) {
            for (int y = 0; y < sizeY; y++) {
                temp = new Node(x, y);
                location = new Vector3(x + 0.5f, 0, y + 0.5f);
                NodeObject = Instantiate(NodeObject, location, Quaternion.identity);
                NodeObject.GetComponent<NodeHolder>().ThisNode = temp;  
                Grid[temp.Xpos, temp.Ypos] = NodeObject;
            }
        }
        Grid[StartNode.x, StartNode.y].GetComponent<NodeHolder>().ThisNode.NodeState = Node.State.StartNode;
        Grid[TargetNode.x, TargetNode.y].GetComponent<NodeHolder>().ThisNode.NodeState = Node.State.TargetNode;
        SetNeighbors();
        SetObstacles();
    }

    private void SetNeighbors() {
        foreach (GameObject holder in Grid) {
            Node node = holder.GetComponent<NodeHolder>().ThisNode; 
            node.N0 = (node.Ypos + 1 < _gridY) ? Grid[node.Xpos, node.Ypos + 1].GetComponent<NodeHolder>().ThisNode : null;
            node.N1 = (node.Xpos + 1 < _gridX && node.Ypos + 1 < _gridY) ? Grid[node.Xpos + 1, node.Ypos + 1].GetComponent<NodeHolder>().ThisNode : null;
            node.N2 = (node.Xpos + 1 < _gridX) ? Grid[node.Xpos + 1, node.Ypos].GetComponent<NodeHolder>().ThisNode : null;
            node.N3 = (node.Xpos + 1 < _gridX && node.Ypos - 1 >= 0) ? Grid[node.Xpos + 1, node.Ypos - 1].GetComponent<NodeHolder>().ThisNode: null;
            node.N4 = (node.Ypos - 1 >= 0) ? Grid[node.Xpos, node.Ypos - 1].GetComponent<NodeHolder>().ThisNode : null;
            node.N5 = (node.Xpos - 1 >= 0 && node.Ypos - 1 >= 0) ? Grid[node.Xpos - 1, node.Ypos - 1].GetComponent<NodeHolder>().ThisNode : null;
            node.N6 = (node.Xpos - 1 >= 0) ? Grid[node.Xpos - 1, node.Ypos].GetComponent<NodeHolder>().ThisNode : null ;
            node.N7 = (node.Xpos - 1 >= 0 && node.Ypos + 1 < _gridY) ? Grid[node.Xpos - 1, node.Ypos + 1].GetComponent<NodeHolder>().ThisNode : null;
        }
    }

    private void SetObstacles() {
        foreach(IntVector2 obs in Obstacles)
        {
            if (obs != StartNode && obs != TargetNode)
            {
                Grid[obs.x, obs.y].GetComponent<NodeHolder>().ThisNode.NodeState = Node.State.Obstacle;
            }
        }
    }

    public void CalculateAgain(int newX, int newY) {
        StartNode = new IntVector2(newX, newY);
        foreach (GameObject obj in Grid) {
            obj.GetComponent<NodeHolder>().ThisNode.Reset();
        }
        Grid[StartNode.x, StartNode.y].GetComponent<NodeHolder>().ThisNode.NodeState = Node.State.StartNode;
        Grid[TargetNode.x, TargetNode.y].GetComponent<NodeHolder>().ThisNode.NodeState = Node.State.TargetNode;
        SetObstacles();
        _algo.CalculatePath(Grid[StartNode.x, StartNode.y].GetComponent<NodeHolder>().ThisNode, Grid[TargetNode.x, TargetNode.y].GetComponent<NodeHolder>().ThisNode);
    }
}
