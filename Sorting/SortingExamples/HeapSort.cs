using System; 
using System.Runtime.CompilerServices;
    
public static class HeapSortExtensions {
    // Функция поддержки свойства пирамиды для элемента i массива a
    public static void MaxHeapify(this Int32[] a, Int32 i) {
        Int32 largest = -1;
        Int32 left = i == 0 ? 1 : 2 * i;
        Int32 right = i == 0 ? 2 : 2 * i + 1;
        if (left < a.GetHeapSize() && a[left] > a[i]) {
            largest = left;
        } else {
            largest = i;
        }
        if (right < a.GetHeapSize() && a[right] > a[largest]) {
            largest = right;
        }
        if (largest != i) {
            Swap(ref a[i], ref a[largest]);
            MaxHeapify(a, largest);
        }
    }
    // Функция построения пирамиды по заданному массиву a
    public static void BuildMaxHeap(this Int32[] a) {
        a.SetHeapSize(a.Length);
        for (Int32 i = a.Length / 2; i >= 0; i--) {
            a.MaxHeapify(i);
        }
    }
    // Алгоритм HeapSort
    public static void HeapSort(this Int32[] a) {
        a.BuildMaxHeap();
        for (Int32 i = a.Length - 1; i > 0; i--) {
            Swap(ref a[0], ref a[i]);
            a.SetHeapSize(a.GetHeapSize() - 1);
            a.MaxHeapify(0);
        }
    }

    public static void Swap<T>(ref T a, ref T b) {
        T t = a;
        a = b;
        b = t;
    }

    // To extend Int32[] arrays with 'HeapSize' "property" via getter and setter.
    private static readonly ConditionalWeakTable<Int32[], Int32Object> heapSize = new ConditionalWeakTable<Int32[], Int32Object>();

    public static Int32 GetHeapSize(this Int32[] a) {
        return heapSize.GetOrCreateValue(a).Value;
    }

    public static void SetHeapSize(this Int32[] a, Int32 val) {
        heapSize.GetOrCreateValue(a).Value = val; 
    }

    private class Int32Object {
        public Int32 Value { get; set; }
    }
}
