using System;

namespace ConsoleChess.GameBoard
{
    class BoardException : ApplicationException
    {
        public BoardException(string message) : base(message)
        {
        }
    }
}
