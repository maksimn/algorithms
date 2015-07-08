using System;

namespace DataStructures {
    abstract class ArrayBasedStructure<T> {
        protected T[] array;
        protected Int32 maxSize = 100;
        protected ArrayBasedStructure() {
            InternalArrayInitializer();
        }
        protected ArrayBasedStructure(Int32 size) {
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
    }
}
