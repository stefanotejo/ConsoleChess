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
                    Console.Clear();
                    Screen.PrintBoard(match.Board);

                    Console.Write("Input origin: ");
                    Position origin = Screen.ReadChessPosition().ToPosition();
                    Console.Write("Input destiny: ");
                    Position destiny = Screen.ReadChessPosition().ToPosition();

                    match.PerformMove(origin, destiny);
                }
            }
            catch(BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
