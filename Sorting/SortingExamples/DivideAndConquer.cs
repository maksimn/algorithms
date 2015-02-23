using System;
using System.Collections.Generic;

static class DivideAndConquerExtensions {
    public static Tuple<Int32, Int32> FindMaxSubarrayFast(this Int32[] a) {
        Int64 currMaxSum = 0;
        Int64 maxSuffixSum = 0;
        if (a.Length > 0) {
            currMaxSum = a[0];
            maxSuffixSum = a[0];
        } else {
            return null;
        }
        var currMax = new KeyValuePair<Int32, Int32>(0, 0);
        var maxSuffix = new KeyValuePair<Int32, Int32>(0, 0);

        for (Int32 i = 1; i < a.Length; i++) {
            if (a[i] >= 0) {
                maxSuffixSum += a[i];
                maxSuffix = new KeyValuePair<Int32, Int32>(maxSuffix.Key, i);
                if (maxSuffixSum > currMaxSum) {
                    currMaxSum = maxSuffixSum;
                    currMax = new KeyValuePair<Int32, Int32>(maxSuffix.Key, maxSuffix.Value);
                }
            }
        }
        return null;
    }
}