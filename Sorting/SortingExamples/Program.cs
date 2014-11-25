using System;
using System.Collections.Generic;

static class ArraySortingExtensions {
    public static void BubbleSort(this int[] a) {
        int n = a.Length;
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n - 1; j++) {
                if (a[j] > a[j + 1]) {
                    Swap(ref a[j], ref a[j + 1]);
                }
            }
        }
    }

    public static void Swap(ref int a, ref int b) {
        int t = a;
        a = b;
        b = t;
    }

    public static void ConsolePrint(this int[] a) {
        foreach (int x in a) {
            Console.Write(x + " ");
        }
        Console.WriteLine();
    }
        
    public static void BubbleSort<T>(this List<T> a, Comparer<T> cmp) {
        int n = a.Count;
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n - 1; j++) {
                if (cmp.Compare(a[j], a[j + 1]) < 0) {
                    T t = a[j];
                    a[j] = a[j + 1];
                    a[j + 1] = t;
                }
            }
        }
    }

    public static void ConsolePrint<T>(this List<T> a) {
        for (int i = 0; i < a.Count; i++) {
            Console.Write(a[i] + " ");
        }
        Console.WriteLine();
    }
}

public class IntComparer : Comparer<int> {
    public override int Compare(int x, int y) {
        return y - x;
    }
}

class Program {
    static void Main(string[] args) {
        int[] arr = new int[] { 1, 5, 0, 89, 3, 7, 9, 53 };
        arr.BubbleSort();
        arr.ConsolePrint();

        List<int> a = new List<int>();
        Random rand = new Random();
        for (int i = 0; i < 10; i++) {
            a.Add(rand.Next(0, 100));
        }
        a.BubbleSort(new IntComparer());
        a.ConsolePrint();
    }
}