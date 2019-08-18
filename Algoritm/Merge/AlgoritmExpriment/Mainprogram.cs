using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

public static class MyInsertionTest {

    static int M = 0;

    /* Kommentarer om koden:
     * Shuffle och data behöver man ändra på på två ställen
     * Data ändras på rad 19 samt på rad 42
     * Shuffle ändras på rad 28 samt rad 43
     * Ändra storlek på N görs på rad 20
     */

    public static void Main() {
        #region data and suffle
        int[] data = ReadIntfile("largeints"); // Also try "largeints" and smallints
        int N = (data.Length)/2;    // Change to some smaller number to test on part of array.
        int mTime = 0; //Snitt tiden
        
        if (N <= 1000) { // Look at numbers before sorting, unless there are too many of them.
            for (int i = 0; i < data.Length; i++) {
                System.Console.Write(data[i] + " ");
            }
        }
        //Shuffle(data, 0, N - 1); //Missa inte att aktivera båda shuffles den andra finns på rad 40
        #endregion

        while (M < N) { //Används för att skriva ut körtiden för alla olika M
            for (int i = 0; i < 10; i++) { //Finns för att hitta snitt-tiden vet att ha det som 10 är fult och borde ha skapat en variable för det
                long before = Environment.TickCount;
                MergeSort(data, 0, N - 1);
                //QuickSort(data, 0, N - 1);
                //InsertionSort(data, 0, N - 1);
                long after = Environment.TickCount;
                if (IsSorted(data, 0, N - 1)) {
                    System.Console.WriteLine("M = " + M + " tid: " + (after - before) / 1000.0 + " seconds");
                }
                mTime += (int)(after - before);
                data = ReadIntfile("largeints"); //Hade varit snyggare att ha en int[] unSorted som får värde tidigt och endast sätter värdet och inget annat
                //Shuffle(data, 0, N - 1);

            }
            mTime /= 10; //Fixar fram snitt tiden för att skrivas ut 
            System.Console.WriteLine("\nM = " + M + " Medeltiden: " + mTime / 1000.0 + " seconds\n");
            mTime = 0;
            //M *= 2;
        }
        if (N <= 1000) { // Look at numbers after sorting, unless there are too many of them.
            for (int i = 0; i < N - 1; i++) {
                System.Console.Write(data[i] + " ");
            }
            System.Console.Write("\n");
        }
        Console.WriteLine("Press Enter to exit");
        Console.ReadLine();
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
            } else {
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
        if (hi - lo <= M) {
            InsertionSort(data, lo, hi);
        } else {
            int mid = lo + (hi - lo) / 2;
            MergeSort(data, lo, mid);
            MergeSort(data, mid + 1, hi);
            Merge(data, lo, mid, hi);
        }
    }
    #endregion

    #region InsertionSort
    static void InsertionSort(int[] a, int lo, int hi) {
        for (int i = lo; i <= hi; i++) {
            for (int j = i; j > lo && a[j] < a[j - 1]; j--) {
                int x = a[j];
                a[j] = a[j - 1];
                a[j - 1] = x;
            }
        }
    }
    #endregion

    #region QuickSort
    public static void QuickSort(int[] data, int lo, int hi) {
        int i = lo;
        int j = hi;

        int pivot = data[lo + (hi - lo) / 2];

        while (i <= j) {
            while (data[i] < pivot) { //Kör här sålänge talet till vänster är mindre än pivot
                i++;
            }
            while (data[j] > pivot) { //Kör här sålänge talet till höger är större än pivot
                j--;
            }
            if (i <= j) { //När det har hittats ett tal som är mindre på vänster och större till höger byter de plats
                int temp = data[j];
                data[j] = data[i];
                data[i] = temp;
                i++;
                j--;
            }
        }
        if (hi - lo <= M) {
            InsertionSort(data, lo, hi);
        } else {
            if (lo < j) { //Kör denna sålänge höger räknaren inte är lägre än minsta möjliga
                QuickSort(data, lo, j);
            }
            if (i < hi) { //Kör denna såläng vänster räknaren inte är större än högsta möjliga
                QuickSort(data, i, hi);
            }
        }
    }
    #endregion
}


