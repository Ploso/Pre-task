using UnityEngine;

public class NodeHolder : MonoBehaviour {

    public Node ThisNode;
    public Material StartM;
    public Material GoalM;
    public Material OpenM;
    public Material ClosedM;
    public Material PathM;
    public Material ObsM;
    public Material Default;
    public Renderer r;
    private AstarGrid _grid;
    public GameObject Astar;

    public NodeHolder(Node node){
        ThisNode = node;
        if (ThisNode.NodeState == Node.State.StartNode) ChangeColor(ColorType.Start);
        else if (ThisNode.NodeState == Node.State.TargetNode) ChangeColor(ColorType.Start);
        else ChangeColor(ColorType.Default);
    }

    public void Awake()
    {
        Astar = GameObject.FindWithTag("Grid");
        _grid = Astar.GetComponent<AstarGrid>();
    }

    public void Update()
    {
        if (ThisNode.NodeState == Node.State.Open) ChangeColor(ColorType.Open);
        else if(ThisNode.NodeState == Node.State.Closed) ChangeColor(ColorType.Closed);
        else if(ThisNode.NodeState == Node.State.Path) ChangeColor(ColorType.Path);
        else if(ThisNode.NodeState == Node.State.StartNode) ChangeColor(ColorType.Start);
        else if(ThisNode.NodeState == Node.State.TargetNode) ChangeColor(ColorType.Goal);
        else if(ThisNode.NodeState == Node.State.Obstacle) ChangeColor(ColorType.Obstacle);
        else ChangeColor(ColorType.Default);
    }

    public void ChangeColor(ColorType type) {
        if (type == ColorType.Start) r.material = StartM;
        else if (type == ColorType.Goal) r.material = GoalM;
        else if (type == ColorType.Open) r.material = OpenM; 
        else if (type == ColorType.Closed) r.material = ClosedM;
        else if (type == ColorType.Path) r.material = PathM;
        else if (type == ColorType.Obstacle) r.material = ObsM;
        else if (type == ColorType.Default) r.material = Default;
    }

    public void OnMouseDown()
    {
        _grid.CalculateAgain(ThisNode.Xpos, ThisNode.Ypos);
    }


    public enum ColorType {
        Start,
        Goal,
        Open,
        Closed,
        Path,
        Obstacle,
        Default
    }
}
