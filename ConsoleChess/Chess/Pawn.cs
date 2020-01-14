using System;
using System.Collections.Generic;
using System.Text;
using ConsoleChess.GameBoard;

namespace ConsoleChess.Chess
{
    class Pawn : GamePiece
    {
        private ChessMatch Match { get; set; }

        public Pawn(Board board, Color color, ChessMatch match) : base(color, board)
        {
            Match = match;
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

                // SPECIAL MOVE: En Passent (for White pawns)
                if (Position.Row == 3) // White pawns can only perform En Passent from this line
                {
                    // Verify left position
                    Position leftPos = new Position(Position.Row, Position.Column - 1);
                    if(Board.IsValidPosition(leftPos) && IsThereEnemyInPosition(leftPos) && Board.GetPiece(leftPos) == Match.EnPassentTarget)
                    {
                        matrix[leftPos.Row - 1, leftPos.Column] = true;
                    }
                    Position rightPos = new Position(Position.Row, Position.Column + 1);
                    // Verify right position
                    if (Board.IsValidPosition(rightPos) && IsThereEnemyInPosition(rightPos) && Board.GetPiece(rightPos) == Match.EnPassentTarget)
                    {
                        matrix[rightPos.Row - 1, rightPos.Column] = true;
                    }
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

                // SPECIAL MOVE: En Passent (for Black pawns)
                if (Position.Row == 4) // Black pawns can only perform En Passent from this line
                {
                    // Verify left position
                    Position leftPos = new Position(Position.Row, Position.Column - 1);
                    if (Board.IsValidPosition(leftPos) && IsThereEnemyInPosition(leftPos) && Board.GetPiece(leftPos) == Match.EnPassentTarget)
                    {
                        matrix[leftPos.Row + 1, leftPos.Column] = true;
                    }
                    Position rightPos = new Position(Position.Row, Position.Column + 1);
                    // Verify right position
                    if (Board.IsValidPosition(rightPos) && IsThereEnemyInPosition(rightPos) && Board.GetPiece(rightPos) == Match.EnPassentTarget)
                    {
                        matrix[rightPos.Row + 1, rightPos.Column] = true;
                    }
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
