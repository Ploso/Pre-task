public class AstarStack {

    public Node Head;
    public AstarStack Next;

    public AstarStack(Node a) {
        Head = a;
        Next = null;
    }

    public bool IsEmpty() { 
        return Head == null;
    }

    public void Insert(Node temp) {

        if (IsEmpty()) Head = temp;
        else if (temp.FCost < Head.FCost) {
            if (Next == null) Next = new AstarStack(Head);
            else Next.Insert(Head);
            Head = temp;
        } else if (temp.FCost == Head.FCost){
            if (temp.HCost < Head.HCost){
                if (Next == null) Next = new AstarStack(Head);
                else Next.Insert(Head);
                Head = temp;
            } else if (temp.HCost == Head.HCost) { 
                if (temp.GCost < Head.GCost) {
                    if (Next == null) Next = new AstarStack(Head);
                    else Next.Insert(Head);
                    Head = temp;
                } else {
                    if (Next == null) Next = new AstarStack(temp);
                    else Next.Insert(temp);
                }
            } else {
                if (Next == null) Next = new AstarStack(temp);
                else Next.Insert(temp);
            }
        } else {
            if (Next == null) Next = new AstarStack(temp);
            else Next.Insert(temp);
        }
    }

    public Node Retrieve() {

        Node temp = Head;
        if (Next == null) Head = null;
        else {
            Head = Next.Head;
            Next = Next.Next;
        }
        return temp;
    }

    public bool Search(Node node){

        if (Head == null) return false;
        if (node.Xpos == Head.Xpos && node.Ypos == Head.Ypos) return true;
        else if (Next == null) return false;
        else return Next.Search(node);
    }
}
