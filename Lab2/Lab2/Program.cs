class Program
{
    static void Main(string[] args)
    {
        Sport sport = new Sport("Теннис", 2, "зимний");
        sport.HobbyPrint();
        sport.MoreTime();
        sport.LessTime();
        Soccer soccer = new Soccer("на траве", 5, "лето", "хз");
        soccer.HobbyPrint();
        soccer.MoreTime();
        soccer.LessTime();
        Music music = new Music("Какая-то там", 7, "микроволновка", "рок");
        music.HobbyPrint();
        music.MoreTime();
        music.LessTime();
    }
}