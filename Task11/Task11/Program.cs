using System;
using System.IO;
class Program
{
    static bool Digit(char c)
    {
        return c >= '0' && c <= '9';
    }
    static bool Right(string s)
    {
        if (s[s.Length - 1] == '.' || s.Length == 0)
            return false;
        int[] vals = s.Split(".").Select(int.Parse).ToArray();
        if(vals.Length != 4) return false;
        foreach(int val in vals)
            if(val > 255) return false;
        return true;
    }
    static void Main()
    {
        string stream = "";
        using (StreamReader sr = new StreamReader("input.txt"))
        {
            while (!sr.EndOfStream)
                stream += sr.ReadLine() + "\n";
        }
        MyVector<string> vector = new MyVector<string>(stream.Split(' ', '\n').ToArray());
        string s; MyVector<string> ans = new MyVector<string>();
        foreach(string str in vector.ToArray()) {
            s = "";
            foreach (char c in str)
            {
                if (Digit(c) || c == '.') s += c;
                else
                {
                    if (s != string.Empty && Right(s)) ans.Add(s);
                    s = "";
                }
            }
            if (s != string.Empty && Right(s)) ans.Add(s);
        }
        using(StreamWriter sw = new StreamWriter("output.txt"))
        {
            for (int i = 0; i < ans.Size(); ++i)
                sw.WriteLine(ans[i]);
        }
    }
}