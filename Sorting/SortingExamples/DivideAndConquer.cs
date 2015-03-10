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
        Matrix<T>[] S = ComputeTenSMatrices(a, b);
    }
    private static Matrix<T>[] ComputeTenSMatrices(Matrix<T> a, Matrix<T> b) {
        Int32 n = a.NumRows;
        // Computing 10 S matrices:
        Matrix<T>[] S = new Matrix<T>[10];
        for (Int32 i = 0; i < S.Length; i++) {
            S[i] = new Matrix<T>(n / 2, n / 2);
        }
        for (Int32 i = 1; i <= n / 2; i++) {
            for (Int32 j = 1; j <= n / 2; j++) {
                // Computing S_1 = B_12 - B_22
                dynamic val1 = b[i, n / 2 + j], val2 = b[n / 2 + i, n / 2 + j];
                S[0][i, j] = val1 - val2;
                // S_2 = A_11 + A_12
                val1 = a[i, j];
                val2 = a[i, n / 2 + j];
                S[1][i, j] = val1 + val2;
                // S_3 = A_21 + A_22
                val1 = a[n / 2 + i, j];
                val2 = a[n / 2 + i, n / 2 + j];
                S[2][i, j] = val1 + val2;
                // S_4 = B_21 - B_11
                val1 = b[n / 2 + i, j];
                val2 = b[i, j];
                S[3][i, j] = val1 - val2;
                // S_5 = A_11 + A_22
                val1 = a[i, j];
                val2 = a[n / 2 + i, n / 2 + j];
                S[4][i, j] = val1 + val2;
                // S_6 = B_11 + B_22
                val1 = b[i, j];
                val2 = b[n / 2 + i, n / 2 + j];
                S[5][i, j] = val1 + val2;
                // S_7 = A_12 - A_22
                val1 = a[i, n / 2 + j];
                val2 = a[n / 2 + i, n / 2 + j];
                S[6][i, j] = val1 - val2;
                // S_8 = B_21 + B_22
                val1 = b[n / 2 + i, j];
                val2 = b[n / 2 + i, n / 2 + j];
                S[7][i, j] = val1 + val2;
                // S_9 = A_11 - A_21
                val1 = a[i, j];
                val2 = a[n / 2 + i, j];
                S[8][i, j] = val1 - val2;
                // S_10 = B_11 + B_12
                val1 = b[i, j];
                val2 = b[i, n / 2 + j];
                S[9][i, j] = val1 + val2;
            }
        }
        return S;
    }
}