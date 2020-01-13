using System;
using System.Collections.Generic;
using System.Text;
using ConsoleChess.GameBoard;

namespace ConsoleChess.Chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public int Round { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Round = 1;
            CurrentPlayer = Color.White;

            SetBoard();
        }

        private void SetBoard()
        {
            Board.PlacePiece(new Rook(Board, Color.Black), new ChessPosition('c', 8).ToPosition());
            Board.PlacePiece(new King(Board, Color.Black), new ChessPosition('d', 8).ToPosition());
            Board.PlacePiece(new Rook(Board, Color.Black), new ChessPosition('e', 8).ToPosition());
            Board.PlacePiece(new Rook(Board, Color.Black), new ChessPosition('c', 7).ToPosition());
            Board.PlacePiece(new Rook(Board, Color.Black), new ChessPosition('d', 7).ToPosition());
            Board.PlacePiece(new Rook(Board, Color.Black), new ChessPosition('e', 7).ToPosition());

            Board.PlacePiece(new Rook(Board, Color.White), new ChessPosition('c', 1).ToPosition());
            Board.PlacePiece(new King(Board, Color.White), new ChessPosition('d', 1).ToPosition());
            Board.PlacePiece(new Rook(Board, Color.White), new ChessPosition('e', 1).ToPosition());
            Board.PlacePiece(new Rook(Board, Color.White), new ChessPosition('c', 2).ToPosition());
            Board.PlacePiece(new Rook(Board, Color.White), new ChessPosition('d', 2).ToPosition());
            Board.PlacePiece(new Rook(Board, Color.White), new ChessPosition('e', 2).ToPosition());
        }

        private void SwitchPlayer()
        {
            if (CurrentPlayer == Color.White) CurrentPlayer = Color.Black;
            else CurrentPlayer = Color.White;
        }
        private void PerformMove(Position origin, Position destiny)
        {
            GamePiece piece = Board.RemovePiece(origin);
            piece.IncrementNumberOfMovements();
            GamePiece capturedPiece = Board.RemovePiece(destiny);
            Board.PlacePiece(piece, destiny);
        }

        public void Play(Position origin, Position destiny)
        {
            PerformMove(origin, destiny);
            Round++;
            SwitchPlayer();
        }

        public void ValidateOriginPosition(Position origin)
        {
            if (Board.GetPiece(origin) == null)
            {
                throw new BoardException("ERROR: There is no piece in chosen origin position");
            }
            if(CurrentPlayer != Board.GetPiece(origin).Color)
            {
                throw new BoardException("ERROR: Piece in chosen origin position belongs to the other player");
            }
            if(!Board.GetPiece(origin).AreTherePossibleMoves())
            {
                throw new BoardException("ERROR: There are no possible moves for the piece in chosen origin position");
            }
        }

        public void ValidateDestinyPosition(Position origin, Position destiny)
        {
            if (!Board.GetPiece(origin).CanMoveToPosition(destiny))
            {
                throw new BoardException("ERROR: Destiny position is invalid");
            }
        }
    }
}
