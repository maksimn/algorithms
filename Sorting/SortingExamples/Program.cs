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

class Program {
    static void Main(string[] args) {
        int[] arr = new int[] { 1, 5, 0, 89, 3, 7, 9, 53 };
        arr.BubbleSort(new IntAscComparer());
        arr.ConsolePrint();
    }
}