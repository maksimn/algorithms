using System; 

public class Pair<T, U> {
    public Pair() {
    }
    public Pair(T first, U second) {
        First = first;
        Second = second;
    }
    public T First { get; set; }
    public U Second { get; set; }
}

static class DivideAndConquerExtensions {
    public static Pair<Int32, Int32> FindMaxSubarrayFast(this Int32[] a) {
        Int64 currMaxSum = 0;
        Int64 maxSuffixSum = 0;
        if (a.Length > 0) {
            currMaxSum = a[0];
            maxSuffixSum = a[0];
        } else {
            return null;
        }
        var currMax = new Pair<Int32, Int32>(0, 0);
        var maxSuffix = new Pair<Int32, Int32>(0, 0);

        for (Int32 i = 1; i < a.Length; i++) {
            // 1. Найти максимальный подмассив-суффикс массива a[0...i] и его сумму
            maxSuffix.Second = i;
            if (maxSuffixSum >= 0) {
                maxSuffixSum += a[i];
            } else {
                maxSuffixSum = a[i];
                maxSuffix.First = i;
            }
            // 2. Найти максимальный подмассив массива a[0...i]
            if (maxSuffixSum >= currMaxSum) {
                currMaxSum = maxSuffixSum;
                currMax.First = maxSuffix.First;
                currMax.Second = maxSuffix.Second;
            }
        }
        return currMax;
    }
}

public class Matrix<T> {
    private T[,] _val; 
    public Matrix(Int32 m, Int32 n) {
        _val = new T[m, n];
    }
    public T this[Int32 i, Int32 j] {
        get {
            return _val[i - 1, j - 1];
        }
        set {
            _val[i - 1, j - 1] = value;
        }
    }
    public Int32 NumRows {
        get {
            return _val.GetLength(0);
        }
    }
    public Int32 NumColumns {
        get {
            return _val.GetLength(1);
        }
    }
    public static Matrix<T> operator +(Matrix<T> a, Matrix<T> b) {
        if(a.NumRows != b.NumRows || a.NumColumns != b.NumColumns) {
            return null;
        }
        Matrix<T> c = new Matrix<T>(a.NumRows, a.NumColumns);
        for (Int32 i = 1; i <= a.NumRows; i++) {
            for (Int32 j = 1; j <= a.NumColumns; j++) {
                //c[i, j] = a[i, j] + b[i, j]; it works in C++, but it doesn't in C#
                //c[i, j] = (T)typeof(T).GetMethod("op_Addition").Invoke(null, new Object[] { a, b }); doesn't work with primitives
                dynamic val1 = a[i, j], val2 = b[i, j];
                dynamic res = val1 + val2;
                c[i, j] = (T)res;
            }
        }
        return c;
    }
    public void ConsolePrint() {
        for (Int32 i = 0; i < _val.GetLength(0); i++) {
            Console.Write("| ");
            for (Int32 j = 0; j < _val.GetLength(1); j++) {
                Console.Write(_val[i, j] + " ");
            }
            Console.Write(" |\n");
        }
    }
}