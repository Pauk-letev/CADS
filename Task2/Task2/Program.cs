using System.Numerics;
using System;
class Complex
{
    public double Re { get; set; }
    public double Im { get; set; }
    public Complex(double re, double im)
    {
        Re = re;
        Im = im;
    }
    public static Complex operator +(Complex a, Complex b)
    {
        return new Complex(a.Re + b.Re, a.Im + b.Im);
    }
    public static Complex operator -(Complex a, Complex b)
    {
        return new Complex(a.Re - b.Re, a.Im - b.Im);
    }
    public static Complex operator *(Complex a, Complex b)
    {
        return new Complex(a.Re * b.Re - a.Im * b.Im, b.Re * a.Im + a.Re * b.Im);
    }
    public static Complex operator /(Complex a, Complex b)
    {
        return new Complex((a.Re * b.Re - a.Im * b.Im) / (b.Re * b.Re + b.Im * b.Im), (b.Re * a.Im + a.Re * b.Im) / (b.Re * b.Re + b.Im * b.Im));
    }
    public double Abs()
    {
        return Math.Sqrt(Re * Re + Im * Im);
    }
    public double Arg()
    {
        return Math.Atan(Im / Re);
    }
    public string Str()
    {
        if (Im < 0)
            return Re.ToString() + Im.ToString() + "i";
        return Re.ToString() + "+" + Im.ToString() + "i";
    }
}
class program
{
    static void Instructions()
    {
        Console.WriteLine("Какое действие вы выберете?\n");
        Console.WriteLine("1 - Ввести комплексное число");
        Console.WriteLine("2 - Сложить с другим числом");
        Console.WriteLine("3 - Вычесть другое число");
        Console.WriteLine("4 - Вычесть из другого числа");
        Console.WriteLine("5 - Умножить на другое число");
        Console.WriteLine("6 - Поделить на другое число");
        Console.WriteLine("7 - Поделить другое число");
        Console.WriteLine("8 - Найти модуль и аргумент числа");
        Console.WriteLine("9 - Вывести вещественную и мнимую части числа");
        Console.WriteLine("0 - Вывести алгебраический вид числа");
        Console.WriteLine("Q - Выйти\n");
    }
    public static Complex input()
    {
        Console.Write("Введите действительную часть: ");
        double re = double.Parse(Console.ReadLine());
        Console.Write("Введите мнимую часть часть: ");
        double im = double.Parse(Console.ReadLine());
        Console.WriteLine("\n");
        return new Complex(re, im);
    }
    static void TextMenu(ref Complex complex)
    {
        Complex otherComplex;
        char choice;
        while (true)
        {
            Instructions();
            choice = Console.ReadKey().KeyChar;
            Console.Clear();
            switch (choice)
            {
                case '1':
                    complex = input();
                    break;
                case '2':
                    otherComplex = input();
                    complex += otherComplex;
                    break;
                case '3':
                    otherComplex = input();
                    complex -= otherComplex;
                    break;
                case '4':
                    otherComplex = input();
                    complex = otherComplex - complex;
                    break;
                case '5':
                    otherComplex = input();
                    complex *= otherComplex;
                    break;
                case '6':
                    otherComplex = input();
                    complex /= otherComplex;
                    break;
                case '7':
                    otherComplex = input();
                    complex = otherComplex / complex;
                    break;
                case '8':
                    Console.WriteLine("Модуль - {0}\nАргумент - {1}\n\n", complex.Abs(), complex.Arg());
                    break;
                case '9':
                    Console.WriteLine("Вещественная часть - {0}\nМнимая часть - {1}\n\n", complex.Re, complex.Im);
                    break;
                case '0':
                    Console.WriteLine("Алгебраическая форма: " + complex.Str() + "\n\n");
                    break;
                case 'q':
                    Console.WriteLine("Пака");
                    return;
                default:
                    Console.WriteLine("Неизвестная команда\n\n");
                    break;
            }
        }
    }
    static void Main(string[] args)
    {
        Complex complex = new Complex(0, 0);
        TextMenu(ref complex);
    }
}