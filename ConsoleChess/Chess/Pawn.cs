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

        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[Board.Rows, Board.Columns];

            return matrix;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
