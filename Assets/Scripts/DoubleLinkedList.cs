public class DoubleLinkedList<T>
{
    public Node<T> Head = null;
    public Node<T> Tail = null;
    public int Count = 0;

    public void AddNode(T dato)
    {
        Node<T> newNode = new Node<T>(dato);

        if (Head == null)
        {
            Head = newNode;
            Tail = newNode;
        }
        else
        {
            Tail.SetNext(newNode);
            newNode.SetPrev(Tail);
            Tail = newNode;
        }
        Count++;
    }
}