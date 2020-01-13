using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleChess.GameBoard
{
    class GamePiece
    {
        public Position ActualPosition { get; set; }
        public Color Color { get; protected set; }
        public Board Board { get; protected set; }
        public int NumberOfMovements { get; protected set; }

        public GamePiece(Color color, Board board)
        {
            ActualPosition = null;
            Color = color;
            Board = board;
            NumberOfMovements = 0;
        }

        public void IncrementNumberOfMovements()
        {
            NumberOfMovements++;
        }
    }
}
