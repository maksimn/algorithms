using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

public class State {
    public Int32 len;
    public Int32 link;
    public SortedDictionary<Char, Int32> next = new SortedDictionary<Char, Int32>();
}

public class ColoredState : State {
    public Byte color;
}

public class SuffixAutomaton<T> where T : State, new() {
    public Int32 Size { get; set; }
    public Int32 Last { get; set; }
    protected List<T> st = new List<T>();
    public SuffixAutomaton() {
        st.Add(new T());
        Size = 1;
        Last = 0;
        st[0].len = 0;
        st[0].link = -1;
    }
    public void Extend(Char c) {
        Int32 cur = Size++;
        st.Add(new T());
        st[cur].len = st[Last].len + 1;
        Int32 p;
        for (p = Last; p != -1 && !st[p].next.ContainsKey(c); p = st[p].link) {
            st[p].next[c] = cur;
        }
        if (p == -1) {
            st[cur].link = 0;
        } else {
            Int32 q = st[p].next[c];
            if (st[p].len + 1 == st[q].len) {
                st[cur].link = q;
            } else {
                Int32 clone = Size++;
                st.Add(new T());
                st[clone].len = st[p].len + 1;
                st[clone].next = new SortedDictionary<char, int>(st[q].next);
                st[clone].link = st[q].link;
                for (; p != -1 && st[p].next.ContainsKey(c) && st[p].next[c] == q; p = st[p].link) {
                    st[p].next[c] = clone;
                }
                st[q].link = st[cur].link = clone;
            }
        }
        Last = cur;
    }
}

public interface ISolver<in T, out TResult> {
    TResult Solve(T obj);
}

public class MaxCommonSubstrSolver : SuffixAutomaton<ColoredState>, ISolver<Object, String> {
    public Char[] delim { get; set; }
    private Boolean IsAchievableFlag = false;
    private StringBuilder answer = new StringBuilder();
    public String Solve(Object obj) {
        InitializeFromConsole();
        return MaxCommonSubstring();
    }
    private void InitColorAttr() {
        foreach (var state in st) {
            state.color = 0;
        }
    }
    public void IsAchievableAux(Int32 node, Char d, Char[] other) {
        st[node].color = 1; // Инициализация 
        Queue<Int32> queue = new Queue<Int32>();
        queue.Enqueue(node);
        while (queue.Count != 0) { // Обход графа в ширину
            Int32 u = queue.Dequeue(); // Выталкивается текущее серое состояние из начала очереди:
            // Проверяется, есть ли из него переход по нужному разделителю. Если да, то устанавливаем флаг в тру и прекращаем поиски.
            if (st[u].next.ContainsKey(d)) {
                IsAchievableFlag = true;
                return;
            }
            // Смотрим все состояния, смежные с текущим
            foreach (var transition in st[u].next) {
                // Если переход происходит не по метке с неподходящим разделителем, то добавляем состояние в очередь.
                if (!other.Contains(transition.Key)) {
                    Int32 v = transition.Value;
                    if (st[v].color == 0) {
                        st[v].color = 1;
                        queue.Enqueue(v);
                    }
                }
            }
            st[u].color = 2;
        }
    }
    public Boolean IsAchievable(Int32 node, Char d, Char[] other) {
        InitColorAttr();
        IsAchievableFlag = false;
        IsAchievableAux(node, d, other);
        return IsAchievableFlag;
    }
    public Boolean IsAllAchievable(Int32 node) {
        foreach (var di in delim) {
            Char[] other = (from del in delim where del != di select del).ToArray();
            if (!IsAchievable(node, di, other)) {
                return false;
            }
        }
        return true;
    }
    public Int32 FindStateThatMatchesMaxSubstring() {
        Int32 currMaxState = 0, longest = 0;
        for (Int32 i = 1; i < Size; i++) {
            if (IsAllAchievable(i) && st[i].len > longest) {
                currMaxState = i;
                longest = st[i].len;
            }
        }
        return currMaxState;
    }
    public void LongestSubstringBuilder(Int32 node) {
        for (Int32 i = node - 1; i >= 0; i--) {
            if (st[i].len + 1 == st[node].len && st[i].next.ContainsValue(node)) {
                foreach (var trans in st[i].next) {
                    if (trans.Value == node) {
                        answer.Append(trans.Key);
                        break;
                    }
                }
                node = i;
            }
        }
    }
    private String GetAnswer() {
        StringBuilder s = new StringBuilder();
        for (Int32 i = answer.Length - 1; i >= 0; i--) {
            s.Append(answer[i]);
        }
        return s.ToString();
    }
    public String MaxCommonSubstring() {
        Int32 state = FindStateThatMatchesMaxSubstring();
        LongestSubstringBuilder(state);
        return GetAnswer();
    }
    public void InitializeFromConsole() {
        String line = Console.ReadLine();
        Int32 n = Convert.ToInt32(line);
        if (n > 10) {
            throw new Exception("Error: Wrong number of input strings");
        }
        Char[] delimiter = new Char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        delim = delimiter.Take(n).ToArray();
        for (Int32 i = 0; i < n; i++) {
            line = Console.ReadLine();
            for (Int32 j = 0; j < line.Length; j++) {
                Extend(line[j]);
            }
            Extend(delimiter[i]);
        }
    }
}

class Program {
    static void Main(String[] args) {
        ISolver<Object, String> maxCommonSubstrSolver = new MaxCommonSubstrSolver();
        Console.WriteLine(maxCommonSubstrSolver.Solve(null));
    }
}