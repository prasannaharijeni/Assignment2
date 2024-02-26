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

class Player
{
    public string Name { get; set; }
    public Position Position { get; set; }
    public int GemCount { get; set; }
    //Player Constructor
    public Player(string name, Position startPosition)
    {
        Name = name;
        Position = startPosition;
        GemCount = 0;
    }
    //direction reprensentation
    public void move(char direction)
    {
        var position = Position;
        var newX = position.X;
        var newY = position.Y;
        switch (direction)
        {
            case 'U': newX--; break;
            case 'D': newX++; break;
            case 'L': newY--; break;
            case 'R': newY++; break;
        }

        if (newX > 5) newX = 5;
        if (newY > 5) newY = 5;
        if (newX < 0) newX = 0;
        if (newY < 0) newY = 0;

        Position = new Position(newX, newY);

    }
}

class Cell
{
    public string Occupant { get; set; }
    public Cell(string occupant)
    {
        Occupant = occupant;
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
