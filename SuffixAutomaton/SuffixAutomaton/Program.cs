using System;
using System.Text;
using System.Collections.Generic;

public class SuffixAutomaton {
    private class State {
        public Int32 len, link;
        public SortedDictionary<Char, Int32> next = new SortedDictionary<Char, Int32>();
    }
    public Int32 N { get; set; } // число подстрок
    private List<State> st = new List<State>();
    public Int32 Size { get; set; }
    public Int32 Last { get; set; }
    public SuffixAutomaton() { 
        st.Add(new State());
        Size = 1; 
        Last = 0;
        st[0].len = 0;
        st[0].link = -1;
    }
    public void Extend(Char c) {
        Int32 cur = Size++;
        st.Add(new State());
        st[cur].len = st[Last].len + 1;
        Int32 p;
        for (p = Last; p != -1 && !st[p].next.ContainsKey(c); p = st[p].link) {
            st[p].next[c] = cur;
        }
        if (p == -1) {
            st[cur].link = 0;
        } 
        else {
            Int32 q = st[p].next[c];
            if (st[p].len + 1 == st[q].len) {
                st[cur].link = q;
            } 
            else {
                Int32 clone = Size++;
                st.Add(new State());
                st[clone].len = st[p].len + 1;
                st[clone].next = new SortedDictionary<char,int>(st[q].next);
                st[clone].link = st[q].link;
                for (; p != -1 && st[p].next.ContainsKey(c) && st[p].next[c] == q; p = st[p].link) {
                    st[p].next[c] = clone;
                }
                st[q].link = st[cur].link = clone;
            }
        }
        Last = cur;
    }
    public void Print() {
        for (Int32 i = 0; i < Size; i++) {
            Console.WriteLine("\nСостояние : {0}\nlen = {1}\nlink = {2}\nNext:", i, st[i].len, st[i].link);
            foreach (var item in st[i].next) {
                Console.WriteLine("{0} --> {1}", item.Key, item.Value);
            }
        }
    }
    private Boolean IsAchievableFlag = false;
    private Byte[] colorAttr = null;
    private void InitColorAttr() {
        colorAttr = new Byte[Size];
    }
    public void IsAchievableAux(Int32 node, Char d, Char[] other) {
        // Инициализация:
        colorAttr[node] = 1;
        Queue<Int32> queue = new Queue<Int32>();
        queue.Enqueue(node);
        // Обход графа в ширину:
        while (queue.Count != 0) {
            // Выталкивается текущее серое состояние из начала очереди:
            Int32 u = queue.Dequeue();
            // Проверяется, есть ли из него переход по нужному разделителю. Если да, то устанавливаем флаг в тру и прекращаем поиски.
            if (st[u].next.ContainsKey(d)) {
                IsAchievableFlag = true;
                return;
            }
            // Смотрим все состояния, смежные с текущим
            foreach (var state in st[u].next) {
                Boolean isMarkEqualToOther = false;
                foreach (var otherDelimiter in other) {
                    if (state.Key == otherDelimiter) {
                        isMarkEqualToOther = true;
                        break;
                    }
                }
                // Если переход происходит не по метке с неподходящим разделителем, то добавляем состояние в очередь.
                if (isMarkEqualToOther == false) {
                    Int32 v = state.Value;
                    if (colorAttr[v] == 0) {
                        colorAttr[v] = 1;
                        queue.Enqueue(v);
                    }
                }
            }
            colorAttr[u] = 2;
        }
    }
    public Boolean IsAchievable(Int32 node, Char d, Char[] other) {
        InitColorAttr();
        IsAchievableFlag = false;
        IsAchievableAux(node, d, other);
        return IsAchievableFlag;
    }
    public Boolean IsAllAchievable(Int32 node, Char[] delimiters) {
        for (Int32 i = 0; i < N; i++) {
            Char ch = delimiters[i];
            Char[] other = new Char[N - 1];
            for (Int32 k = 0, j = 0; k < N; k++) {
                if (delimiters[k] != ch) {
                    other[j++] = delimiters[k]; 
                }
            }
            if (!IsAchievable(node, ch, other)) {
                return false;
            }
        }
        return true;
    }
    public Int32 FindStateThatMatchesMaxSubstring(Char[] delimiters) {
        Int32 currMaxState = 0, longest = 0;
        for (Int32 i = 1; i < Size; i++) {
            if (IsAllAchievable(i, delimiters) && st[i].len > longest) {
                currMaxState = i;
                longest = st[i].len;
            }
        }
        return currMaxState;
    }
    public StringBuilder answer = new StringBuilder();
    public void LongestSubstringBuilder(Int32 node) {
        for (Int32 i = node - 1; i >= 0; i--) {
            if(st[i].len + 1 == st[node].len && st[i].next.ContainsValue(node)) {
                foreach (var trans in st[i].next) {
                    if (trans.Value == node) {
                        answer.Append(trans.Key);
                        break;
                    }
                }
                LongestSubstringBuilder(i);
            }
        }
    }
    public String GetAnswer() {
        StringBuilder s = new StringBuilder();
        for (Int32 i = answer.Length - 1; i >= 0; i--) {
            s.Append(answer[i]);
        }
        return s.ToString();
    }
    public String MaxCommonSubstring(Char[] delim) {
        Int32 state = FindStateThatMatchesMaxSubstring(delim);
        LongestSubstringBuilder(state);
        return GetAnswer();
    }
}

class Program {
    static void Main(String[] args) {
        SuffixAutomaton sa = new SuffixAutomaton();
        String line = Console.ReadLine();
        Int32 n = Convert.ToInt32(line);
        if (n > 10) {
            throw new Exception("Error: Wrong number of input strings");
        }
        sa.N = n;
        Char[] delimiter = new Char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        Char[] delim = new Char[n];
        for (Int32 i = 0; i < n; i++) {
            delim[i] = delimiter[i];
        }
        for (Int32 i = 0; i < n; i++) {
            line = Console.ReadLine();
            for (Int32 j = 0, length = line.Length; j < length; j++) {
                sa.Extend(line[j]);
            }
            sa.Extend(delimiter[i]);
        }
            
//        Console.WriteLine( "Total Memory Consumption {0} bytes.", GC.GetTotalMemory(false)); 
        //sa.Print();
        Console.WriteLine(sa.MaxCommonSubstring(delim));
    }
}