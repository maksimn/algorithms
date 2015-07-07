using System;

namespace DataStructures {
    public class Stack<T> {
        private Int32 top;
        private T[] array;
        private Int32 maxSize = 100;
        public Stack() {
            InternalArrayInitializer();
        }
        public Stack(Int32 size) {
            if (size > 0) {
                maxSize = size;
                InternalArrayInitializer();
            } else {
                throw new ArgumentOutOfRangeException("size", 
                    "size argument must be integer that is greater than zero."
                    );
            }           
        }
        private void InternalArrayInitializer() {
            array = new T[maxSize];
        }
        public Boolean IsEmpty {
            get {
                if (top == 0) {
                    return true;
                }
                return false;
            }
        }
    }
}
