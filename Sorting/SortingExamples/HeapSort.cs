using System; 
using System.Runtime.CompilerServices;
    
public static class HeapSortExtensions {
    private class Int32Object {
        public Int32 Value { get; set; }
    }

    private static void MaxHeapify(this Int32[] a, Int32 i) {
        Int32 largest = -1;
        Int32 left = i == 0 ? 1 : i << 1;
        Int32 right = i == 0 ? 2 : (i << 1) + 1;
        if (left < a.GetHeapSize() && a[left] > a[i]) {
            largest = left;
        } else {
            largest = i;
        }
    }

    // To extend Int32[] arrays with 'HeapSize' "property" via getter and setter.
    private static readonly ConditionalWeakTable<Int32[], Int32Object> heapSize = new ConditionalWeakTable<Int32[], Int32Object>();

    public static Int32 GetHeapSize(this Int32[] a) {
        return heapSize.GetOrCreateValue(a).Value;
    }

    public static void SetHeapSize(this Int32[] a, Int32 val) {
        heapSize.GetOrCreateValue(a).Value = val; 
    }
}
