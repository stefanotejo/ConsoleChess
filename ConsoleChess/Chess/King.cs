using System;
using System.Collections.Generic;
using System.Text;
using ConsoleChess.GameBoard;

namespace ConsoleChess.Chess
{
    class King : GamePiece
    {
        public King(Board board, Color color) : base(color, board)
        {
        }

        private bool CanMove(Position position)
        {
            GamePiece piece = Board.GetPiece(position);
            return piece == null || piece.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[Board.Rows, Board.Columns];
            Position position = new Position(0, 0);

            // North
            position.Redefine(Position.Row - 1, Position.Column);
            if(Board.IsValidPosition(position) && CanMove(position)) matrix[position.Row, position.Column] = true;
            // Northeast
            position.Redefine(Position.Row - 1, Position.Column + 1);
            if (Board.IsValidPosition(position) && CanMove(position)) matrix[position.Row, position.Column] = true;
            // East
            position.Redefine(Position.Row, Position.Column + 1);
            if (Board.IsValidPosition(position) && CanMove(position)) matrix[position.Row, position.Column] = true;
            // Southeast
            position.Redefine(Position.Row + 1, Position.Column + 1);
            if (Board.IsValidPosition(position) && CanMove(position)) matrix[position.Row, position.Column] = true;
            // South
            position.Redefine(Position.Row + 1, Position.Column);
            if (Board.IsValidPosition(position) && CanMove(position)) matrix[position.Row, position.Column] = true;
            // Southwest
            position.Redefine(Position.Row + 1, Position.Column - 1);
            if (Board.IsValidPosition(position) && CanMove(position)) matrix[position.Row, position.Column] = true;
            // West
            position.Redefine(Position.Row, Position.Column - 1);
            if (Board.IsValidPosition(position) && CanMove(position)) matrix[position.Row, position.Column] = true;
            // Northwest
            position.Redefine(Position.Row - 1, Position.Column - 1);
            if (Board.IsValidPosition(position) && CanMove(position)) matrix[position.Row, position.Column] = true;

            return matrix;
        }

        public override string ToString()
        {
            return "K";
        }
    }
}
