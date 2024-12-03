using System;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class DataStructure : MonoBehaviour
{
    private LinkedList<int> linkedList;
    private Queue<int> queue;
    private Stack<int> stack;
    private Dictionary<string, int> dictionary;
    private PriorityQueue priority;
    private HashSet<int> hashSet;
    private HashSet<int> hashSet2;

    private void Awake()
    {
        InitiaLize();
    }
    private void OnEnable()
    {
        Stack();
        Queue();
        LinkedList();
        HashSet();
        Priority();
        TestSortingAlgorithms();
        values();
    }

    private void InitiaLize()
    {
        linkedList = new LinkedList<int>();
        queue = new Queue<int>();
        stack = new Stack<int>();
        dictionary = new Dictionary<string, int>();
        priority = new PriorityQueue();
        hashSet = new HashSet<int>();
        hashSet2 = new HashSet<int>();
    }

    private void Stack()
    {
        stack.Push(1);
        stack.Push(2);

        while (stack.Count > 0)
        {
            int item = stack.Pop();
            Debug.Log(item);
        }
    }

    private void Queue()
    {
        queue.Enqueue(3);
        queue.Enqueue(4);

        while (queue.Count > 0)
        {
            int item = queue.Dequeue();
            Debug.Log(item);
        }
    }

    private void LinkedList()
    {
        linkedList.AddFirst(5);
        linkedList.AddLast(6);

        LinkedListNode<int> node = linkedList.Find(5);
        linkedList.AddBefore(node, 4);

        node = linkedList.Find(6);
        linkedList.AddAfter(node, 7);

        Debug.Log("LinkedList");
        foreach (var item in linkedList)
        {
            Debug.Log(item);
        }
    }

    private void HashSet()
    {
        // chỉ thêm những phần tử không trùng nhau 
        hashSet.Add(8);
        hashSet.Add(9);
        hashSet.Add(10);
        hashSet.Remove(10);

        hashSet2.Add(10);
        hashSet2.Add(8);

        hashSet.ExceptWith(hashSet2);
        hashSet.SymmetricExceptWith(hashSet2);
        hashSet.IntersectWith(hashSet2);// giũ lại phần tủ chung của hai hashet
        hashSet.UnionWith(hashSet2);//họp nhất hai hashset

        Debug.Log("Hashset");
        foreach (var item in hashSet)
        {
            Debug.Log(item);
        }
    }

    private void Priority()
    {
        // Ví dụ sử dụng PriorityQueue
        priority.Enqueue(1, 10); // (value, priority)
        priority.Enqueue(2, 5);
        priority.Enqueue(3, 20);

        Debug.Log("PriorityQueue:");
        while (priority.Count > 0)
        {
            var item = priority.Dequeue();
            Debug.Log($"Value: {item.Key}, Priority: {item.Value}");
        }
    }

    private IEnumerable<int> values()
    {
        yield return 1;
        yield return 2;
        yield return 3;
        yield return 4;
        yield return 5;
    }

    // Cài đặt MergeSort
    private void MergeSort(int[] arr)
    {
        if (arr.Length <= 1) return;

        int mid = arr.Length / 2;
        int[] left = new int[mid];
        int[] right = new int[arr.Length - mid];

        Array.Copy(arr, 0, left, 0, mid);
        Array.Copy(arr, mid, right, 0, arr.Length - mid);

        MergeSort(left);
        MergeSort(right);

        Merge(arr, left, right);
    }

    private void Merge(int[] arr, int[] left, int[] right)
    {
        int i = 0, j = 0, k = 0;

        while (i < left.Length && j < right.Length)
        {
            if (left[i] <= right[j])
            {
                arr[k++] = left[i++];
            }
            else
            {
                arr[k++] = right[j++];
            }
        }

        while (i < left.Length)
        {
            arr[k++] = left[i++];
        }

        while (j < right.Length)
        {
            arr[k++] = right[j++];
        }
    }
    // Cài đặt QuickSort
    private void QuickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            int pi = Partition(arr, low, high);
            QuickSort(arr, low, pi - 1);
            QuickSort(arr, pi + 1, high);
        }
    }

    private int Partition(int[] arr, int low, int high)
    {
        int pivot = arr[high];
        int i = (low - 1);

        for (int j = low; j < high; j++)
        {
            if (arr[j] <= pivot)
            {
                i++;
                Swap(ref arr[i], ref arr[j]);
            }
        }

        Swap(ref arr[i + 1], ref arr[high]);
        return i + 1;
    }

    private void Swap(ref int x, ref int y)
    {
        int temp = x;
        x = y;
        y = temp;
    }

    // Kiểm tra các thuật toán sắp xếp
    private void TestSortingAlgorithms()
    {
        int[] quickSortArray = { 9, 7, 5, 11, 12, 2, 14, 3, 10, 6 };
        int[] mergeSortArray = { 8, 1, 6, 3, 5, 4, 7, 2 };

        Debug.Log("Before QuickSort:");
        PrintArray(quickSortArray);

        QuickSort(quickSortArray, 0, quickSortArray.Length - 1);

        Debug.Log("After QuickSort:");
        PrintArray(quickSortArray);

        Debug.Log("Before MergeSort:");
        PrintArray(mergeSortArray);

        MergeSort(mergeSortArray);

        Debug.Log("After MergeSort:");
        PrintArray(mergeSortArray);
    }

    // In mảng ra console
    private void PrintArray(int[] arr)
    {
        foreach (var item in arr)
        {
            Debug.Log(item);
        }
    }
}

public class PriorityQueue
{
    private List<KeyValuePair<int, int>> items = new List<KeyValuePair<int, int>>();

    // Thêm phần tử vào queue với độ ưu tiên
    public void Enqueue(int value, int priority)
    {
        items.Add(new KeyValuePair<int, int>(value, priority));
        items.Sort((x, y) => y.Value.CompareTo(x.Value)); // Sắp xếp giảm dần theo độ ưu tiên
    }

    // Lấy phần tử có độ ưu tiên cao nhất
    public KeyValuePair<int, int> Dequeue()
    {
        if (items.Count == 0)
        {
            throw new System.InvalidOperationException("Queue is empty.");
        }

        var item = items[0];
        items.RemoveAt(0);
        return item;
    }

    // Kiểm tra số lượng phần tử trong PriorityQueue
    public int Count
    {
        get { return items.Count; }
    }
}
