using System;

namespace DataStructures {
    class Program {
        internal static void Main() {
            try {
                StackDemo();
            } catch {
            }
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
    }
}
