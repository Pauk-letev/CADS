class VectorException : Exception
{
    public VectorException() :
        base("Какая-то ошибка!")
    { }
}
class VectorArgumentNullException : ArgumentNullException
{
    public VectorArgumentNullException() :
        base("Передаваемый объект не может быть null!")
    { }
}
class VectorArgumentException : ArgumentException
{
    public VectorArgumentException() :
        base("Неверный тип объекта!")
    { }
}
class VectorOutOfMemoryException : OutOfMemoryException
{
    public VectorOutOfMemoryException() :
        base("Слишком большой вектор!")
    { }
}
class VectorIndexOutOfRangeException : Exception
{
    public VectorIndexOutOfRangeException() :
        base("Индекс улетел...")
    { }
}