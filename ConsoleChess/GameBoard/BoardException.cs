using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleChess.GameBoard
{
    class BoardException : ApplicationException
    {
        public BoardException(string message) : base(message)
        {
        }
    }
}
