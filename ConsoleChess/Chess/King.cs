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

        public override string ToString()
        {
            return "K";
        }
    }
}
