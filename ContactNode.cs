namespace ContactListSort;

/// <summary>
/// Represents a single node in the linked list.
/// Each node stores the contact's name and a reference (pointer) to the next node.
/// </summary>
public class ContactNode
{
    public string Name;
    public ContactNode? Next;

    public ContactNode(string name)
    {
        Name = name;
        Next = null;
    }
}
