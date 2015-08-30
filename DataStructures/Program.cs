using System;

namespace DataStructures {
    class Program {
        internal static void Main() {
            try {
                StackDemo();
            } catch {
            }
            QueueDemo();
        }
        private static void StackDemo() {
            Stack<String> stack = new Stack<String>(10);
            stack.Push("Max");
            stack.Push("Maxim");
            stack.Push("Jeffrey");
            stack.Push("Martin");
            for (Int32 i = 0; i < 5; i++) {
                stack.Pop();
            }
        }
        private static void QueueDemo() {
            Queue<String> q = new Queue<String>(6);
            q.Enqueue("4");
            q.Enqueue("1");
            q.Enqueue("3");
            q.Dequeue();
            q.Enqueue("8");
            q.Dequeue();
        }
    }
}
