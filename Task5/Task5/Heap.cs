namespace Task5
{
    class Heap<T>
    {
        private T[] heapify;
        private int size;
        private int capacity;
        public class Comparer : IComparer<T>
        {
            public int Compare(T x, T y)
            {
                return Comparer<T>.Default.Compare(x, y);
            }
        }
        Comparer comparer = new Comparer();
        public Heap(T[] arr)
        {
            size = arr.Length;
            capacity = size - 1;
            for (int i = 1; i < 32; i *= 2)
                capacity |= capacity >> i;
            ++capacity;
            heapify = new T[capacity];
            int index, parent; T tmp;
            for (int i = 0; i < size; ++i)
            {
                heapify[i] = arr[i];
                index = i;
                while (index > 0)
                {
                    parent = (index - 1) / 2;
                    if (comparer.Compare(heapify[parent], heapify[index]) >= 0)
                        break;
                    tmp = heapify[index];
                    heapify[index] = heapify[parent];
                    heapify[parent] = tmp;
                    index = parent;
                }
            }
        }
        public T Max()
        {
            if (size == 0) throw new HeapIsEmptyException();
            return heapify[0];
        }
        public T Extract()
        {
            if (size == 0) throw new HeapIsEmptyException();
            T max = heapify[0];
            heapify[0] = heapify[size - 1];
            int minChild, index = 0; T tmp;
            while (2 * index + 1 < size)
            {
                minChild = 2 * index + 1;
                if (minChild + 1 < size && comparer.Compare(heapify[minChild + 1], heapify[minChild]) > 0)
                    ++minChild;
                if (comparer.Compare(heapify[index], heapify[minChild]) >= 0)
                    break;
                tmp = heapify[index];
                heapify[index] = heapify[minChild];
                heapify[minChild] = tmp;
                index = minChild;
            }
            --size;
            return max;
        }
        public void Increase(int index, T y)
        {
            if (index >= size) throw new IndexOutOfHeapException();
            try
            {
                dynamic a = heapify[index];
                dynamic b = y;
                heapify[index] = a + b;
            }
            catch (WrongOperationException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            int parent; T tmp;
            while (index > 0)
            {
                parent = (index - 1) / 2;
                if (comparer.Compare(heapify[parent], heapify[index]) >= 0)
                    break;
                tmp = heapify[index];
                heapify[index] = heapify[parent];
                heapify[parent] = tmp;
                index = parent;
            }
        }
        public void Insert(T x)
        {
            if (size == capacity)
            {
                T[] newHeapify; 
                try
                {
                    capacity *= 2;
                    newHeapify = new T[capacity];
                }
                catch (TooBigHeapException ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
                for (int i = 0; i < size; ++i)
                    newHeapify[i] = heapify[i];
                heapify = newHeapify;
            }
            heapify[size] = x;
            int parent, index = size; T tmp;
            while (index > 0)
            {
                parent = (index - 1) / 2;
                if (comparer.Compare(heapify[parent], heapify[index]) >= 0)
                    break;
                tmp = heapify[index];
                heapify[index] = heapify[parent];
                heapify[parent] = tmp;
                index = parent;
            }
            ++size;
        }
        public void Merge(Heap<T> another)
        {
            Heap<T> newHeap = new Heap<T>(another.heapify);
            for (int i = 0; i < size; ++i)
                newHeap.Insert(heapify[i]);
            this.heapify = newHeap.heapify;
            this.size = newHeap.size;
            this.capacity = newHeap.capacity;
        }
    }
}