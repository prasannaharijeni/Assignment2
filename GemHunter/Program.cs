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

class Board
{
    public Cell[,] Grid { get; set; } = new Cell[6, 6];

    public Board()
    {
        // Initialize the board with empty spaces
        for (int i = 0; i < 6; i++)
            for (int j = 0; j < 6; j++)
                Grid[i, j] = new Cell("-");

        // Place players
        Grid[0, 0].Occupant = "P1";
        Grid[5, 5].Occupant = "P2";

        // Place gems and Obstacles
        Random random = new Random();
        for (int i = 0; i < 5; i++)
        {
            int gemX, gemY, obsX, obsY;
            do
            {
                gemX = random.Next(6);
                gemY = random.Next(6);
            } while (Grid[gemX, gemY].Occupant != "-");
            Grid[gemX, gemY].Occupant = "G";

            do
            {
                obsX = random.Next(6);
                obsY = random.Next(6);
            } while (Grid[obsX, obsY].Occupant != "-");
            Grid[obsX, obsY].Occupant = "O";
        }
    }
    //method to display Current position of theplayers in the  board
    public void display()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
                Console.Write(Grid[i, j].Occupant + " ");
            Console.WriteLine();
        }
    }
    //Checks the players move is valid or not
    public bool isValidMove(Player player, char direction)
    {
        int newX = player.Position.X;
        int newY = player.Position.Y;

        switch (direction)
        {
            case 'U': newX--; break;
            case 'D': newX++; break;
            case 'L': newY--; break;
            case 'R': newY++; break;
            default: return false;
        }

        if (newX > 5) newX = 5;
        if (newY > 5) newY = 5;
        if (newX < 0) newX = 0;
        if (newY < 0) newY = 0;


        var isValidMove = Grid[newX, newY].Occupant != "O";

        if (isValidMove)
        {
            Grid[player.Position.X, player.Position.Y].Occupant = "-";
        }

        return isValidMove;
    }
    //Checks if the player's new position contains a gem and updates the player's GemCount.
    public void CollectGem(Player player)
    {
        if (Grid[player.Position.X, player.Position.Y].Occupant == "G")
        {
            player.GemCount++;
        }
        Grid[player.Position.X, player.Position.Y].Occupant = player.Name;
    }
}

class Game
{
    public Board Board { get; set; }
    public Player Player1 { get; set; }
    public Player Player2 { get; set; }
    public Player CurrentTurn { get; set; }
    public int TotalTurns { get; set; } = 0;

    public Game()
    {
        Board = new Board();
        Player1 = new Player("P1", new Position(0, 0));
        Player2 = new Player("P2", new Position(5, 5));
        CurrentTurn = Player1;
    }
    //game begins, start game
    public void Start()
    {
        Console.WriteLine("Welcome to Gem Hunters!");
        while (!IsGameOver())
        {
            Board.display();
            char direction;
            do
            {
                Console.WriteLine($"{CurrentTurn.Name}'s turn. Please enter move (U, D, L, R):");
                TotalTurns++;

                direction = Char.ToUpper(Console.ReadKey().KeyChar);
                var validMove = Board.isValidMove(CurrentTurn, direction);
                if (validMove)
                {
                    CurrentTurn.move(direction);
                    Board.CollectGem(CurrentTurn);
                }
                Console.WriteLine();
                Board.display();
                SwitchTurn();
            } while (!IsGameOver());
        }
        Board.display();
        AnnounceWinner();
    }
    //Switches between player1 and Player2
    public void SwitchTurn()
    {
        CurrentTurn = (CurrentTurn == Player1) ? Player2 : Player1;
    }
    //Checks if the game has reached its end condition.
    public bool IsGameOver()
    {
        return TotalTurns == 15;
    }
    //Announce the game winner based on the GemCount of both player
    public void AnnounceWinner()
    {
        Board.display();
        Console.WriteLine("Game Over!");
        Console.WriteLine($"Player1's Total Gems Count: {Player1.GemCount}");
        Console.WriteLine($"Player2's Total Gems Count: {Player2.GemCount}");

        if (Player1.GemCount > Player2.GemCount)
        {
            Console.WriteLine("WooHoo!!! Player 1 wins!");
        }
        else if (Player1.GemCount < Player2.GemCount)
        {
            Console.WriteLine("WooHoo!!! Player 2 wins!");
        }
        else
        {
            Console.WriteLine("Great Match!! It's a tie!! Both Players well played!!!");
        }
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
