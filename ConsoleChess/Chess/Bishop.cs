using System;
using System.Collections.Generic;
using System.Text;
using ConsoleChess.GameBoard;

namespace ConsoleChess.Chess
{
    class Bishop : GamePiece
    {
        public Bishop(Board board, Color color) : base(color, board)
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

            // Northwest
            position.Redefine(Position.Row - 1, Position.Column - 1);
            while (Board.IsValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if (Board.GetPiece(position) != null && Board.GetPiece(position).Color != Color)
                {
                    break;
                }
                position.Redefine(position.Row - 1, position.Column - 1);
            }
            // Northeast
            position.Redefine(Position.Row - 1, Position.Column + 1);
            while (Board.IsValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if (Board.GetPiece(position) != null && Board.GetPiece(position).Color != Color)
                {
                    break;
                }
                position.Redefine(position.Row - 1, position.Column + 1);
            }
            // Southeast
            position.Redefine(Position.Row + 1, Position.Column + 1);
            while (Board.IsValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if (Board.GetPiece(position) != null && Board.GetPiece(position).Color != Color)
                {
                    break;
                }
                position.Redefine(position.Row + 1, position.Column + 1);
            }
            // Southwest
            position.Redefine(Position.Row + 1, Position.Column - 1);
            while (Board.IsValidPosition(position) && CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if (Board.GetPiece(position) != null && Board.GetPiece(position).Color != Color)
                {
                    break;
                }
                position.Redefine(position.Row + 1, position.Column - 1);
            }

            return matrix;
        }

        public override string ToString()
        {
            return "B";
        }
    }
}
