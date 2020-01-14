using ConsoleChess.GameBoard;

namespace ConsoleChess.Chess
{
    class Knight : GamePiece
    {
        public Knight(Board board, Color color) : base(color, board)
        {
        }

        private bool CanMove(Position position)
        {
            GamePiece piece = Board.GetPiece(position);
            return piece == null || piece.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[Board.Rows, Board.Columns];
            Position position = new Position(0, 0);

            // Northwest A
            position.Redefine(Position.Row - 1, Position.Column - 2);
            if (Board.IsValidPosition(position) && CanMove(position)) matrix[position.Row, position.Column] = true;
            // Northwest B
            position.Redefine(Position.Row - 2, Position.Column - 1);
            if (Board.IsValidPosition(position) && CanMove(position)) matrix[position.Row, position.Column] = true;
            // Northeast A
            position.Redefine(Position.Row - 2, Position.Column + 1);
            if (Board.IsValidPosition(position) && CanMove(position)) matrix[position.Row, position.Column] = true;
            // Northeast B
            position.Redefine(Position.Row - 1, Position.Column + 2);
            if (Board.IsValidPosition(position) && CanMove(position)) matrix[position.Row, position.Column] = true;
            // Southeast A
            position.Redefine(Position.Row + 1, Position.Column + 2);
            if (Board.IsValidPosition(position) && CanMove(position)) matrix[position.Row, position.Column] = true;
            // Southeast B
            position.Redefine(Position.Row + 2, Position.Column + 1);
            if (Board.IsValidPosition(position) && CanMove(position)) matrix[position.Row, position.Column] = true;
            // Southwest A
            position.Redefine(Position.Row + 2, Position.Column - 1);
            if (Board.IsValidPosition(position) && CanMove(position)) matrix[position.Row, position.Column] = true;
            // Southwest B
            position.Redefine(Position.Row + 1, Position.Column - 2);
            if (Board.IsValidPosition(position) && CanMove(position)) matrix[position.Row, position.Column] = true;

            return matrix;
        }

        public override string ToString()
        {
            return "N";
        }
    }
}
