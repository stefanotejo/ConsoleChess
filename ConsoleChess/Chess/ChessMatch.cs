using System;
using System.Collections.Generic;
using System.Text;
using ConsoleChess.GameBoard;
using ConsoleChess.Chess;

namespace ConsoleChess.Chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public int Round { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; set; }
        private HashSet<GamePiece> Pieces { get; set; }
        private HashSet<GamePiece> CapturedPieces { get; set; }
        public bool Check { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Round = 1;
            CurrentPlayer = Color.White;
            Pieces = new HashSet<GamePiece>();
            CapturedPieces = new HashSet<GamePiece>();
            Check = false;

            SetBoard();
        }

        private void PlaceNewPiece(GamePiece piece, char column, int row)
        {
            Board.PlacePiece(piece, new ChessPosition(column, row).ToPosition());
            Pieces.Add(piece);
        }

        private void SetBoard()
        {
            PlaceNewPiece(new King(Board, Color.Black), 'a', 8);
            PlaceNewPiece(new Rook(Board, Color.Black), 'b', 8);

            PlaceNewPiece(new Rook(Board, Color.White), 'h', 7);
            PlaceNewPiece(new Rook(Board, Color.White), 'c', 1);
            PlaceNewPiece(new King(Board, Color.White), 'd', 1);
            // Black side
            /*
            PlaceNewPiece(new Rook(Board, Color.Black), 'c', 8);
            PlaceNewPiece(new King(Board, Color.Black), 'd', 8);
            PlaceNewPiece(new Rook(Board, Color.Black), 'e', 8);
            PlaceNewPiece(new Rook(Board, Color.Black), 'c', 7);
            PlaceNewPiece(new Rook(Board, Color.Black), 'd', 7);
            PlaceNewPiece(new Rook(Board, Color.Black), 'e', 7);
            // White side
            PlaceNewPiece(new Rook(Board, Color.White), 'c', 1);
            PlaceNewPiece(new King(Board, Color.White), 'd', 1);
            PlaceNewPiece(new Rook(Board, Color.White), 'e', 1);
            PlaceNewPiece(new Rook(Board, Color.White), 'c', 2);
            PlaceNewPiece(new Rook(Board, Color.White), 'd', 2);
            PlaceNewPiece(new Rook(Board, Color.White), 'e', 2);
            */
        }

        private Color Adversary(Color color)
        {
            if (color == Color.White) return Color.Black;
            return Color.White;
        }

        private GamePiece GetKing(Color color)
        {
            foreach(GamePiece piece in GetPiecesInGameByColor(color))
            {
                if (piece is King) return piece;
            }
            return null;
        }

        private void SwitchPlayer()
        {
            if (CurrentPlayer == Color.White) CurrentPlayer = Color.Black;
            else CurrentPlayer = Color.White;
        }

        private GamePiece PerformMove(Position origin, Position destiny)
        {
            GamePiece piece = Board.RemovePiece(origin);
            piece.IncrementNumberOfMovements();
            GamePiece capturedPiece = Board.RemovePiece(destiny);
            Board.PlacePiece(piece, destiny);

            if (capturedPiece != null) CapturedPieces.Add(capturedPiece);

            return capturedPiece;
        }

        private void UndoMove(Position origin, Position destiny, GamePiece capturedPiece)
        {
            GamePiece piece = Board.RemovePiece(destiny);
            piece.DecrementNumberOfMovements();

            if (capturedPiece != null)
            {
                Board.PlacePiece(capturedPiece, destiny);
                CapturedPieces.Remove(capturedPiece);
            }
            Board.PlacePiece(piece, origin);
        }

        private bool IsInCheck(Color color)
        {
            GamePiece king = GetKing(color);

            if (king == null)
            {
                throw new BoardException($"ERROR: There is no {color} king");
            }

            foreach(GamePiece piece in GetPiecesInGameByColor(Adversary(color)))
            {
                bool[,] matrix = piece.PossibleMoves();

                if (matrix[king.Position.Row, king.Position.Column])
                {
                    // This means the adversary piece can reach the king
                    return true;
                }
            }
            return false;
        }

        private bool IsInCheckmate(Color color)
        {
            if (!IsInCheck(color)) return false;

            foreach(GamePiece piece in GetPiecesInGameByColor(color))
            {
                bool[,] matrix = piece.PossibleMoves();

                for(int i = 0; i < Board.Rows; i++)
                {
                    for(int j = 0; j < Board.Columns; j++)
                    {
                        if(matrix[i, j])
                        {
                            Position origin = piece.Position;
                            Position destiny = new Position(i, j);
                            GamePiece capturedPiece = PerformMove(origin, destiny);
                            bool checkTest = IsInCheck(color);
                            UndoMove(origin, destiny, capturedPiece);
                            if (!checkTest)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public HashSet<GamePiece> GetCapturedPiecesByColor(Color color)
        {
            HashSet<GamePiece> returnHash = new HashSet<GamePiece>();

            foreach(GamePiece piece in CapturedPieces)
            {
                if (piece.Color == color) returnHash.Add(piece);
            }
            return returnHash;
        }

        public HashSet<GamePiece> GetPiecesInGameByColor(Color color)
        {
            HashSet<GamePiece> returnHash = new HashSet<GamePiece>();

            foreach (GamePiece piece in Pieces)
            {
                if (piece.Color == color) returnHash.Add(piece);
            }
            returnHash.ExceptWith(GetCapturedPiecesByColor(color));
            return returnHash;
        }

        public void Play(Position origin, Position destiny)
        {
            GamePiece capturedPiece = PerformMove(origin, destiny);

            if (IsInCheck(CurrentPlayer))
            {
                UndoMove(origin, destiny, capturedPiece);
                throw new BoardException("ERROR: A player may not put his own king in check or remain in check");
            }

            if (IsInCheck(Adversary(CurrentPlayer))) {
                Check = true;
            }
            else
            {
                Check = false;
            }

            if (IsInCheckmate(Adversary(CurrentPlayer)))
            {
                Finished = true;
            }
            else
            {
                Round++;
                SwitchPlayer();
            }
        }

        public void ValidateOriginPosition(Position origin)
        {
            if (Board.GetPiece(origin) == null)
            {
                throw new BoardException("ERROR: There is no piece in chosen origin position");
            }
            if (CurrentPlayer != Board.GetPiece(origin).Color)
            {
                throw new BoardException("ERROR: Piece in chosen origin position belongs to the other player");
            }
            if (!Board.GetPiece(origin).AreTherePossibleMoves())
            {
                throw new BoardException("ERROR: There are no possible moves for the piece in chosen origin position");
            }
        }

        public void ValidateDestinyPosition(Position origin, Position destiny)
        {
            if (!Board.GetPiece(origin).CanMoveToPosition(destiny))
            {
                throw new BoardException("ERROR: Destiny position is invalid");
            }
        }
    }
}
