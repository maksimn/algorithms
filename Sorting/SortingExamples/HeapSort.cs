using System; 
using System.Collections.Generic;
using System.Runtime.CompilerServices;
    
public static class HeapSortExtensions {
    // Функция поддержки свойства [невозрастающей/неубывающей] пирамиды для элемента i массива a
    public static void Heapify<T>(this T[] a, Int32 i, IComparer<T> cmp) {
        Int32 extremal = -1;
        Int32 left = i == 0 ? 1 : 2 * i;
        Int32 right = i == 0 ? 2 : 2 * i + 1;
        if (left < a.GetHeapSize() && cmp.Compare(a[left], a[i]) > 0) {
            extremal = left;
        } else {
            extremal = i;
        }
        if (right < a.GetHeapSize() && cmp.Compare(a[right], a[extremal]) > 0) {
            extremal = right;
        }
        if (extremal != i) {
            Swap(ref a[i], ref a[extremal]);
            Heapify(a, extremal, cmp);
        }
    }
    // Функция построения пирамиды по заданному массиву a
    public static void BuildHeap<T>(this T[] a, IComparer<T> cmp) {
        a.SetHeapSize(a.Length);
        for (Int32 i = a.Length / 2; i >= 0; i--) {
            a.Heapify(i, cmp);
        }
    }
    // Алгоритм HeapSort
    public static void HeapSort<T>(this T[] a, IComparer<T> cmp) {
        a.BuildHeap(cmp);
        for (Int32 i = a.Length - 1; i > 0; i--) {
            Swap(ref a[0], ref a[i]);
            a.SetHeapSize(a.GetHeapSize() - 1);
            a.Heapify(0, cmp);
        }
    }

    public static void Swap<T>(ref T a, ref T b) {
        T t = a;
        a = b;
        b = t;
    }

    // To extend Int32[] arrays with 'HeapSize' "property" via getter and setter.
    private static readonly ConditionalWeakTable<Object, Int32Object> heapSize = new ConditionalWeakTable<Object, Int32Object>();

    public static Int32 GetHeapSize<T>(this T[] a) {
        return heapSize.GetOrCreateValue(a).Value;
    }

    public static void SetHeapSize<T>(this T[] a, Int32 val) {
        heapSize.GetOrCreateValue(a).Value = val; 
    }

    private class Int32Object {
        public Int32 Value { get; set; }
    }
}
