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
    private static Boolean IsPow2(Int32 num) {
        return !Convert.ToBoolean((num & (num - 1)));
    }
    public static Matrix<T> operator +(Matrix<T> a, Matrix<T> b) {
        return _Arythmetic(a, b, (aij, bij) => { dynamic v1 = aij, v2 = bij; return v1 + v2; });
    }
    public static Matrix<T> operator -(Matrix<T> a, Matrix<T> b) {
        return _Arythmetic(a, b, (aij, bij) => { dynamic v1 = aij, v2 = bij; return v1 - v2; });
    }
    public static Matrix<T> operator *(Matrix<T> a, Matrix<T> b) {
        if (a.NumColumns != b.NumRows) {
            return null;
        }
        Matrix<T> c = new Matrix<T>(a.NumRows, b.NumColumns);
        if (a.NumRows == a.NumColumns && b.NumRows == b.NumColumns && IsPow2(a.NumRows)) {
            // Strassen algorithm
            StrassenMultiplication(a, b, c);
            return c;
        }
        // Trivial algorithm:
        for (Int32 i = 1; i <= a.NumRows; i++) {
            for (Int32 j = 1; j <= a.NumColumns; j++) {
                dynamic res = 0, val1, val2;
                for (Int32 k = 1; k <= a.NumColumns; k++) {
                    val1 = a[i, k];
                    val2 = b[k, j];
                    res += val1 * val2;
                }
                c[i, j] = res;
            }
        }
        return c;
    }
    private static Matrix<T> _Arythmetic(Matrix<T> a, Matrix<T> b, Func<T, T, T> arythmAction) {
        if (a.NumRows != b.NumRows || a.NumColumns != b.NumColumns) {
            return null;
        }
        Matrix<T> c = new Matrix<T>(a.NumRows, a.NumColumns);
        for (Int32 i = 1; i <= a.NumRows; i++) {
            for (Int32 j = 1; j <= a.NumColumns; j++) {
                c[i, j] = arythmAction(a[i, j], b[i, j]);
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
    private static void StrassenMultiplication(Matrix<T> a, Matrix<T> b, Matrix<T> c) {
        Int32 n = a.NumRows;
        var aInd = new SubmatrixIndices() { i_max = n, i_min = 1, j_max = n, j_min = 1 };
        StrassenMultiplication(a, aInd, b, aInd, c, aInd);
    }
    private static void StrassenMultiplication(Matrix<T> a, SubmatrixIndices aIndices, 
        Matrix<T> b, SubmatrixIndices bIndices, Matrix<T> c, SubmatrixIndices cIndices) {

    }
    private struct SubmatrixIndices {
        public Int32 i_max, i_min, j_max, j_min;
    }
}