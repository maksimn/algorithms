using System;

namespace DataStructures {
    public class Stack<T> : ArrayBasedStructure<T> {
        private Int32 top; // Индекс элемента на вершине стека. -1, если стек пуст.
        public Stack() : base() {
            Initialize();
        }
        public Stack(Int32 size) : base(size) {
            Initialize();
        }
        private void Initialize() {
            top = -1;
        }
        public Boolean IsEmpty {
            get {
                if (top == -1) {
                    return true;
                }
                return false;
            }
        }
        public void Push(T obj) {
            if (top + 1 < maxSize) {
                array[++top] = obj;
            }
        }
        public T Pop() {
            if (IsEmpty) {
                throw new Exception("You cant pop from empty stack");
            }
            return array[--top + 1];
        }
    }
}
