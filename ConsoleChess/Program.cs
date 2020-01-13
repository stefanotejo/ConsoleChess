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
                Board board = new Board(8, 8);

                board.PlacePiece(new Rook(board, Color.Black), new Position(0, 0));
                board.PlacePiece(new Rook(board, Color.White), new Position(2, 4));
                board.PlacePiece(new King(board, Color.Black), new Position(2, 6));
                board.PlacePiece(new King(board, Color.White), new Position(3, 5));

                Screen.PrintBoard(board);
            }
            catch(BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
