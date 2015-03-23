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
            StrassenMultiplication(a, b, c); // Strassen algorithm
            return c;
        }
        for (Int32 i = 1; i <= a.NumRows; i++) { // Trivial algorithm:            
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
        var aInd = new SubmatrixIndices() { i_min = 1, j_min = 1, n = a.NumRows };
        StrassenMultiplication(a, aInd, b, aInd, c, aInd);
    }
    private static void StrassenMultiplication(Matrix<T> a, SubmatrixIndices aIndices, 
        Matrix<T> b, SubmatrixIndices bIndices, Matrix<T> c, SubmatrixIndices cIndices) {
        if (aIndices.n == 1) { // Здесь нужно реализовать перемножение матриц единичного размера
            dynamic val1 = a[aIndices.i_min, aIndices.j_min],
                    val2 = b[bIndices.i_min, bIndices.j_min];
            c[cIndices.i_min, cIndices.j_min] = val1 * val2;
            return;
        }
        // 1. Разбиение на подматрицы
        Int32 sz = aIndices.n / 2;
        var a11 = new SubmatrixIndices() { i_min = aIndices.i_min, j_min = aIndices.j_min, n = sz };
        var a12 = new SubmatrixIndices() { i_min = aIndices.i_min, j_min = aIndices.j_min + sz, n = sz };
        var a21 = new SubmatrixIndices() { i_min = aIndices.i_min + sz, j_min = aIndices.j_min, n = sz };
        var a22 = new SubmatrixIndices() { i_min = aIndices.i_min + sz, j_min = aIndices.j_min + sz, n = sz };

        var b11 = new SubmatrixIndices() { i_min = bIndices.i_min, j_min = bIndices.j_min, n = sz };
        var b12 = new SubmatrixIndices() { i_min = bIndices.i_min, j_min = bIndices.j_min + sz, n = sz };
        var b21 = new SubmatrixIndices() { i_min = bIndices.i_min + sz, j_min = bIndices.j_min, n = sz };
        var b22 = new SubmatrixIndices() { i_min = bIndices.i_min + sz, j_min = bIndices.j_min + sz, n = sz };

        var c11 = new SubmatrixIndices() { i_min = cIndices.i_min, j_min = cIndices.j_min, n = sz };
        var c12 = new SubmatrixIndices() { i_min = cIndices.i_min, j_min = cIndices.j_min + sz, n = sz };
        var c21 = new SubmatrixIndices() { i_min = cIndices.i_min + sz, j_min = cIndices.j_min, n = sz };
        var c22 = new SubmatrixIndices() { i_min = cIndices.i_min + sz, j_min = cIndices.j_min + sz, n = sz };

        // 2. Создание и вычисление матриц S[1], ..., S[10]
        Matrix<T>[] S = new Matrix<T>[10];
        for (Int32 i = 0; i < S.Length; i++) {
            S[i] = new Matrix<T>(sz, sz);
        }
        SubmatrixIndices sInd = new SubmatrixIndices() { i_min = 1, j_min = 1, n = sz };
        SubmatrixSubtract(b, b12, b, b22, S[0], sInd);
        SubmatrixSum(a, a11, a, a12, S[1], sInd);
        SubmatrixSum(a, a21, a, a22, S[2], sInd);
        SubmatrixSubtract(b, b21, b, b11, S[3], sInd);
        SubmatrixSum(a, a11, a, a22, S[4], sInd);
        SubmatrixSum(b, b11, b, b22, S[5], sInd);
        SubmatrixSubtract(a, a12, a, a22, S[6], sInd);
        SubmatrixSum(b, b21, b, b22, S[7], sInd);
        SubmatrixSubtract(a, a11, a, a21, S[8], sInd);
        SubmatrixSum(b, b11, b, b12, S[9], sInd);

        // 3. Создание и рекурсивное вычисление матриц P[1], ..., P[7]
        Matrix<T>[] P = new Matrix<T>[7];
        for (Int32 i = 0; i < P.Length; i++) {
            P[i] = new Matrix<T>(sz, sz);
        }
        StrassenMultiplication(a, a11, S[0], sInd, P[0], sInd);
        StrassenMultiplication(S[1], sInd, b, b22, P[1], sInd);
        StrassenMultiplication(S[2], sInd, b, b11, P[2], sInd);
        StrassenMultiplication(a, a22, S[3], sInd, P[3], sInd);
        StrassenMultiplication(S[4], sInd, S[5], sInd, P[4], sInd);
        StrassenMultiplication(S[6], sInd, S[7], sInd, P[5], sInd);
        StrassenMultiplication(S[8], sInd, S[9], sInd, P[6], sInd);

        // 4. Вычисление подматриц C11, C12, C21, C22, линейных по P[1] ... P[7]
        Matrix<T> temp1 = new Matrix<T>(sz, sz);
        SubmatrixSum(P[4], sInd, P[3], sInd, temp1, sInd);
        Matrix<T> temp2 = new Matrix<T>(sz, sz);
        SubmatrixSubtract(temp1, sInd, P[1], sInd, temp2, sInd);
        SubmatrixSum(temp2, sInd, P[5], sInd, c, c11);
        SubmatrixSum(P[0], sInd, P[1], sInd, c, c12);
        SubmatrixSum(P[2], sInd, P[3], sInd, c, c21);
        temp1 = new Matrix<T>(sz, sz);
        temp2 = new Matrix<T>(sz, sz);
        SubmatrixSum(P[4], sInd, P[0], sInd, temp1, sInd);
        SubmatrixSubtract(temp1, sInd, P[2], sInd, temp2, sInd);
        SubmatrixSubtract(temp2, sInd, P[6], sInd, c, c22);
    }
    private static void SubmatrixSum(Matrix<T> a, SubmatrixIndices aInd, 
            Matrix<T> b, SubmatrixIndices bInd, Matrix<T> c, SubmatrixIndices cInd) {
        SubmatrixArythm(a, aInd, b, bInd, c, cInd, (x, y) => { 
            dynamic v1 = x, v2 = y; 
            return v1 + v2; 
        });
    }
    private static void SubmatrixSubtract(Matrix<T> a, SubmatrixIndices aInd,
            Matrix<T> b, SubmatrixIndices bInd, Matrix<T> c, SubmatrixIndices cInd) {
        SubmatrixArythm(a, aInd, b, bInd, c, cInd, (x, y) => {
            dynamic v1 = x, v2 = y;
            return v1 - v2;
        });
    }
    private static void SubmatrixArythm(Matrix<T> a, SubmatrixIndices aInd, 
            Matrix<T> b, SubmatrixIndices bInd, Matrix<T> c, SubmatrixIndices cInd,
            Func<T, T, T> callback) {
        Int32 sz = aInd.n; 
        Int32 i_a = aInd.i_min, j_a = aInd.j_min, i_b = bInd.i_min, j_b = bInd.j_min;
        for (Int32 i = 0; i < sz; i++) {
            for (Int32 j = 0; j < sz; j++) {
                c[cInd.i_min + i, cInd.j_min + j] = callback(a[i_a + i, j_a + j], b[i_b + i, j_b + j]); 
            }
        }
    }
    private struct SubmatrixIndices {
        public Int32 i_min, j_min, n;
    }
}