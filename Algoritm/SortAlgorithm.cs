using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

public static class MyInsertionTest {
    

    public static void Main() {
        
        int counter = 0;
        #region data and suffle
        int []data = ReadIntfile("smallints"); // Also try "largeints" and smallints
        //int[] data = { 0, 10, 7, 8, 9, 1, 5, 0, 0, 1, 2, 3, 10, 9, 0, 100, 1, 0, 0, 100, 0 };
        int N = data.Length;    // Change to some smaller number to test on part of array.
                         // Look at numbers before sorting, unless there are too many of them.
        int zeroCounter = 0;
        if (N <= 1000) {
            for (int i = 0; i < data.Length; i++) {
                if(data[i] == 0) {
                    //Console.WriteLine("i = " + i);
                    zeroCounter++;
                }
                //System.Console.Write(data[i] + " ");
            }
            Console.WriteLine("Amount of zeroes: " + zeroCounter);
            System.Console.Write("\n\n");
        }
        //Shuffle(data, 0, N-1);
        #endregion
       
        long before = Environment.TickCount;
        //InsertionSort(data, 0, data.Length - 1);
        //MergeSort(data, 0, data.Length - 1);
        QuickSort(data, 0, N - 1);
        //QuickSort_Recursive(data, 0, data.Length - 1);

        long after = Environment.TickCount;

        // Look at numbers after sorting, unless there are too many of them.
        if (N <= 1000) {
            for (int i = 0; i < data.Length; i++) {
                System.Console.Write(data[i] + " ");
            }
            //System.Console.Write("\n");
        }

        if (IsSorted(data, 0, N - 1)) {
            System.Console.WriteLine((after - before) / 1000.0 + " seconds");
        }
        Console.WriteLine("Press Enter to exit");
        Console.ReadLine();
    }
    static void InsertionSort(int []a, int lo, int hi) {
        for (int i = lo; i <= hi; i++) {
            for (int j = i; j > lo && a[j] < a[j - 1]; j--) {
                int x = a[j]; a[j] = a[j - 1]; a[j - 1] = x;
            }
        }
    }

    private static bool IsSorted(int []a, int lo, int hi) {
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

    public static void Shuffle(int[] a, int lo, int hi) {
        Random rand = new Random();
        for (int i = lo; i <= hi; i++) {
            int r = i + rand.Next(hi + 1 - i);     // between i and hi
            int t = a[i]; a[i] = a[r]; a[r] = t;
        }
    }

    static int[] ReadIntfile(String filename) {
        byte[] bytes = File.ReadAllBytes(filename);
        int[] ints = new int[bytes.Length / 4];

        for (int i = 0; i < ints.Length; i++) {
            for (int j = 0; j < 4; j++) {
                ints[i] += (bytes[i * 4 + j] & 255) << (3 - j) * 8;
            }
        }
        return ints;
    }

    #region MergeSort
    public static void Merge(int[] data, int lo, int mid, int hi) {
        int i, j, k;
        int n1 = mid - lo + 1;
        int n2 = hi - mid;

        int[] Lo, Hi;
        Lo = new int[n1];
        Hi = new int[n2];

        for (i = 0; i < n1; i++) {
            Lo[i] = data[lo + i];
        }
        for (j = 0; j < n2; j++) {
            Hi[j] = data[mid + 1 + j];
        }
        i = 0;
        j = 0;
        k = lo;

        while (i < n1 && j < n2) {
            if (Lo[i] <= Hi[j]) {
                data[k] = Lo[i];
                i++;
            }
            else {
                data[k] = Hi[j];
                j++;
            }
            k++;
        }
        while (i < n1) {
            data[k] = Lo[i];
            i++;
            k++;
        }
        while (j < n2) {
            data[k] = Hi[j];
            j++;
            k++;
        }
    }
    public static void MergeSort(int[] data, int lo, int hi) {
        if (lo < hi) {
            int mid = lo + (hi - lo) / 2;
            MergeSort(data, lo, mid);
            MergeSort(data, mid + 1, hi);
            Merge(data, lo, mid, hi);
        }
    }
    #endregion

    #region QuickSort
    public static int Partition(int[] data, int lo, int hi) {
        int pivot = data[hi];
        int i = lo - 1;
        for (int j = lo; j < hi; j++) {
            if (data[j] < pivot) {
                int temp = data[i];
                data[i] = data[j];
                data[j] = temp;
                i++;
            }
        }
        int temp1 = data[i + 1];
        data[i + 1] = pivot;
        pivot = temp1;

        return i;
    }

    public static void QuickSort(int[] data, int lo, int hi) {
        if (lo < hi) {
            int pi = Partition(data, lo, hi);
            QuickSort(data, lo, pi - 1);
            QuickSort(data, pi + 1, hi);
        }
    }

    //public static void QuickSort(int[] data, int lo, int hi) {
    //    int i = lo;
    //    int j = hi;
    //    var pivot = data[hi];

    //    while (i <= j) {
    //        while (data[i] < pivot) {
    //            i++;
    //        }
    //        while (data[j] > pivot) {
    //            j--;
    //        }
    //        if (i <= j) {
    //            var tmp = data[i];
    //            data[i] = data[j];
    //            data[j] = tmp;

    //            i++;
    //            j--;
    //        }
    //    }
    //    if (lo < j) {
    //        QuickSort(data, lo, i);
    //    }
    //    if (i <  hi) {
    //        QuickSort(data, i, hi);
    //    }
    //}


    //public static void QuickSort(int[] data, int size) {
    //    if (size < 2) {
    //        return;
    //    }
    //    Random rand = new Random();
    //    int pivot, lo, hi;
    //    lo = 0;
    //    hi = size - 1;
    //    pivot = data[rand.Next(size)];
    //    while (lo <= hi) {
    //        while (data[lo] < pivot) {
    //            lo++;
    //        }
    //        while (data[hi] > pivot) {
    //            hi--;
    //        }
    //        if (lo <= hi) {
    //            int temp = data[lo];
    //            data[lo] = data[hi];
    //            data[hi] = temp;
    //            lo++;
    //            hi--;
    //        }
    //    }
    //    QuickSort(data, hi++);
    //    QuickSort(data, size - lo);
    //}
    #endregion

    static void InsertionSort(int[] a, int lo, int mid, int hi) {
        //Pukt1: Om hi inte är större än lo har vi högst ett element och då är
        //denna del av arrayen redan sorterad, så, klart!
        for (int i = lo; i <= hi; i++) {

            for (int j = i; j > lo && a[j] < a[j - 1]; j--) {
                int x = a[j];
                a[j] = a[j - 1];
                a[j - 1] = x;
                System.Console.WriteLine(a[j] + " ");
            }
        }
    }

   
    //    public static void MoveValueFromSourceToResult(List<int> list, List<int> result) {
    //        result.Add(list.First());
    //        list.RemoveAt(0);
    //    }
    //    //Quicksort ->
    //    static int Quicksort(int[] Quickdata, int left, int right) {
    //        int pivot = Quickdata[left];
    //        while (true) {
    //            while (Quickdata[left] < pivot)
    //                left++;

    //            while (Quickdata[right] > pivot)
    //                right--;

    //            //if (left < right && Quickdata[left] == Quickdata[right])
    //            //    left++;
    //            if (left < right) {
    //                int temp = Quickdata[right];
    //                Quickdata[right] = Quickdata[left];
    //                Quickdata[left] = temp;
    //                left++;
    //                right--;
    //            } else {
    //                return right;
    //            }

    //        }
    //    }

    //    static public void QuickSort_Recursive(int[] arr, int left, int right) {

    //        // For Recusrion
    //        if (left < right) {
    //            int pivot = Quicksort(arr, left, right);
    //            if(pivot < 2) {
    //                Console.WriteLine("");
    //            }

    //            if (pivot > 1)
    //                QuickSort_Recursive(arr, left, pivot - 1);

    //            if (pivot + 1 < right)
    //                QuickSort_Recursive(arr, pivot + 1, right);

    //        }
}

