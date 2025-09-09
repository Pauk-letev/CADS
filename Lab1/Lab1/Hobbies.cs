class Hobby
{
    public string Name { get; set; }
    public int Hours { get; set; }

    public Hobby()
    {
        Name = "неизвестно";
        Hours = 0;
    }
    public Hobby(string name, int hours)
    {
        Name = name;
        Hours = hours;
    }

    public void HobbyPrint()
    {
        Console.WriteLine("Я занимаюсь " + Name + " {0} часа", Hours);
    }
}

class Sport : Hobby
{
    public string Kind {  get; set; }

    public Sport() : base()
    {
        Kind = "неизвестно";
    }
    public Sport(string name, int hours, string kind)
        : base(name, hours)
    {
        Kind = kind;
    }

    public void SportWrite()
    {
        Console.WriteLine("Я каждый день занимаюсь {0} по {1} часов в день. Это {2} вид спорта", Name, Hours, Kind);
    }
}

class Soccer : Sport
{
    public string Team { get; set; }

    public Soccer() : base()
    {
        Team = "неизвестно";
    }
    public Soccer(string name, int hours, string kind, string team)
        : base(name, hours, kind)
    {
        Team = team;
    }

    public void SoccerWrite()
    {
        Console.WriteLine("Я занимаюсь {0} по {1} часов в день. Это {2} футбол. Я состою в команде {3}", Name, Hours, Kind, Team);
    }
}

class Music : Hobby
{
    public string Instrument { get; set; }
    public string Style { get; set; }

    public Music() : base()
    {
        Instrument = "неизвестно";
        Style = "неизвестно";
    }
    public Music(string name, int hours, string instrument, string style)
        : base(name, hours)
    {
        Instrument = instrument;
        Style = style;
    }

    public void MusicWrite()
    {
        Console.WriteLine("Я занимаюсь {0} {1} часов в день. Я играю на {2} в стиле {3}", Name, Hours, Instrument, Style);
    }
}