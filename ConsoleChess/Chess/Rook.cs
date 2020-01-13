using System;
using System.Collections.Generic;
using System.Text;
using ConsoleChess.GameBoard;

namespace ConsoleChess.Chess
{
    class Rook : GamePiece
    {
        public Rook(Board board, Color color) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
