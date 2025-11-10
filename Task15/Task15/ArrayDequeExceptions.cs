class ArrayDequeException : Exception
{
    public ArrayDequeException() :
        base("Какая-то ошибка!")
    { }
}
class ArrayDequeArgumentNullException : ArgumentNullException
{
    public ArrayDequeArgumentNullException() :
        base("Передаваемый объект не может быть null!")
    { }
}
class ArrayDequeOutOfMemoryException : OutOfMemoryException
{
    public ArrayDequeOutOfMemoryException() :
        base("Слишком большой дек!")
    { }
}