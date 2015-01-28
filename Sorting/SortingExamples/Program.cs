using System;
using System.Collections.Generic;

static class ArraySortingExtensions {
    public static void BubbleSort<T>(this T[] a, IComparer<T> cmp) {
        int n = a.Length;
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n - 1; j++) {
                if (cmp.Compare(a[j], a[j + 1]) > 0) {
                    Swap(ref a[j], ref a[j + 1]);
                }
            }
        }
    }

    public static void InsertionSort<T>(this T[] a, IComparer<T> cmp) {
        for (Int32 j = 1; j < a.Length; j++) {
            T key = a[j];
            Int32 i = j - 1;
            for ( ; i > -1 && cmp.Compare(a[i], key) > 0; i--) {
                a[i + 1] = a[i];
            }
            a[i + 1] = key;
        }
    }

    public static void SelectionSort<T>(this T[] a, IComparer<T> cmp) {
        for (Int32 i = 0, n = a.Length; i < n - 1; i++) {
            Int32 ind = i;
            for (Int32 j = i; j < n; j++) {
                if (cmp.Compare(a[j], a[ind]) < 0) {
                    ind = j;
                }
            }
            Swap(ref a[i], ref a[ind]);
        }
    }

    public static Boolean SequentialSearch<T>(this T[] a, T val) {
        foreach (var elem in a) {
            if (elem.Equals(val)) {
                return true;
            }
        }
        return false;
    }

    public static void Swap<T>(ref T a, ref T b) {
        T t = a;
        a = b;
        b = t;
    }

    public static void ConsolePrint<T>(this T[] a) {
        foreach (T x in a) {
            Console.Write(x + " ");
        }
        Console.WriteLine();
    }
}

public class IntAscComparer : IComparer<int> {
    public int Compare(int x, int y) {
        return x - y;
    }
}

public class IntDescComparer : IComparer<Int32> {
    public Int32 Compare(Int32 x, Int32 y) {
        return y - x;
    }
}

class Program {
    static void Main(String[] args) {
        int[] arr = new int[] { 1, 5, 0, 89, 3, 7, 9, 53 };
        arr.BubbleSort(new IntAscComparer());
        arr.ConsolePrint();
        arr.InsertionSort(new IntDescComparer());
        arr.ConsolePrint();
        arr.SelectionSort(new IntAscComparer());
        arr.ConsolePrint();
        Console.WriteLine("Is 5 in arr? : " + arr.SequentialSearch(5));
        Console.WriteLine("Is 105 in arr? : " + arr.SequentialSearch(105)); 
    }
}