﻿using System.Collections.Generic;
using ConsoleChess.GameBoard;

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
        public GamePiece EnPassentTarget { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Round = 1;
            CurrentPlayer = Color.White;
            Pieces = new HashSet<GamePiece>();
            CapturedPieces = new HashSet<GamePiece>();
            Check = false;
            EnPassentTarget = null;

            SetBoard();
        }

        private void PlaceNewPiece(GamePiece piece, char column, int row)
        {
            Board.PlacePiece(piece, new ChessPosition(column, row).ToPosition());
            Pieces.Add(piece);
        }

        private void SetBoard()
        {

            // White side
            PlaceNewPiece(new Rook(Board, Color.White), 'a', 1);
            PlaceNewPiece(new Knight(Board, Color.White), 'b', 1);
            PlaceNewPiece(new Bishop(Board, Color.White), 'c', 1);
            PlaceNewPiece(new Queen(Board, Color.White), 'd', 1);
            PlaceNewPiece(new King(Board, Color.White, this), 'e', 1);
            PlaceNewPiece(new Bishop(Board, Color.White), 'f', 1);
            PlaceNewPiece(new Knight(Board, Color.White), 'g', 1);
            PlaceNewPiece(new Rook(Board, Color.White), 'h', 1);
            PlaceNewPiece(new Pawn(Board, Color.White, this), 'a', 2);
            PlaceNewPiece(new Pawn(Board, Color.White, this), 'b', 2);
            PlaceNewPiece(new Pawn(Board, Color.White, this), 'c', 2);
            PlaceNewPiece(new Pawn(Board, Color.White, this), 'd', 2);
            PlaceNewPiece(new Pawn(Board, Color.White, this), 'e', 2);
            PlaceNewPiece(new Pawn(Board, Color.White, this), 'f', 2);
            PlaceNewPiece(new Pawn(Board, Color.White, this), 'g', 2);
            PlaceNewPiece(new Pawn(Board, Color.White, this), 'h', 2);

            // Black side
            PlaceNewPiece(new Rook(Board, Color.Black), 'a', 8);
            PlaceNewPiece(new Knight(Board, Color.Black), 'b', 8);
            PlaceNewPiece(new Bishop(Board, Color.Black), 'c', 8);
            PlaceNewPiece(new Queen(Board, Color.Black), 'd', 8);
            PlaceNewPiece(new King(Board, Color.Black, this), 'e', 8);
            PlaceNewPiece(new Bishop(Board, Color.Black), 'f', 8);
            PlaceNewPiece(new Knight(Board, Color.Black), 'g', 8);
            PlaceNewPiece(new Rook(Board, Color.Black), 'h', 8);
            PlaceNewPiece(new Pawn(Board, Color.Black, this), 'a', 7);
            PlaceNewPiece(new Pawn(Board, Color.Black, this), 'b', 7);
            PlaceNewPiece(new Pawn(Board, Color.Black, this), 'c', 7);
            PlaceNewPiece(new Pawn(Board, Color.Black, this), 'd', 7);
            PlaceNewPiece(new Pawn(Board, Color.Black, this), 'e', 7);
            PlaceNewPiece(new Pawn(Board, Color.Black, this), 'f', 7);
            PlaceNewPiece(new Pawn(Board, Color.Black, this), 'g', 7);
            PlaceNewPiece(new Pawn(Board, Color.Black, this), 'h', 7);
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

            // SPECIAL MOVES:

            // Lesser Castling
            if (piece is King && destiny.Column == origin.Column + 2)
            {
                Position rookOrigin = new Position(origin.Row, origin.Column + 3);
                Position rookDestiny = new Position(origin.Row, origin.Column + 1);
                GamePiece rook = Board.RemovePiece(rookOrigin);
                rook.IncrementNumberOfMovements();
                Board.PlacePiece(rook, rookDestiny);
            }

            // Greater Castling
            if (piece is King && destiny.Column == origin.Column - 2)
            {
                Position rookOrigin = new Position(origin.Row, origin.Column - 4);
                Position rookDestiny = new Position(origin.Row, origin.Column - 1);
                GamePiece rook = Board.RemovePiece(rookOrigin);
                rook.IncrementNumberOfMovements();
                Board.PlacePiece(rook, rookDestiny);
            }

            // En Passent
            if (piece is Pawn)
            {
                if (destiny.Column != origin.Column && capturedPiece == null)
                {
                    Position targetPawnPosition;
                    if(piece.Color == Color.White)
                    {
                        targetPawnPosition = new Position(destiny.Row + 1, destiny.Column);
                    }
                    else
                    {
                        targetPawnPosition = new Position(destiny.Row - 1, destiny.Column);
                    }
                    capturedPiece = Board.RemovePiece(targetPawnPosition);
                    CapturedPieces.Add(capturedPiece);
                }
            }

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

            // SPECIAL MOVES:

            // Lesser Castling
            if (piece is King && destiny.Column == origin.Column + 2)
            {
                Position rookOrigin = new Position(origin.Row, origin.Column + 3);
                Position rookDestiny = new Position(origin.Row, origin.Column + 1);
                GamePiece rook = Board.RemovePiece(rookDestiny);
                rook.DecrementNumberOfMovements();
                Board.PlacePiece(rook, rookOrigin);
            }
            // Lesser Castling
            if (piece is King && destiny.Column == origin.Column - 2)
            {
                Position rookOrigin = new Position(origin.Row, origin.Column - 4);
                Position rookDestiny = new Position(origin.Row, origin.Column - 1);
                GamePiece rook = Board.RemovePiece(rookDestiny);
                rook.DecrementNumberOfMovements();
                Board.PlacePiece(rook, rookOrigin);
            }
            // En Passent
            if (piece is Pawn)
            {
                if (destiny.Column != origin.Column && capturedPiece == EnPassentTarget)
                {
                    GamePiece targetPawn = Board.RemovePiece(destiny);
                    Position targetPawnPosition;
                    if (piece.Color == Color.White)
                    {
                        targetPawnPosition = new Position(3, destiny.Column);
                    }
                    else
                    {
                        targetPawnPosition = new Position(4, destiny.Column);
                    }
                    Board.PlacePiece(targetPawn, targetPawnPosition);
                }
            }
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
            GamePiece piece = Board.GetPiece(destiny);

            // SPECIAL MOVE: Promotion
            if (piece is Pawn)
            {
                if ((piece.Color == Color.White && destiny.Row == 0) || (piece.Color == Color.Black && destiny.Row == 7))
                {
                    piece = Board.RemovePiece(destiny);
                    Pieces.Remove(piece);
                    GamePiece queen = new Queen(Board, piece.Color);
                    Board.PlacePiece(queen, destiny);
                    Pieces.Add(queen);
                }

            }

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

            // SPECIAL MOVE: En Passent
            if(piece is Pawn && (destiny.Row == origin.Row - 2 || destiny.Row == origin.Row + 2))
            {
                EnPassentTarget = piece;
            }
            else
            {
                EnPassentTarget = null;
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
