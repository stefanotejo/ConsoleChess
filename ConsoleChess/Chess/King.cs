using ConsoleChess.GameBoard;

namespace ConsoleChess.Chess
{
    class King : GamePiece
    {
        public ChessMatch Match { get; set; }

        public King(Board board, Color color, ChessMatch match) : base(color, board)
        {
            Match = match;
        }

        private bool CanMove(Position position)
        {
            GamePiece piece = Board.GetPiece(position);
            return piece == null || piece.Color != Color;
        }

        private bool TestRookForCastling(Position position)
        {
            GamePiece piece = Board.GetPiece(position);
            return piece != null && piece is Rook && piece.Color == Color && piece.NumberOfMovements == 0;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[Board.Rows, Board.Columns];
            Position position = new Position(0, 0);

            // North
            position.Redefine(Position.Row - 1, Position.Column);
            if(Board.IsValidPosition(position) && CanMove(position)) matrix[position.Row, position.Column] = true;
            // Northeast
            position.Redefine(Position.Row - 1, Position.Column + 1);
            if (Board.IsValidPosition(position) && CanMove(position)) matrix[position.Row, position.Column] = true;
            // East
            position.Redefine(Position.Row, Position.Column + 1);
            if (Board.IsValidPosition(position) && CanMove(position)) matrix[position.Row, position.Column] = true;
            // Southeast
            position.Redefine(Position.Row + 1, Position.Column + 1);
            if (Board.IsValidPosition(position) && CanMove(position)) matrix[position.Row, position.Column] = true;
            // South
            position.Redefine(Position.Row + 1, Position.Column);
            if (Board.IsValidPosition(position) && CanMove(position)) matrix[position.Row, position.Column] = true;
            // Southwest
            position.Redefine(Position.Row + 1, Position.Column - 1);
            if (Board.IsValidPosition(position) && CanMove(position)) matrix[position.Row, position.Column] = true;
            // West
            position.Redefine(Position.Row, Position.Column - 1);
            if (Board.IsValidPosition(position) && CanMove(position)) matrix[position.Row, position.Column] = true;
            // Northwest
            position.Redefine(Position.Row - 1, Position.Column - 1);
            if (Board.IsValidPosition(position) && CanMove(position)) matrix[position.Row, position.Column] = true;

            // SPECIAL MOVE: Castling
            if(NumberOfMovements == 0 && !Match.Check)
            {
                // Lesser Castling
                Position rightRookPos = new Position(Position.Row, Position.Column + 3);
                if (TestRookForCastling(rightRookPos))
                {
                    Position pos1 = new Position(Position.Row, Position.Column + 1);
                    Position pos2 = new Position(Position.Row, Position.Column + 2);
                    if(Board.GetPiece(pos1) == null && Board.GetPiece(pos2) == null)
                    {
                        matrix[pos2.Row, pos2.Column] = true;
                    }
                }
                // Greater Castling
                Position leftRookPos = new Position(Position.Row, Position.Column + 3);
                if (TestRookForCastling(leftRookPos))
                {
                    Position pos1 = new Position(Position.Row, Position.Column - 1);
                    Position pos2 = new Position(Position.Row, Position.Column - 2);
                    Position pos3 = new Position(Position.Row, Position.Column - 3);
                    if (Board.GetPiece(pos1) == null && Board.GetPiece(pos2) == null && Board.GetPiece(pos3) == null)
                    {
                        matrix[pos2.Row, pos2.Column] = true;
                    }
                }
            }

            return matrix;
        }

        public override string ToString()
        {
            return "K";
        }
    }
}
