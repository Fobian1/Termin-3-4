using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

public static class MyInsertionTest {

    public static void Main() {
        int[] data = ReadIntfile("smallints"); // Also try "largeints"!
        int N = data.Length;    // Change to some smaller number to test on part of array.

        // Look at numbers before sorting, unless there are too many of them.
        if (N <= data.Length) {
            for (int i = 0; i < N; i++) {
                //System.Console.Write(data[i] + " ");
            }
            System.Console.Write("\n\n");
        }
        Shuffle(data, 0, data.Length - 1);
        long before = Environment.TickCount;
        //DoMergeSort(data);
        QuickSort_Recursive(data, 0, data.Length - 1);
        long after = Environment.TickCount;

        // Look at numbers after sorting, unless there are too many of them.
        if (N <= 1000) {
            for (int i = 0; i < N; i++) {
                System.Console.Write(data[i] + " ");
            }
            System.Console.Write("\n");
        }

        if (IsSorted(data, 0, N - 1)) {
            System.Console.WriteLine((after - before) / 1000.0 + " seconds");
        }
        Console.WriteLine("Press Enter to exit");
        Console.ReadLine();
    }

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
            for (int j = 0; j < 4; j++) {
                ints[i] += (bytes[i * 4 + j] & 255) << (3 - j) * 8;
            }
        }
        return ints;
    }   

    //Mergesort ->
    public static void DoMergeSort(this int[] Mergedata) {
        var sortedNumbers = MergeSort(Mergedata);
        for (int i = 0; i < sortedNumbers.Length; i++) {
            Mergedata[i] = sortedNumbers[i];
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
    static int Quicksort(int[] Quickdata, int left, int right) {
        int pivot = Quickdata[left];
        while (true) {
            while (Quickdata[left] < pivot)
                left++;

            while (Quickdata[right] > pivot)
                right--;

            //if (left < right && Quickdata[left] == Quickdata[right])
            //    left++;
            if (left < right) {
                int temp = Quickdata[right];
                Quickdata[right] = Quickdata[left];
                Quickdata[left] = temp;
                left++;
                right--;
            } else {
                return right;
            }

        }
    }

    static public void QuickSort_Recursive(int[] arr, int left, int right) {

        // For Recusrion
        if (left < right) {
            int pivot = Quicksort(arr, left, right);
            if(pivot < 2) {
                Console.WriteLine("");
            }

            if (pivot > 1)
                QuickSort_Recursive(arr, left, pivot - 1);

            if (pivot + 1 < right)
                QuickSort_Recursive(arr, pivot + 1, right);
            
        }
        
    }
}

