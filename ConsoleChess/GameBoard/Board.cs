using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleChess.GameBoard
{
    class Board
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        private GamePiece[,] Pieces { get; set; }

        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Pieces = new GamePiece[rows, columns];
        }

        public bool IsValidPosition(Position position)
        {
            if (position.Row < 0 || position.Row >= Rows || position.Column < 0 || position.Column >= Columns)
            {
                return false;
            }
            return true;
        }

        public void ValidatePosition(Position position)
        {
            if (!IsValidPosition(position))
            {
                throw new BoardException("ERROR: Invalid position");
            }
        }

        public bool IsThereGamePiece(Position position)
        {
            ValidatePosition(position); // Throws exception if position not valid, ending execution of IsThereGamePiece()
            return GetPiece(position) != null;
        }

        public GamePiece GetPiece(int row, int column)
        {
            return Pieces[row, column];
        }
        public GamePiece GetPiece(Position position)
        {
            return Pieces[position.Row, position.Column];
        }

        public void PlacePiece(GamePiece piece, Position position)
        {
            if (IsThereGamePiece(position))
            {
                throw new BoardException("ERROR: There is already a game piece at this position");
            }
            Pieces[position.Row, position.Column] = piece;
            piece.Position = position;
        }

        public GamePiece RemovePiece(Position position)
        {
            if (GetPiece(position) == null)
            {
                return null;
            }
            GamePiece aux = GetPiece(position);
            aux.Position = null;
            Pieces[position.Row, position.Column] = null;
            return aux;
        }
    }
}
