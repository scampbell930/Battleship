using System;

namespace SCampbe8Battleship
{
	public class Gameboard
	{

		private char[,] board = new char[10, 10];
        bool hacks = false;


        /// <summary>
        /// Gameboard constructor
        /// </summary>
		public Gameboard()
		{
            // Initially fills board with all spaces
			FillBoard();
		}


        /// <summary>
        /// Gameboard properties
        /// </summary>
        public char[,] Board { get => board; set => board = value; }
        public bool Hacks { get => hacks; set => hacks = value; }


        /// <summary>
        /// Display prints the board at its current state
        /// </summary>
        public void Display()
        {
            // Print top numbers for board
            Console.Write("  ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();

            for (int row = 0; row < Board.GetLength(0); row++)
            {
                Console.Write(row);
                Console.Write('|');
                for (int col = 0; col < Board.GetLength(1); col++)
                {
                    // Check if hacks are on
                    if (hacks)
                    {
                        Console.Write(Board[row, col] + "|");
                    }
                    else if (Board[row, col] == 'S')
                    {
                        Console.Write(" " + "|");
                    }
                    else
                    {
                        Console.Write(Board[row, col] + "|");
                    }
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Fills board with all spaces
        /// </summary>
        public void FillBoard()
        {
            for (int row = 0; row < Board.GetLength(0); row++)
            {
                for (int col = 0; col < Board.GetLength(1); col++)
                {
                    Board[row, col] = ' ';
                }
            }
        }

	}

}
