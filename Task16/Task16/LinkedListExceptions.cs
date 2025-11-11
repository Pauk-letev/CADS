class LinkedListException : Exception
{
    public LinkedListException() :
        base("Какая-то ошибка!")
    { }
}
class LinkedListArgumentNullException : ArgumentNullException
{
    public LinkedListArgumentNullException() :
        base("Передаваемый объект не может быть null!")
    { }
}
class LinkedListIndexOutOfRangeException : Exception
{
    public LinkedListIndexOutOfRangeException() :
        base("Индекс улетел...")
    { }
}