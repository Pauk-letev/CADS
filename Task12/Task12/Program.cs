class Program
{
    static void Main()
    {
        MyStack<int> stack = new MyStack<int>();
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        stack.Push(4);
        Console.WriteLine(stack.Search(0));
        Console.WriteLine(stack.Search(4));
        Console.WriteLine(stack.Search(5));
        Console.WriteLine(stack.Pop());
        Console.WriteLine(stack.Pop());
        Console.WriteLine(stack.Pop());
    }
}