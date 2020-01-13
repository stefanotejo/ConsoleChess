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

        public bool AreTherePossibleMoves()
        {
            bool[,] matrix = PossibleMoves();
            for(int i = 0; i < Board.Rows; i++)
            {
                for(int j = 0; j < Board.Columns; j++)
                {
                    if(matrix[i, j]) return true;
                }
            }
            return false;
        }

        public bool CanMoveToPosition(Position position)
        {
            return PossibleMoves()[position.Row, position.Column];
        }

        abstract public bool[,] PossibleMoves();
    }
}
