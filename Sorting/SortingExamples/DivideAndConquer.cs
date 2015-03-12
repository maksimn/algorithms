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
        var aInd = new SubmatrixIndices() { i_min = 1, j_min = 1, n = n };
        StrassenMultiplication(a, aInd, b, aInd, c, aInd);
    }
    private static void StrassenMultiplication(Matrix<T> a, SubmatrixIndices aIndices, 
        Matrix<T> b, SubmatrixIndices bIndices, Matrix<T> c, SubmatrixIndices cIndices) {
        // 1. Разбиение на подматрицы
        Int32 sz = aIndices.n / 2;
        SubmatrixIndices a11 = new SubmatrixIndices() { 
            i_min = aIndices.i_min, j_min = aIndices.j_min, n = sz 
        };
        SubmatrixIndices a12 = new SubmatrixIndices() {
            i_min = aIndices.i_min, j_min = aIndices.j_min + sz, n = sz
        };
        SubmatrixIndices a21 = new SubmatrixIndices() {
            i_min = aIndices.i_min + sz, j_min = aIndices.j_min, n = sz
        };
        SubmatrixIndices a22 = new SubmatrixIndices() {
            i_min = aIndices.i_min + sz, j_min = aIndices.j_min + sz, n = sz
        };
        SubmatrixIndices b11 = new SubmatrixIndices() {
            i_min = bIndices.i_min, j_min = bIndices.j_min, n = sz
        };
        SubmatrixIndices b12 = new SubmatrixIndices() {
            i_min = bIndices.i_min, j_min = bIndices.j_min + sz, n = sz
        };
        SubmatrixIndices b21 = new SubmatrixIndices() {
            i_min = bIndices.i_min + sz, j_min = bIndices.j_min, n = sz
        };
        SubmatrixIndices b22 = new SubmatrixIndices() {
            i_min = bIndices.i_min + sz, j_min = bIndices.j_min + sz, n = sz
        };
        SubmatrixIndices c11 = new SubmatrixIndices() {
            i_min = cIndices.i_min, j_min = cIndices.j_min, n = sz
        };
        SubmatrixIndices c12 = new SubmatrixIndices() {
            i_min = cIndices.i_min, j_min = cIndices.j_min + sz, n = sz
        };
        SubmatrixIndices c21 = new SubmatrixIndices() {
            i_min = cIndices.i_min + sz, j_min = cIndices.j_min, n = sz
        };
        SubmatrixIndices c22 = new SubmatrixIndices() {
            i_min = cIndices.i_min + sz, j_min = cIndices.j_min + sz, n = sz
        };
        // Создание и вычисление матриц S_1, ..., S_10
        Matrix<T>[] S = new Matrix<T>[10];
        for (Int32 i = 0; i < S.Length; i++) {
            S[i] = new Matrix<T>(sz, sz);
        }
        SubmatrixIndices sInd = new SubmatrixIndices() { i_min = 1, j_min = 1, n = sz };
        SubmatrixSubtract(b, b12, b, b22, S[0], sInd);
        SubmatrixSum(a, a11, a, a12, S[1], sInd);
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
        Matrix<T> res = new Matrix<T>(sz, sz);
        Int32 i_a = aInd.i_min, j_a = aInd.j_min, i_b = bInd.i_min, j_b = bInd.j_min;
        for (Int32 i = 0; i < sz; i++) {
            for (Int32 j = 0; j < sz; j++) { 
                res[i + 1, j + 1] = callback(a[i_a + i, j_a + j], b[i_b + i, j_b + j]);
            }
        }
    }
    private struct SubmatrixIndices {
        public Int32 i_min, j_min, n;
    }
}