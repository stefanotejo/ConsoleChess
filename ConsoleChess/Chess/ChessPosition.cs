using ConsoleChess.GameBoard;

namespace ConsoleChess.Chess
{
    class ChessPosition
    {
        private char Column { get; set; }
        private int Row { get; set; }

        public ChessPosition(char column, int row)
        {
            Column = column;
            Row = row;
        }

        public Position ToPosition()
        {
            return new Position(8 - Row, Column - 'a');
        }

        public override string ToString()
        {
            return string.Empty + Column + Row;
        }
    }
}
