namespace ContactListSort;

/// <summary>
/// A singly linked list that manages a sequence of contacts.
/// Demonstrates node creation, list traversal, and in-place node deletion.
/// </summary>
public class ContactLinkedList
{
    // Reference to the first node in the list (entry point)
    public ContactNode? Head;

    /// <summary>
    /// Appends a new contact to the end of the linked list.
    /// Time Complexity: O(n) - traverses to the tail to attach the new node.
    /// </summary>
    public void Add(string name)
    {
        ContactNode newNode = new ContactNode(name);

        // Case 1: If the list is empty, new node becomes the Head
        if (Head == null)
        {
            Head = newNode;
            return;
        }

        // Case 2: Traverse to the final node (where Next is null)
        ContactNode current = Head;
        while (current.Next != null)
        {
            current = current.Next;
        }

        // Link the final node's Next pointer to the new node
        current.Next = newNode;
    }

    /// <summary>
    /// Appends multiple contact names in sequence.
    /// </summary>
    public void AddRange(params string[] names)
    {
        foreach (var name in names)
        {
            Add(name);
        }
    }

    /// <summary>
    /// Removes duplicate contact names from a sorted linked list in-place.
    /// 
    /// Logic:
    /// 1. Compare the current contact with the next one.
    /// 2. If both have the same name, delete the duplicate by bypassing it.
    /// 3. Move to the next unique contact.
    /// 
    /// Time Complexity:  O(n) - single pass through the list.
    /// Space Complexity: O(1) - in-place link updates; no extra memory allocated.
    /// </summary>
    public void RemoveDuplicates()
    {
        ContactNode? current = Head;

        // Traverse while both the current node and its successor exist
        while (current != null && current.Next != null)
        {
            // Step 1: Compare current contact with the next one
            if (current.Name == current.Next.Name)
            {
                // Step 2: Delete duplicate by updating current.Next to bypass current.Next
                current.Next = current.Next.Next;

                // NOTE: Do not advance 'current' here.
                // The newly linked current.Next might also be a duplicate of current.
            }
            else
            {
                // Step 3: Move to the next unique contact
                current = current.Next;
            }
        }
    }

    /// <summary>
    /// Traverses the list from Head to end and prints each node's name.
    /// Time Complexity: O(n)
    /// </summary>
    public void Display()
    {
        if (Head == null)
        {
            Console.WriteLine("[Empty List]");
            return;
        }

        ContactNode? current = Head;
        while (current != null)
        {
            Console.Write(current.Name);
            if (current.Next != null)
            {
                Console.Write(" → ");
            }
            current = current.Next;
        }
        Console.WriteLine();
    }
}
