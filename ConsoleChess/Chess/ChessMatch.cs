using System;
using System.Collections.Generic;
using System.Text;
using ConsoleChess.GameBoard;

namespace ConsoleChess.Chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        private int Round { get; set; }
        private Color CurrentPlayer { get; set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Round = 1;
            CurrentPlayer = Color.White;

            SetBoard();
        }

        private void SetBoard()
        {
            Board.PlacePiece(new Rook(Board, Color.Black), new ChessPosition('c', 1).ToPosition());
            Board.PlacePiece(new Rook(Board, Color.White), new ChessPosition('a', 4).ToPosition());
            Board.PlacePiece(new King(Board, Color.Black), new ChessPosition('h', 6).ToPosition());
            Board.PlacePiece(new King(Board, Color.White), new ChessPosition('e', 5).ToPosition());
        }

        public void PerformMove(Position origin, Position destiny)
        {
            GamePiece piece = Board.RemovePiece(origin);
            piece.IncrementNumberOfMovements();
            GamePiece capturedPiece = Board.RemovePiece(destiny);
            Board.PlacePiece(piece, destiny);
        }
    }
}
