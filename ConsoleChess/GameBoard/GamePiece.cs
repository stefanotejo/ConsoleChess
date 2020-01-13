using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleChess.GameBoard
{
    abstract class GamePiece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public Board Board { get; protected set; }
        public int NumberOfMovements { get; protected set; }

        public GamePiece(Color color, Board board)
        {
            Position = null;
            Color = color;
            Board = board;
            NumberOfMovements = 0;
        }

        public void IncrementNumberOfMovements()
        {
            NumberOfMovements++;
        }

        abstract public bool[,] PossibleMoves();
    }
}
