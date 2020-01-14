using System;
using System.Collections.Generic;
using System.Text;
using ConsoleChess.GameBoard;
using ConsoleChess.Chess;

namespace ConsoleChess
{
    class Screen
    {
        public static void PrintMatch(ChessMatch match)
        {
            PrintBoard(match.Board);
            Console.WriteLine();
            PrintCapturedPieces(match);
            Console.WriteLine();
            Console.WriteLine($"Round: {match.Round}");
            Console.WriteLine($"Waiting for {match.CurrentPlayer} player's move...");

            if (match.Check)
            {
                Console.WriteLine("CHECK!");
            }
        }

        public static void PrintMatch(ChessMatch match, bool[,] possibleMoves)
        {
            PrintBoard(match.Board, possibleMoves);
            Console.WriteLine();
            PrintCapturedPieces(match);
            Console.WriteLine();
            Console.WriteLine($"Round: {match.Round}");
            Console.WriteLine($"Waiting for {match.CurrentPlayer} player's move...");

            if (match.Check)
            {
                Console.WriteLine("CHECK!");
            }
        }

        public static void PrintCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Captured pieces:");

            Console.Write("White: ");
            PrintSet(match.GetCapturedPiecesByColor(Color.White));
            Console.WriteLine();

            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkBlue;

            Console.Write("Black: ");
            PrintSet(match.GetCapturedPiecesByColor(Color.Black));
            Console.WriteLine();

            Console.ForegroundColor = aux;
        }

        public static void PrintSet(HashSet<GamePiece> set)
        {
            Console.Write("[");

            foreach(GamePiece piece in set)
            {
                Console.Write(piece + " ");
            }
            Console.Write("]");
        }

        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + " ");

                for (int j = 0; j < board.Columns; j++)
                {
                    PrintPiece(board.GetPiece(i, j));
                }

                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintBoard(Board board, bool[,] possibleMoves)
        {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor modifiedBackground = ConsoleColor.Green;

            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + " ");

                for (int j = 0; j < board.Columns; j++)
                {
                    if(possibleMoves[i, j])
                    {
                        Console.BackgroundColor = modifiedBackground;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBackground;
                    }
                    PrintPiece(board.GetPiece(i, j));
                    Console.BackgroundColor = originalBackground;
                }

                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = originalBackground;
        }

        public static void PrintPiece(GamePiece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.Color == Color.White)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }

        public static ChessPosition ReadChessPosition()
        {
            string input = Console.ReadLine();
            char column = input[0];
            int row = int.Parse(input[1] + string.Empty);
            return new ChessPosition(column, row);
        }
    }
}
