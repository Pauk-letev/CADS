using Task5;
class Program
{
    public static void Main(string[] args)
    {
        int[] intArr = { 5, 3, 1, 0, -57, 12 };
        Heap<int> heap = new Heap<int>(intArr);
        Console.WriteLine(heap.Max());
        heap.Increase(5, 65);
        //heap.Increase(8, 45); выведет IndexOutOfHeapException 
        Console.WriteLine(heap.Max());

        int[] intArr2 = { 4, 6, 2, 1, 500 };
        Heap<int> extraHeap = new Heap<int>(intArr2);
        heap.Merge(extraHeap);
        Console.WriteLine(heap.Max());

        string[] stringArr = { "Dada", "netnet", "AGAAGA", "Dad", "ag" };
        Heap<string> h = new Heap<string>(stringArr);
        Console.WriteLine(h.Max());
        h.Increase(3, "df");

        double[] doubleArr = { };
        Heap<double> heapD = new Heap<double>(doubleArr);
        //Console.WriteLine(heapD.Max()); выведет HeapIsEmptyException
    }
}