using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

public static class MyInsertionTest {

    static void InsertionSort(int[] a, int lo, int hi) {
        //Pukt1: Om hi inte är större än lo har vi högst ett element och då är
        //denna del av arrayen redan sorterad, så, klart!
        for (int i = lo; i <= hi; i++) {
            for (int j = i; j > lo && a[j] < a[j - 1]; j--) {
                int x = a[j]; a[j] = a[j - 1]; a[j - 1] = x;
            }
        }
    }
    private static bool IsSorted(int[] a, int lo, int hi) {
        int flaws = 0;
        for (int i = lo + 1; i <= hi; i++) {
            if (a[i] < a[i - 1]) {
                if (flaws++ >= 10) {
                    System.Console.WriteLine("...");
                    break;
                }
                System.Console.WriteLine("a[" + i + "] = " + a[i] + ", a[" + (i - 1) + "] = " + a[i + 1]);
            }
        }
        return flaws == 0;
    }

    // Shuffles the first n elements of a.
    public static void Shuffle(int[] a, int lo, int hi) {
        Random rand = new Random();
        for (int i = lo; i <= hi; i++) {
            int r = i + rand.Next(hi + 1 - i);     // between i and hi
            int t = a[i]; a[i] = a[r]; a[r] = t;
        }
    }

    static int[] ReadIntfile(String filename) {
        // Read file into a byte array, and then combine every group of four bytes to an int. (Not
        // the standard way, but it works!)
        byte[] bytes = File.ReadAllBytes(filename);
        int[] ints = new int[bytes.Length / 4];
        for (int i = 0; i < ints.Length; i++) {
            for (int j = 0; j < 4; j++) { ints[i] += (bytes[i * 4 + j] & 255) << (3 - j) * 8; }
        }
        return ints;
    }

    public static void Main() {
        int[] data = ReadIntfile("smallints"); // Also try "largeints"!
        int N = data.Length;    // Change to some smaller number to test on part of array.

        // Look at numbers before sorting, unless there are too many of them.
        if (N <= data.Length) {
            for (int i = 0; i < N; i++) { System.Console.Write(data[i] + " "); }
            System.Console.Write("\n\n");
        }

        long before = Environment.TickCount;
        DoMergeSort(data);
        long after = Environment.TickCount;

        // Look at numbers after sorting, unless there are too many of them.
        if (N <= data.Length) {
            for (int i = 0; i < N; i++) { System.Console.Write(data[i] + " "); }
            System.Console.Write("\n");
        }

        if (IsSorted(data, 0, N - 1)) {
            System.Console.WriteLine((after - before) / 1000.0 + " seconds");
        }
        Console.WriteLine("Press Enter to exit");
        Console.ReadLine();
    }

    //Mergesort ->
    public static void DoMergeSort(this int[] data) {
        var sortedNumbers = MergeSort(data);
        for (int i = 0; i < sortedNumbers.Length; i++) {
            data[i] = sortedNumbers[i];
        }
    }
    private static int[] MergeSort(int[] data) {
        if (data.Length <= 1)
            return data;

        var left = new List<int>();
        var right = new List<int>();

        for (int i = 0; i < data.Length; i++) {
            if (i % 2 > 0) {
                left.Add(data[i]);
            } else {
                right.Add(data[i]);
            }
        }

        left = MergeSort(left.ToArray()).ToList();
        right = MergeSort(right.ToArray()).ToList();

        return Merge(left, right);
    }

    private static int[] Merge(List<int> left, List<int> right) {
        var result = new List<int>();

        while (NotEmpty(left) && NotEmpty(right)) {
            if (left.First() <= right.First())
                MoveValueFromSourceToResult(left, result);
            else
                MoveValueFromSourceToResult(right, result);
        }

        while (NotEmpty(left)) {
            MoveValueFromSourceToResult(left, result);
        }
        while (NotEmpty(right))
            MoveValueFromSourceToResult(right, result);

        return result.ToArray();
    }

    public static bool NotEmpty(List<int> list) {
        return list.Count > 0;
    }

    public static void MoveValueFromSourceToResult(List<int> list, List<int> result) {
        result.Add(list.First());
        list.RemoveAt(0);
    }
    //Quicksort ->
    public static void Quicksort(IComparable[] elements, int left, int right) {
        int i = left, j = right;
        IComparable partitioning = elements[(left + right) / 2];

        while (i <= j) {
            while (elements[i].CompareTo(partitioning) < 0) {
                i++;
            }

            while (elements[j].CompareTo(partitioning) > 0) {
                j--;
            }

            if (i <= j) {
                // Swap
                IComparable tmp = elements[i];
                elements[i] = elements[j];
                elements[j] = tmp;

                i++;
                j--;
            }
        }

        // Recursive calls
        if (left < j) {
            Quicksort(elements, left, j);
        }

        if (i < right) {
            Quicksort(elements, i, right);
        }
    }

}

