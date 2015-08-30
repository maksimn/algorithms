using System;

namespace DataStructures {
    public class Queue<T> : ArrayBasedStructure<T> {
        private Int32 head; // Голова очереди - откуда убирается элемент
        private Int32 tail; // Хвост - куда добавляется новый элемент
        public Queue(Int32 size = 100) : base(size) {
            Initialize();
        }
        private void Initialize() {
            head = tail = 0;
        }
        public void Enqueue(T obj) {
            if (head == tail + 1 || (head == 0 && tail == array.Length - 1)) {
                throw new Exception("The queue is full.");
            }
            array[tail] = obj;
            if (tail == array.Length - 1) {
                tail = 0;
            } else {
                tail++;
            }
        }
        public T Dequeue() {
            if (head == tail) {
                throw new Exception("Queue is empty");
            }
            T x = array[head];
            if (head == array.Length - 1) {
                head = 0;
            } else {
                head++;
            }
            return x;
        }
    }
}
