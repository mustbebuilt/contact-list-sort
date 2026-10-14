# Linked List: Remove Duplicates from a Sorted Contact List

An educational C# (.NET 10) project demonstrating the fundamentals of **Singly Linked Lists**, including **node representation**, **pointer traversal**, and **$\mathcal{O}(1)$ in-place node deletion**.

---

## 📚 Table of Contents
1. [The Problem Scenario](#the-problem-scenario)
2. [What is a Linked List?](#what-is-a-linked-list)
3. [Linked List vs. Array for Deduplication](#linked-list-vs-array-for-deduplication)
4. [Anatomy of the Contact Linked List](#anatomy-of-the-contact-linked-list)
5. [Core Operations Explained](#core-operations-explained)
   - [1. Appending a Node (`Add`)](#1-appending-a-node-add)
   - [2. Traversing and Displaying (`Display`)](#2-traversing-and-displaying-display)
   - [3. In-Place Duplicate Removal (`RemoveDuplicates`)](#3-in-place-duplicate-removal-removeduplicates)
6. [Visual Step-by-Step Pointer Bypass](#visual-step-by-step-pointer-bypass)
7. [Complexity Analysis](#complexity-analysis)
8. [Code Structure](#code-structure)
9. [How to Run](#how-to-run)

---

## 📌 The Problem Scenario

Imagine maintaining a sorted contact list on a phone. Over time, syncing contacts across multiple sources (Google, SIM card, Outlook) creates duplicate entries:

```text
Input (Sorted):  Aarav Sharma → Aarav Sharma → Bella Thomas → Catherine Lee → Catherine Lee
Output:          Aarav Sharma → Bella Thomas → Catherine Lee
```

Because the list is alphabetically sorted, all identical names appear consecutively. This enables an in-place cleanup using simple pointer adjustments.

---

## 💡 What is a Linked List?

A **Linked List** is a linear data structure where elements are not stored at contiguous memory locations. Instead, each element is an independent object called a **Node**.

Each node consists of two parts:
1. **Data**: The value stored (in our case, the contact's `Name`).
2. **Next Pointer / Reference**: A reference pointing to the memory address of the subsequent node in the sequence.

The entry point to the list is called the **Head**. The last node in the list points to `null`, indicating the end.

```
Head
 │
 ▼
┌─────────────────┐      ┌─────────────────┐      ┌─────────────────┐
│ Name: "Aarav"   │ ───► │ Name: "Bella"   │ ───► │ Name: "Catherine│ ───► null
│ Next: 0x2A...   │      │ Next: 0x3F...   │      │ Next: null      │
└─────────────────┘      └─────────────────┘      └─────────────────┘
```

---

## ⚖️ Linked List vs. Array for Deduplication

| Operation / Feature | Array / `List<T>` | Singly Linked List |
| :--- | :--- | :--- |
| **Memory Layout** | Contiguous block in memory | Independent heap objects linked by references |
| **Node Deletion** | **$\mathcal{O}(n)$** per deletion (all subsequent elements must shift left in memory) | **$\mathcal{O}(1)$** per deletion (only a single pointer assignment: `current.Next = current.Next.Next`) |
| **Total Deduplication Time** | **$\mathcal{O}(n^2)$** without extra memory, or $\mathcal{O}(n)$ using an auxiliary Hash Set | **$\mathcal{O}(n)$** with **$\mathcal{O}(1)$ space** (in-place) |
| **Extra Memory Allocated** | Often $\mathcal{O}(n)$ if creating a new list | **$\mathcal{O}(1)$** (no new nodes or collections allocated) |

---

## 🔬 Anatomy of the Contact Linked List

### 1. The Node (`ContactNode.cs`)
```csharp
public class ContactNode
{
    public string Name;        // Data payload
    public ContactNode? Next;  // Pointer/reference to the next node

    public ContactNode(string name)
    {
        Name = name;
        Next = null;
    }
}
```

### 2. The List Container (`ContactLinkedList.cs`)
```csharp
public class ContactLinkedList
{
    public ContactNode? Head;  // Entry point to the first node
    ...
}
```

---

## 🛠️ Core Operations Explained

### 1. Appending a Node (`Add`)

To add a new contact to the end of the list:
1. Wrap the name in a new `ContactNode`.
2. If `Head == null` (the list is empty), set `Head = newNode`.
3. Otherwise, initialize a pointer `ContactNode current = Head` and traverse until reaching the node where `current.Next == null`.
4. Link `current.Next = newNode`.

```csharp
public void Add(string name)
{
    ContactNode newNode = new ContactNode(name);

    if (Head == null)
    {
        Head = newNode;
        return;
    }

    ContactNode current = Head;
    while (current.Next != null)
    {
        current = current.Next;
    }

    current.Next = newNode;
}
```

---

### 2. Traversing and Displaying (`Display`)

To print the list, start at `Head` and step from node to node until reaching `null`:

```csharp
ContactNode? current = Head;
while (current != null)
{
    Console.Write(current.Name);
    if (current.Next != null) Console.Write(" → ");
    current = current.Next; // Advance to next node
}
```

---

### 3. In-Place Duplicate Removal (`RemoveDuplicates`)

The core algorithm follows three logical rules:

1. **Compare** the current contact with the next contact (`current.Name == current.Next.Name`).
2. **Delete the duplicate** if names match by repointing `current.Next` to `current.Next.Next`.
3. **Advance** `current = current.Next` only when the names are distinct.

```csharp
public void RemoveDuplicates()
{
    ContactNode? current = Head;

    // We need both current and current.Next to compare two adjacent contacts
    while (current != null && current.Next != null)
    {
        // 1. Compare current contact with next contact
        if (current.Name == current.Next.Name)
        {
            // 2. Delete duplicate by bypassing current.Next
            current.Next = current.Next.Next;

            // CRITICAL: Do NOT advance 'current' here!
            // The new current.Next may also be a duplicate of current.
        }
        else
        {
            // 3. Names differ: safely move to the next unique contact
            current = current.Next;
        }
    }
}
```

> **Why do we NOT advance `current` when a duplicate is found?**  
> Consider three identical names in a row: `Aarav → Aarav → Aarav`.  
> When the first duplicate is deleted, `current` (the first `Aarav`) is now linked to the third `Aarav`. In the next iteration, `current` must compare against the newly linked node before advancing.

---

## 🎨 Visual Step-by-Step Pointer Bypass

Here is how the pointer mutation works in memory:

### State 1: Comparison
`current` points to node 1, `current.Next` points to node 2. Both have the name `"Aarav Sharma"`.

```
        current          current.Next       current.Next.Next
           │                  │                    │
           ▼                  ▼                    ▼
     ┌───────────┐      ┌───────────┐        ┌───────────┐
     │ Aarav (1) │ ───► │ Aarav (2) │ ─────► │ Bella (3) │ ───► ...
     └───────────┘      └───────────┘        └───────────┘
```

### State 2: Pointer Bypass (`current.Next = current.Next.Next`)
We reassign the `Next` pointer of node 1 directly to node 3.

```
        current                               current.Next
           │                                       │
           ▼                                       ▼
     ┌───────────┐                           ┌───────────┐
     │ Aarav (1) │ ────────────────────────► │ Bella (3) │ ───► ...
     └───────────┘                           └───────────┘
           │
           │ (bypassed node 2 is no longer reachable and is garbage collected)
           ▼
     ┌───────────┐
     │ Aarav (2) │
     └───────────┘
```

Because node 2 has no incoming references, the .NET Garbage Collector reclaims its memory. The list now contains only unique entries.

---

## 📊 Complexity Analysis

- **Time Complexity: $\mathcal{O}(n)$**  
  Every node in the list is visited at most once. Each node comparison and pointer assignment takes $\mathcal{O}(1)$ constant time.
- **Auxiliary Space Complexity: $\mathcal{O}(1)$**  
  The algorithm modifies existing object references in-place without creating new nodes, lists, or hash sets.

---

## 📁 Code Structure

```
contact-list-sort/
├── ContactNode.cs        # Node definition: Name and Next pointer
├── ContactLinkedList.cs  # Linked list implementation (Add, Display, RemoveDuplicates)
├── Program.cs            # Demo console application exercising all scenarios
├── .gitignore            # Git exclusion rules for .NET and OS artifacts
├── README.md             # This educational documentation
└── contact-list-sort.csproj # .NET 10 project file
```

---

## 🚀 How to Run

1. Open your terminal in the project directory:
   ```bash
   cd /Users/martincooper/Documents/learning-experiments/algorithms-data-structures-module/weekFour/contact-list-sort
   ```
2. Run the application:
   ```bash
   dotnet run
   ```

### Output
```text
======================================================================
    LINKED LIST DEMO: REMOVE DUPLICATES FROM A SORTED CONTACT LIST    
======================================================================

--- DEMO 1: Main Example (Aarav, Bella, Catherine) ---
Input Contact List (Sorted):  Aarav Sharma → Aarav Sharma → Bella Thomas → Catherine Lee → Catherine Lee
Output Contact List:          Aarav Sharma → Bella Thomas → Catherine Lee

--- DEMO 2: All Duplicate Nodes ---
Before: John Doe → John Doe → John Doe
After:  John Doe

--- DEMO 3: Runs of 3+ Duplicates ---
Before: Alice → Alice → Bob → Charlie → Charlie → Charlie
After:  Alice → Bob → Charlie

--- DEMO 4: Edge Cases ---
Single Node Before: Zara Patel
Single Node After:  Zara Patel
Empty List Before:  [Empty List]
Empty List After:   [Empty List]

======================================================================
All demonstrations completed successfully.
======================================================================
```
