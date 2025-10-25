namespace Task5
{
    public class HeapIsEmptyException : Exception
    {
        public HeapIsEmptyException()
            : base("Куча пуста, ээээ!") { }
    }
    public class TooBigHeapException : OutOfMemoryException
    {
        public TooBigHeapException()
            : base("Слишком большая куча, ты че!") { }
    }
    public class WrongOperationException : InvalidOperationException
    {
        public WrongOperationException()
            : base("Над данным типом невозможно провести данную операцию!!") { }
    }
    public class IndexOutOfHeapException : Exception
    {
        public IndexOutOfHeapException()
            : base("Такого индекса в куче нет!!") { }
    }
}
