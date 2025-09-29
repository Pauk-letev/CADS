using System;
using System.IO;
using System.Linq;
class Institute
{
    public string InstituteName { get; set; }
    public int Year { get; set; }
    public string Fucalty { get; set; }
    public string Group { get; set; }
}
class Subject
{
    public string Name { get; set; }
    public int Mark { get; set; }
    public Subject(string[] input)
    {
        Name = input[0];
        Mark = int.Parse(input[1]);
    }
}
class Student : Institute
{
    public string Sername { get; set; }
    public Subject [] subject;

    public Student(string[] input)
    {
        Sername = input[0];
        InstituteName = input[1];
        Fucalty = input[2];
        Group = input[3];
        Year = int.Parse(input[4]);
        subject = new Subject[int.Parse(input[5])];
    }
    public Student()
    {
        Sername = "";
        InstituteName = "";
        Fucalty = "";
        Group = "";
        Year = 0;
    }
    public bool Great()
    {
        foreach (Subject subj in subject)
            if (subj.Mark != 5)
                return false;
        return true;
    }
    public int Doubles()
    {
        int k = 0;
        foreach (Subject subj in subject)
            if (subj.Mark == 2)
                ++k;
        return k;
    }
    public double Mean()
    {
        int s = 0;
        foreach (Subject subj in subject)
            s += subj.Mark;
        return (double)s/subject.Length;
    }
}
class DataBase
{
    public Student[] dataBase;
    private int size;
    private int capacity;
    public int[] expelled = { 0, 0, 0, 0, 0, 0 };
    public DataBase()
    {
        capacity = 1;
        size = 0;
        dataBase = new Student[capacity];
    }
    public void Add(Student student)
    {
        if (size == capacity)
        {
            Student[] tempDataBase = dataBase;
            capacity *= 2;
            dataBase = new Student[capacity];
            for (int i = 0; i < size; ++i)
                dataBase[i] = tempDataBase[i];
        }
        dataBase[size] = student;
        ++size;
    }
    public void Remove(Student student, bool setting = false)
    {
        int found = -1;
        for (int i = 0; i < size; ++i)
            if (dataBase[i].Sername == student.Sername)
                found = i;
        if (found == -1)
            return;
        for (int i = found; i < size - 1; ++i)
            dataBase[i] = dataBase[i + 1];
        if (!setting) ++expelled[dataBase[found].Year];
        --size;
    }
    public void Edit(Student student)
    {
        int found = -1;
        for (int i = 0; i < size; ++i)
            if (dataBase[i].Sername == student.Sername)
                found = i;
        if (found == -1)
            return;
        Console.WriteLine("Что вы хотели бы изменить?\n" +
            "1) фамилия;\n2) институт\n3) кафедру\n4) группу\n5) курс" +
            "6) список предметов.\n");
        int choice = int.Parse(Console.ReadLine());
        switch (choice)
        {
            case 1:
                Console.Write("Введите фамилию: ");
                dataBase[found].Sername = Console.ReadLine();
                break;
            case 2:
                Console.Write("Введите институт: ");
                dataBase[found].InstituteName = Console.ReadLine();
                break;
            case 3:
                Console.Write("Введите кафедру: ");
                dataBase[found].Fucalty = Console.ReadLine();
                break;
            case 4:
                Console.Write("Введите группу: ");
                dataBase[found].Group = Console.ReadLine();
                break;
            case 5:
                Console.Write("Введите курс обучения: ");
                dataBase[found].Year = int.Parse(Console.ReadLine());
                break;
            case 6:
                Console.Write("Введите количество предметов, по которым сдавался экзамен: ");
                int n = int.Parse(Console.ReadLine());
                dataBase[found].subject = new Subject[n];
                for (int i = 0; i < n; ++i)
                {
                    Console.Write("Введите" + i + "-й предмет: ");
                    dataBase[found].subject[i].Name = Console.ReadLine();
                    Console.Write("Введите оценку: ");
                    dataBase[found].subject[i].Mark = int.Parse(Console.ReadLine());

                }
                break;
            default:
                Console.WriteLine("Некорректная команда");
                break;
        }
    }
}
class Program
{
    static DataBase Input()
    {
        DataBase bas = new DataBase();
        string line;
        using (StreamReader sr = new StreamReader("base.txt"))
        {
            while ((line = sr.ReadLine()) != null)
            {
                Student student = new Student(line.Split().ToArray());
                for (int i = 0; i < student.subject.Length; ++i)
                    student.subject[i] = new Subject(sr.ReadLine().Split().ToArray());
                sr.ReadLine();
                bas.Add(student);
            }
        }
        return bas;
    }
    static void Instructions()
    {
        Console.WriteLine("Введите номер для любого из последующих запросов: \n");
        Console.WriteLine("1) фамилии студентов, у которых две и более двоек за сессию, и удалить их;\r\n" +
            "2) институт, на котором на первом курсе наибольшее количество отличников;\r\n" +
            "3) курс, на котором исключено большее количество студентов;\r\n" +
            "4) институт с наибольшим количеством отличников;\r\n" +
            "5) полный список отличников с указанием института, группы и курса, где они учатся;\r\n" +
            "6) группу, где нет двоечников;\r\n" +
            "7) институт и курс, на котором средний бал не меньше 3,5;\r\n" +
            "8) фамилии студентов, у которых нет троек и двоек;\r\n" +
            "9) институт и группу, где наибольшее количество отличников;\r\n" +
            "10) фамилии студентов-отличников на третьем курсе;\r\n" +
            "11) предметы и перечень кафедр, на которых они присутствуют;\r\n" +
            "12) фамилии студентов, группу и институт, где средний балл составляет 4,5;\r\n" +
            "13) студентов первого курса, у которых три двойки и удалите их;\r\n" +
            "14) группы, в которых нет двоечников;\r\n" +
            "15) фамилии студентов-отличников на первом и втором курсах по всем институтам, средний балл по каждой группе и упорядочьте группы по нему;\r\n" +
            "16) институты, на которых нет двоечников;\r\n" +
            "17) фамилии студентов, которые не явились хотя бы на один экзамен (оценка 0) и удалите тех, у которых средний балл ниже 3;\r\n" +
            "18) институт, на котором на первом курсе наибольшее количество групп, где нет двоек;\r\n" +
            "19) курс с наибольшим количеством отличников;\r\n" +
            "20) институт, на котором на первом курсе наибольшее количество двоечников;\r\n" +
            "21) группы, в которых нет отличников;\r\n" +
            "22) полный список двоечников с указанием института, группы и курса, где они учатся;\r\n" +
            "23) фамилии студентов-отличников на втором курсе с указанием группы и института, где они учатся;");
        Console.WriteLine("0) изменить данные в базе.\n");
    }
    static void Output(string outputting)
    {
        using (StreamWriter sw = new StreamWriter("output.txt", false))
        {
            sw.WriteLine(outputting);
        }
    }
    static string Response1(ref DataBase dataBase)
    {
        string result = "Студенты, получившие 2 за сессию: ";
        foreach (Student student in dataBase.dataBase)
            if (student.Doubles() > 0)
            {
                result += student.Sername + ", ";
                dataBase.Remove(student);
            }
        result += "\b\b\n\nДанные были удалены из базы";
        return result;
    }
    static string Response2(ref DataBase dataBase)
    {
        Dictionary<string, int> institutes = new Dictionary<string, int>();
        foreach (Student student in dataBase.dataBase)
            institutes[student.InstituteName] = 0;
        foreach (Student student in dataBase.dataBase)
            if (student.Year == 1 && student.Great())
                ++institutes[student.InstituteName];
        var max = institutes.First();
        foreach (var institute in institutes)
            if (max.Value < institute.Value)
                max = institute;
        return "В " + max.Key + " на первом курсе больше всего отличников";
    }
    static string Response3(ref DataBase dataBase)
    {
        int k = 0;
        for (int i = 1; i < 6; ++i)
            if (dataBase.expelled[i] > dataBase.expelled[k])
                k = i;
        return "Больше всего было ислючено студентов " + k + "-го курса";
    }
    static string Response4(ref DataBase dataBase)
    {
        Dictionary<string, int> institutes = new Dictionary<string, int>();
        foreach (Student student in dataBase.dataBase)
            institutes[student.InstituteName] = 0;
        foreach (Student student in dataBase.dataBase)
            if (student.Great()) 
                ++institutes[student.InstituteName];
        var max = institutes.First();
        foreach (var institute in institutes)
            if (max.Value < institute.Value)
                max = institute;
        return "В " + max.Key + " больше всего отличников";
    }
    static string Response5(ref DataBase dataBase)
    {
        string result = "Список отличников:\n\n";
        foreach (Student student in dataBase.dataBase)
            if (student.Great())
                result += student.Sername + ", " + student.InstituteName + ", " + student.Group + ", " + student.Year.ToString() + "-й курс\n";
        return result;
    }
    static string Response614(ref DataBase dataBase)
    {
        string result = "Двоечников нет в группах: ";
        Dictionary<string, bool> groups = new Dictionary<string, bool>();
        foreach (Student student in dataBase.dataBase)
            groups[student.Group] = true;
        foreach (Student student in dataBase.dataBase)
            if (groups[student.Group] && student.Doubles() > 0)
                groups[student.Group] = false;
        foreach (var group in groups)
            if (group.Value) result += group.Key + ", ";
        return result + "\b\b";
    }
    static string Response7(ref DataBase dataBase)
    {
        string result = "У учащихся следующих институтов средний балл не ниже 3,5:\n\n";
        Dictionary<KeyValuePair<string, int>, double> instituteYears = new Dictionary<KeyValuePair<string, int>, double>();
        foreach (Student student in dataBase.dataBase)
            instituteYears[KeyValuePair.Create(student.InstituteName, student.Year)] = 5;
        foreach (Student student in dataBase.dataBase)
            if (student.Mean() < instituteYears[KeyValuePair.Create(student.InstituteName, student.Year)])
                instituteYears[KeyValuePair.Create(student.InstituteName, student.Year)] = student.Mean();
        foreach (var instyear in instituteYears)
            if (instyear.Value > 3.5)
                result += instyear.Key.Key + ", " + instyear.Key.Value + "-й курс\n";
        return result;
    }
    static string Response8(ref DataBase dataBase)
    {
        string result = "Студенты-хорошисты: ";
        foreach (Student student in dataBase.dataBase)
        {
            bool good = true;
            foreach (Subject subject in student.subject)
                if (subject.Mark == 2 || subject.Mark == 3)
                    good = false;
            if (good) result += student.Sername + ", ";
        }
        return result + "\b\b";
    }
    static string Response9(ref DataBase dataBase)
    {
        Dictionary<KeyValuePair<string, string>, int> instituteGroups = new Dictionary<KeyValuePair<string, string>, int>();
        foreach (Student student in dataBase.dataBase)
            instituteGroups[KeyValuePair.Create(student.InstituteName, student.Group)] = 0;
        foreach (Student student in dataBase.dataBase)
            if (student.Great())
                ++instituteGroups[KeyValuePair.Create(student.InstituteName, student.Group)];
        var maxim = instituteGroups.First();
        foreach (var instgroup in instituteGroups)
            if (instgroup.Value > maxim.Value)
                maxim = instgroup;
        return "В группе " + maxim.Key.Value + " института " + maxim.Key.Key + " больше всего отличников";
    }
    static string Response10(ref DataBase dataBase)
    {
        string result = "Студенты-третьекурсники-отличники: ";
        foreach (Student student in dataBase.dataBase)
            if (student.Year == 3 && student.Great())
                result += student.Sername + ", ";
        return result + "\b\b";
    }
    static string Response11(ref DataBase dataBase)
    {
        string result = "Списки предметов:\n\n";
        Dictionary<string, string> subjects = new Dictionary<string, string>();
        foreach (Student student in dataBase.dataBase)
            foreach (Subject subject in student.subject)
                subjects[subject.Name] = "";
        foreach (Student student in dataBase.dataBase)
            foreach (Subject subject in student.subject)
                if (subjects[subject.Name].IndexOf(student.Fucalty) == -1)
                    subjects[subject.Name] += student.Fucalty + ", ";
        foreach (var subj in subjects)
            result += subj.Key + ", присутствующий на кафедрах " + subj.Value + "\b\b";
        return result;
    }
    static string Response12(ref DataBase dataBase)
    {
        string result = "Списки групп, имеющих оценку 4,5:\n\n";
        Dictionary<KeyValuePair<string, string>, int> instituteGroups = new Dictionary<KeyValuePair<string, string>, int>();
        Dictionary<KeyValuePair<string, string>, int> instituteGroupsMark = new Dictionary<KeyValuePair<string, string>, int>();
        foreach (Student student in dataBase.dataBase)
        {
            instituteGroups[KeyValuePair.Create(student.InstituteName, student.Group)] = 0;
            instituteGroupsMark[KeyValuePair.Create(student.InstituteName, student.Group)] = 0;
        }
        foreach (Student student in dataBase.dataBase)
            foreach (Subject subject in student.subject)
            {
                instituteGroupsMark[KeyValuePair.Create(student.InstituteName, student.Group)] += subject.Mark;
                ++instituteGroups[KeyValuePair.Create(student.InstituteName, student.Group)];
            }
        foreach (var instituteGroup in instituteGroups.Keys)
        {
            double mean = (double)instituteGroupsMark[instituteGroup] / instituteGroups[instituteGroup];
            if (mean == 4.5)
            {
                foreach (Student student in dataBase.dataBase)
                    if (student.InstituteName == instituteGroup.Key && student.Group == instituteGroup.Value)
                        result += student.Sername + ", ";
                result += "\b\b - студенты " + instituteGroup.Key + ", " + instituteGroup.Value;
            }
        }
        return result;
    }
    static string Response13(ref DataBase dataBase)
    {
        string result = "Первокурсники с 3 двойками: ";
        foreach (Student student in dataBase.dataBase)
            if (student.Year == 1 && student.Doubles() == 3)
            {
                result += student.Sername + ", ";
                dataBase.Remove(student);
            }
        result += "\b\b\n\nДанные были удалены из базы";
        return result;
    }
    static string Response15(ref DataBase dataBase)
    {
        string result = "Отличники на первом и втором курсах: ";
        foreach (Student student in dataBase.dataBase)
            if ((student.Year == 1 || student.Year == 2) && student.Great())
                result += student.Sername + ", ";
        result += "\b\b\n\nСписки групп, упорядоченные по среднему баллу:\n";
        Dictionary<string, double> groupsMark = new Dictionary<string, double>();
        Dictionary<string, int> groups = new Dictionary<string, int>();
        foreach (Student student in dataBase.dataBase)
        {
            groupsMark[student.Group] = 0;
            groups[student.Group] = 0;
        }
        foreach (Student student in dataBase.dataBase)
            foreach (Subject subject in student.subject)
            {
                groupsMark[student.Group] += subject.Mark;
                ++groups[student.Group];
            }
        foreach (var key in groups.Keys)
            groupsMark[key] /= (double)groups[key];
        KeyValuePair<double, string>[] sorting = new KeyValuePair<double, string>[groupsMark.Keys.Count];
        int k = 0;
        foreach (var group in groupsMark)
            sorting[k++] = KeyValuePair.Create(group.Value, group.Key);
        Array.Sort(sorting);
        foreach (var group in sorting)
            result += group.Value + " - " + group.Key.ToString() + "\n";
        return result;
    }
    static string Response16(ref DataBase dataBase)
    {
        string result = "Двоечников нет в институтах: ";
        Dictionary<string, bool> institutes = new Dictionary<string, bool>();
        foreach (Student student in dataBase.dataBase)
            institutes[student.InstituteName] = true;
        foreach (Student student in dataBase.dataBase)
            if (institutes[student.InstituteName] && student.Doubles() > 0)
                institutes[student.InstituteName] = false;
        foreach (var institute in institutes)
            if (institute.Value) result += institute.Key + ", ";
        return result + "\b\b";
    }
    static string Response17(ref DataBase dataBase)
    {
        string result = "Не явившиеся на экзамен студенты: ";
        foreach (Student student in dataBase.dataBase)
            foreach (Subject subject in student.subject)
                if (subject.Mark == 0)
                {
                    result += student.Sername + ", ";
                    break;
                }
        foreach (Student student in dataBase.dataBase)
            if (student.Mean() < 3)
                dataBase.Remove(student);
        result += "\b\b\n\nТакже была удалена информация о студентах, чей средний бал ниже 3";
       return result;
    }
    static string Response18(ref DataBase dataBase)
    {
        Dictionary<string, string> institutes = new Dictionary<string, string>();
        foreach (Student student in dataBase.dataBase)
            if (student.Year == 1)
                institutes[student.InstituteName] = "";
        foreach (Student student in dataBase.dataBase)
            if (student.Year == 1 && institutes[student.InstituteName].IndexOf(student.Group) == -1)
                institutes[student.InstituteName] += student.Group + ", ";
        foreach (Student student in dataBase.dataBase)
            if (student.Year == 1 && student.Doubles() > 0)
                institutes[student.InstituteName].Replace(student.Group + ", ", "");
        KeyValuePair<string, string> max = new KeyValuePair<string, string>();
        foreach (var institute in institutes)
            if (institute.Value.IndexOf(',') > -1)
                max = institute;
        foreach (var institute in institutes)
            if (institute.Value.Split(",").ToArray().Length > max.Value.Split(",").ToArray().Length)
                max = institute;
        return "В " + max.Key + " наибольшее количество групп без двоек";
    }
    static string Response19(ref DataBase dataBase)
    {
        int[] years = new int[6];
        foreach (Student student in dataBase.dataBase)
            if (student.Great())
                ++years[student.Year];
        int max = 0;
        for (int i = 1; i < 6; ++i)
            if (years[i] > years[max])
                max = i;
        return "На " + max + "-м курсе больше всего отличников";
    }
    static string Response20(ref DataBase dataBase)
    {
        Dictionary<string, int> institutes = new Dictionary<string, int>();
        foreach (Student student in dataBase.dataBase)
            institutes[student.InstituteName] = 0;
        foreach (Student student in dataBase.dataBase)
            if (student.Year == 1 && student.Doubles() > 0)
                ++institutes[student.InstituteName];
        var max = institutes.First();
        foreach (var institute in institutes)
            if (max.Value < institute.Value)
                max = institute;
        return "В " + max.Key + " на первом курсе больше всего двоечников";
    }
    static string Response21(ref DataBase dataBase)
    {
        string result = "Отличников нет в группах: ";
        Dictionary<string, bool> groups = new Dictionary<string, bool>();
        foreach (Student student in dataBase.dataBase)
            groups[student.Group] = true;
        foreach (Student student in dataBase.dataBase)
            if (groups[student.Group] && student.Great())
                groups[student.Group] = false;

        foreach (var group in groups)
            if (group.Value) result += group.Key + ", ";
        return result + "\b\b";
    }
    static string Response22(ref DataBase dataBase)
    {
        string result = "Список двоечников:\n\n";
        foreach (Student student in dataBase.dataBase)
            if (student.Doubles() > 0)
                result += student.Sername + ", " + student.InstituteName + ", " + student.Group + ", " + student.Year.ToString() + "-й курс\n";
        return result;
    }
    static string Response23(ref DataBase dataBase)
    {
        string result = "Список отличников со второого курса:\n\n";
        foreach (Student student in dataBase.dataBase)
            if (student.Year == 2 && student.Great())
                result += student.Sername + ", " + student.InstituteName + ", " + student.Group + "\n";
        return result;
    }
    static void Response0(ref DataBase dataBase)
    {
        int req; Student student = new Student();
        while (true)
        {
            Console.WriteLine("Какие действия вы выберете:\n" +
                "1) ввод данных;\n" +
                "2) удаление данных;\n" +
                "3) редактирование данных.\n");
            req = int.Parse(Console.ReadLine());
            switch (req)
            {
                case 1:
                    Console.Write("Введите фамилию студента: ");
                    student.Sername = Console.ReadLine();
                    Console.Write("Введите институт: ");
                    student.InstituteName = Console.ReadLine();
                    Console.Write("Введите кафедру, к которой он прикреплен: ");
                    student.Fucalty = Console.ReadLine();
                    Console.Write("Введите группу: ");
                    student.Group = Console.ReadLine();
                    Console.Write("Введите курс обучения: ");
                    student.Year = int.Parse(Console.ReadLine());
                    Console.Write("Введите количество предметов, по которым сдавался экзамен: ");
                    int n = int.Parse(Console.ReadLine());
                    student.subject = new Subject[n];
                    for (int i = 0; i < n; ++i)
                    {
                        Console.Write("Введите" + i + "-й предмет: ");
                        student.subject[i].Name = Console.ReadLine();
                        Console.Write("Введите оценку: ");
                        student.subject[i].Mark = int.Parse(Console.ReadLine());
                    }
                    dataBase.Add(student);
                    break;
                case 2:
                    Console.Write("Введите фамилию удаляемого студента: ");
                    student.Sername = Console.ReadLine();
                    dataBase.Remove(student, true);
                    break;
                case 3:
                    Console.Write("Введите фамилию редактируемого студента: ");
                    student.Sername = Console.ReadLine();
                    dataBase.Edit(student);
                    break;
                default:
                    Console.WriteLine("Некорректная команда");
                    break;
            }
            Console.WriteLine("Хотите закончить редактирование? Y - да, любая другая клавиша - нет");
            if (Console.ReadKey().KeyChar == 'y' || Console.ReadKey().KeyChar == 'Y')
                return;
        }
    }
    static void Request(ref DataBase dataBase)
    {
        int req; string result;
        while (true)
        {
            Instructions();
            req = int.Parse(Console.ReadLine());
            switch (req)
            {
                case 1:
                    result = Response1(ref dataBase);
                    break;
                case 2:
                    result = Response2(ref dataBase);
                    break;
                case 3:
                    result = Response3(ref dataBase);
                    break;
                case 4:
                    result = Response4(ref dataBase);
                    break;
                case 5:
                    result = Response5(ref dataBase);
                    break;
                case 6:
                case 14:
                    result = Response614(ref dataBase);
                    break;
                case 7:
                    result = Response7(ref dataBase);
                    break;
                case 8:
                    result = Response8(ref dataBase);
                    break;
                case 9:
                    result = Response9(ref dataBase);
                    break;
                case 10:
                    result = Response10(ref dataBase);
                    break;
                case 11:
                    result = Response11(ref dataBase);
                    break;
                case 12:
                    result = Response12(ref dataBase);
                    break;
                case 13:
                    result = Response13(ref dataBase);
                    break;
                case 15:
                    result = Response15(ref dataBase);
                    break;
                case 16:
                    result = Response16(ref dataBase);
                    break;
                case 17:
                    result = Response17(ref dataBase);
                    break;
                case 18:
                    result = Response18(ref dataBase);
                    break;
                case 19:
                    result = Response19(ref dataBase);
                    break;
                case 20:
                    result = Response20(ref dataBase);
                    break;
                case 21:
                    result = Response21(ref dataBase);
                    break;
                case 22:
                    result = Response22(ref dataBase);
                    break;
                case 23:
                    result = Response23(ref dataBase);
                    break;
                case 0:
                    Response0(ref dataBase);
                    result = "В базу были внесены ручные изменения";
                    break;
                default:
                    result = "Запрос не определен";
                    break;
            }
            Console.Clear();
            Output(result);
            Console.WriteLine("Хотите выйти из программы? Q - да, любая другая клавиша - нет");
            if (Console.ReadKey().KeyChar == 'q' || Console.ReadKey().KeyChar == 'Q')
                return;
        }
    }
    static void Save(DataBase dataBase)
    {
        using (StreamWriter sw = new StreamWriter("base.txt", false))
        {
            foreach(Student student in dataBase.dataBase)
            {
                sw.WriteLine(student.Sername + " " + student.InstituteName + " " + student.Fucalty + " " +
                    student.Group + " " + student.Year.ToString() + " " + student.subject.Length);
                foreach (Subject subject in student.subject)
                    sw.WriteLine(subject.Name + subject.Mark);
                sw.WriteLine();
            }
        }
    }
    static void Main(string[] args)
    {
        DataBase dataBase = Input();
        Request(ref dataBase);
        Save(dataBase);
    }
}