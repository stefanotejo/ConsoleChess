using System;
using ConsoleChess.GameBoard;
using ConsoleChess.Chess;

namespace ConsoleChess
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch match = new ChessMatch();

                while (!match.Finished)
                {
                    try
                    {
                        Console.Clear();
                        Screen.PrintBoard(match.Board);
                        Console.WriteLine();
                        Console.WriteLine($"Round: {match.Round}");
                        Console.WriteLine($"Waiting for {match.CurrentPlayer} player's move...");

                        Console.WriteLine();
                        Console.Write("Input origin: ");
                        Position origin = Screen.ReadChessPosition().ToPosition();
                        match.ValidateOriginPosition(origin);

                        bool[,] possibleMoves = match.Board.GetPiece(origin).PossibleMoves();

                        Console.Clear();
                        Screen.PrintBoard(match.Board, possibleMoves);
                        Console.WriteLine();
                        Console.WriteLine($"Round: {match.Round}");
                        Console.WriteLine($"Waiting for {match.CurrentPlayer} player's move...");

                        Console.WriteLine();
                        Console.Write("Input destiny: ");
                        Position destiny = Screen.ReadChessPosition().ToPosition();
                        match.ValidateDestinyPosition(origin, destiny);

                        match.Play(origin, destiny);
                    }
                    catch(BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.Write("Press 'enter' to continue...");
                        Console.ReadLine(); // Wait for user to press enter
                        Console.Clear();
                    }
                }
            }
            catch(BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
