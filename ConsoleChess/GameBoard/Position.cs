namespace ConsoleChess.GameBoard
{
    class Position
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public void Redefine(int row, int column) // Auxiliary method to be used in the PossibleMoves() methods
        {
            Row = row;
            Column = column;
        }

        public override string ToString()
        {
            return $"{Row}, {Column}";
        }
    }
}
