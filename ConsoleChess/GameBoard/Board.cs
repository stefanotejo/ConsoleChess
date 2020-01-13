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
    }
}
