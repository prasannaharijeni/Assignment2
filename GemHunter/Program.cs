using System;
using System.Linq;

class Position
{
    public int X { get; set; }
    public int Y { get; set; }
    //Position constructor
    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
}

class Program
{
    //This method will start the application
    static void Main()
    {
        Game game = new Game();
        game.Start();
    }
}
