using System.Reflection.Metadata.Ecma335;

abstract class Hobby
{
    public string Name { get; set; }
    public int Hours { get; set; }

    abstract public void HobbyPrint();
    abstract public void MoreTime();
    abstract public void LessTime();
}

class Sport : Hobby
{
    public string Kind { get; set; }

    public Sport(string name, int hours, string kind)
    {
        Name = name;
        Hours = hours;
        Kind = kind;
    }

    public override void HobbyPrint()
    {
        Console.WriteLine("Я каждый день занимаюсь {0} по {1} часов в день. Это {2} вид спорта", Name, Hours, Kind);
    }
    public override void MoreTime()
    {
        Console.WriteLine("Занимаясь {0}, я потрачу {1} часов в неделю, {2} часов в месяц, {3} часов в год" +
            " и, быть может, через {4} лет я стану профессионалом в {0}", Name, 7 * Hours, 30 * Hours, 365 * Hours, (double) 30000 / 365 / Hours);
    }
    public override void LessTime()
    {
        Console.Write("Если бы я не занимался {0}, то я мог бы ", Name);
        if(Hours < 5)
        {
            Console.WriteLine("получить самоэкзамен по КАСД");
        }
        else
        {
            Console.WriteLine("не отчислиться с ФКТиПМ");
        }
    }
}

class Soccer : Sport
{
    public string Team { get; set; }

    public Soccer(string name, int hours, string kind, string team)
        : base(name, hours, kind)
    {
        Team = team;
    }

    public new void HobbyPrint()
    {
        Console.WriteLine("Я занимаюсь {0} по {1} часов в день. Это {2} футбол. Я состою в команде {3}", Name, Hours, Kind, Team);
    }
    public override void MoreTime()
    {
        Console.WriteLine("Занимаясь {0} футболом {1} часов, я буду тратить {2} часов в месяц. Таким образом, команда {3} " +
            "попадет в полуфинал через {4} лет", Name, Hours, 30 * Hours, Team, (double) 3E6 / 365 / Hours);
    }
    public override void LessTime()
    {
        Console.WriteLine("Та все, на фундмах мне не выжить");
    }

}

class Music : Hobby
{
    public string Instrument { get; set; }
    public string Style { get; set; }

    public Music(string name, int hours, string instrument, string style)
    {
        Name = name;
        Hours = hours;
        Instrument = instrument;
        Style = style;
    }

    public override void HobbyPrint()
    {
        Console.WriteLine("Я занимаюсь {0} {1} часов в день. Я играю на {2} в стиле {3}", Name, Hours, Instrument, Style);
    }
    public override void MoreTime()
    {
        Console.WriteLine("Занимаясь {0}, я потрачу {1} часов в неделю, {2} часов в месяц, {3} часов в год" +
                        " и, быть может, через {4} лет я стану настоящим {5}-музыкантом", Name, 7 * Hours, 30 * Hours, 365 * Hours, (double) 80000 / 365 / Hours, Style);
    }
    public override void LessTime()
    {
        if(Hours < 10)
        {
            Console.WriteLine("Не, ну, в принципе, можно попробовать кодить на " + Instrument);
        }
        else
        {
            Console.WriteLine("Так, я в полуфинале, что дальше?");
        }
    }
}