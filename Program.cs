namespace ContactListSort;

internal class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("======================================================================");
        Console.WriteLine("    LINKED LIST DEMO: REMOVE DUPLICATES FROM A SORTED CONTACT LIST    ");
        Console.WriteLine("======================================================================\n");

        // --------------------------------------------------------------------
        // Demonstration 1: The Problem Specification Example
        // --------------------------------------------------------------------
        Console.WriteLine("--- DEMO 1: Main Example (Aarav, Bella, Catherine) ---");

        ContactLinkedList contacts = new ContactLinkedList();
        contacts.AddRange(
            "Aarav Sharma",
            "Aarav Sharma",
            "Bella Thomas",
            "Catherine Lee",
            "Catherine Lee"
        );

        Console.Write("Input Contact List (Sorted):  ");
        contacts.Display();

        // Perform in-place linked list duplicate removal
        contacts.RemoveDuplicates();

        Console.Write("Output Contact List:          ");
        contacts.Display();
        Console.WriteLine();

        // --------------------------------------------------------------------
        // Demonstration 2: All Duplicate Nodes
        // --------------------------------------------------------------------
        Console.WriteLine("--- DEMO 2: All Duplicate Nodes ---");

        ContactLinkedList allDuplicates = new ContactLinkedList();
        allDuplicates.AddRange("John Doe", "John Doe", "John Doe");

        Console.Write("Before: ");
        allDuplicates.Display();

        allDuplicates.RemoveDuplicates();

        Console.Write("After:  ");
        allDuplicates.Display();
        Console.WriteLine();

        // --------------------------------------------------------------------
        // Demonstration 3: Consecutive Runs of Multiple Duplicates
        // --------------------------------------------------------------------
        Console.WriteLine("--- DEMO 3: Runs of 3+ Duplicates ---");

        ContactLinkedList multipleRuns = new ContactLinkedList();
        multipleRuns.AddRange("Alice", "Alice", "Bob", "Charlie", "Charlie", "Charlie");

        Console.Write("Before: ");
        multipleRuns.Display();

        multipleRuns.RemoveDuplicates();

        Console.Write("After:  ");
        multipleRuns.Display();
        Console.WriteLine();

        // --------------------------------------------------------------------
        // Demonstration 4: Edge Cases (Single Node & Empty List)
        // --------------------------------------------------------------------
        Console.WriteLine("--- DEMO 4: Edge Cases ---");

        ContactLinkedList singleNode = new ContactLinkedList();
        singleNode.Add("Zara Patel");
        Console.Write("Single Node Before: ");
        singleNode.Display();
        singleNode.RemoveDuplicates();
        Console.Write("Single Node After:  ");
        singleNode.Display();

        ContactLinkedList emptyList = new ContactLinkedList();
        Console.Write("Empty List Before:  ");
        emptyList.Display();
        emptyList.RemoveDuplicates();
        Console.Write("Empty List After:   ");
        emptyList.Display();

        Console.WriteLine("\n======================================================================");
        Console.WriteLine("All demonstrations completed successfully.");
        Console.WriteLine("======================================================================");
    }
}
