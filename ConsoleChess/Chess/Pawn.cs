using System;
using System.Collections.Generic;
using System.Text;
using ConsoleChess.GameBoard;

namespace ConsoleChess.Chess
{
    class Pawn : GamePiece
    {
        public Pawn(Board board, Color color) : base(color, board)
        {
        }

        private bool CanMove(Position position)
        {
            GamePiece piece = Board.GetPiece(position);
            return piece == null || piece.Color != Color;
        }

        private bool IsThereEnemyInPosition(Position position)
        {
            GamePiece piece = Board.GetPiece(position);
            return piece != null && piece.Color != Color;
        }

        private bool IsPositionFree(Position position)
        {
            return Board.GetPiece(position) == null;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[Board.Rows, Board.Columns];
            Position position = new Position(0, 0);

            if (Color == Color.White)
            {
                // One square north
                position.Redefine(Position.Row - 1, Position.Column);
                if (Board.IsValidPosition(position) && IsPositionFree(position))
                {
                    matrix[position.Row, position.Column] = true;
                }
                // Two squares north
                position.Redefine(Position.Row - 2, Position.Column);
                if (Board.IsValidPosition(position) && IsPositionFree(position) && NumberOfMovements == 0)
                {
                    matrix[position.Row, position.Column] = true;
                }
                // Northwest
                position.Redefine(Position.Row - 1, Position.Column - 1);
                if (Board.IsValidPosition(position) && IsThereEnemyInPosition(position))
                {
                    matrix[position.Row, position.Column] = true;
                }
                // Northeast
                position.Redefine(Position.Row - 1, Position.Column + 1);
                if (Board.IsValidPosition(position) && IsThereEnemyInPosition(position))
                {
                    matrix[position.Row, position.Column] = true;
                }
            }
            else
            {
                // One square south
                position.Redefine(Position.Row + 1, Position.Column);
                if (Board.IsValidPosition(position) && IsPositionFree(position))
                {
                    matrix[position.Row, position.Column] = true;
                }
                // Two squares south
                position.Redefine(Position.Row + 2, Position.Column);
                if (Board.IsValidPosition(position) && IsPositionFree(position) && NumberOfMovements == 0)
                {
                    matrix[position.Row, position.Column] = true;
                }
                // Southwest
                position.Redefine(Position.Row + 1, Position.Column - 1);
                if (Board.IsValidPosition(position) && IsThereEnemyInPosition(position))
                {
                    matrix[position.Row, position.Column] = true;
                }
                // Southeast
                position.Redefine(Position.Row + 1, Position.Column + 1);
                if (Board.IsValidPosition(position) && IsThereEnemyInPosition(position))
                {
                    matrix[position.Row, position.Column] = true;
                }
            }

            return matrix;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
