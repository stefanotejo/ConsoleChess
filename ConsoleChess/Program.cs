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

                    Console.WriteLine();
                    Console.Write("Input origin: ");
                    Position origin = Screen.ReadChessPosition().ToPosition();

                    bool[,] possibleMoves = match.Board.GetPiece(origin).PossibleMoves();

                    Console.Clear();
                    Screen.PrintBoard(match.Board, possibleMoves);

                    Console.WriteLine();
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
