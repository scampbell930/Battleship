using System;
using System.Collections.Generic;

namespace SCampbe8Battleship
{
	public class Game
	{

		private Ship destroyer1;
		private Ship destroyer2;
		private Ship submarine1;
		private Ship submarine2;
		private Ship battleship;
		private Ship carrier;
		private Gameboard gameboard;
		private List<Ship> ships;

		public Game()
		{
			// Initialize and place ships on board
			// Create gameboard
			this.Gameboard = new Gameboard();

			// Initialize Ships using ship placing function to determine bow and stern locations
			int[] location = PlaceShip(2);

			// Parsing the location array into the individual bow and stern arrays
			int[] bowDestroyer1 = { location[0], location[1] };
			int[] sternDestroyer1 = { location[2], location[3] };

			// Creating my ship using the ship constructor
			this.destroyer1 = new Ship(2, "Destroyer", bowDestroyer1, sternDestroyer1);

			// The same process is repeated for all of the remaining ships
			location = PlaceShip(2);
			int[] bowDestroyer2 = { location[0], location[1] };
			int[] sternDestroyer2 = { location[2], location[3] };
			this.destroyer2 = new Ship(2, "Destroyer", bowDestroyer2, sternDestroyer2);

			location = PlaceShip(3);
			int[] bowSubmarine1 = { location[0], location[1] };
			int[] sternSubmarine1 = { location[2], location[3] };
			this.submarine1 = new Ship(3, "Submarine", bowSubmarine1, sternSubmarine1);

			location = PlaceShip(3);
			int[] bowSubmarine2 = { location[0], location[1] };
			int[] sternSubmarine2 = { location[2], location[3] };
			this.submarine2 = new Ship(3, "Submarine", bowSubmarine2, sternSubmarine2);

			location = PlaceShip(4);
			int[] bowBattleship = { location[0], location[1] };
			int[] sternBattleship = { location[2], location[3] };
			this.battleship = new Ship(4, "Battleship", bowBattleship, sternBattleship);

			location = PlaceShip(5);
			int[] bowCarrier = { location[0], location[1] };
			int[] sternCarrier = { location[2], location[3] };
			this.carrier = new Ship(5, "Carrier", bowCarrier, sternCarrier);

			// Initialize Ship List to store ships
			// Gives easy access to all ships at once
			ships = new List<Ship>();
		}

		/// <summary>
		/// Ship properties
		/// </summary>
		public Ship Destroyer1 { get => destroyer1; set => destroyer1 = value; }
        public Ship Destroyer2 { get => destroyer2; set => destroyer2 = value; }
        public Ship Submarine1 { get => submarine1; set => submarine1 = value; }
        public Ship Submarine2 { get => submarine2; set => submarine2 = value; }
        public Ship Battleship { get => battleship; set => battleship = value; }
        public Ship Carrier { get => carrier; set => carrier = value; }
        public Gameboard Gameboard { get => gameboard; set => gameboard = value; }

		/// <summary>
		/// Main program to run a game object
		/// </summary>
		public void Run()
		{
			// Greeting and main menu
			DisplayWelcomeMessage();
			DisplayMainMenu();

			// Process users main menu selection
			MainMenuResult();
		}

        #region Main Menu

		/// <summary>
		/// Displays welcome message when a new game is run
		/// </summary>
        private void DisplayWelcomeMessage()
        {
            Console.WriteLine("Welcome to CommandConsole BATTLESHIP!");
            Console.WriteLine("Select an option and then press <Enter>");
        }

		/// <summary>
		/// Displays the main menu of options to choose from to play the battleship game
		/// </summary>
		private void DisplayMainMenu()
        {
            Console.WriteLine("1) Start Game");
            Console.WriteLine("2) Start Game with Hacks Enabled");
            Console.WriteLine("3) Exit");
        }

		/// <summary>
		/// Recieves input from the user to then be processed
		/// </summary>
		/// <returns></returns>
		private int MainMenuInput()
        {
			bool validInput = false;

			// Loop until valid input is reached
			while (!validInput)
            {
				// Determine what input was given
				int answer = int.Parse(Console.ReadLine());
				
				// Check if input was valid
				if (answer >= 1 || answer <= 3)
                {
					return answer;
                }
                else
                {
                    Console.WriteLine("Invalid input, please try again:");
                }
            }
			// Keeps function happy and a return for is something breaks in the loop
			return -1;
        }

		/// <summary>
		/// Processes input that was received in MainMenuInput
		/// </summary>
		private void MainMenuResult()
        {
			// Check which value was returned by the MainMenuInput method
            switch (MainMenuInput())
            {
				case 1:     // Start game
					// Create a random game with no hacks
					RandomGame(false);
					break;
				case 2:     // Start game with hacks
					// Create a random game with gameboard hacks
					RandomGame(true);
					break;
				case 3:		// Exit game
                    Console.WriteLine("Goodbye!");
					break;
				default:	// Default incase program breaks
                    Console.WriteLine("Something has gone terribly wrong. Help me!");
					break;
            }
        }

        #endregion

        #region Game Process

		/// <summary>
		/// Creates a random battleship game with either hack or no hacks enabled
		/// </summary>
		/// <param name="hacks"></param>
		private void RandomGame(bool hacks)
        {
			// Check if hacks are turned on
			if (hacks)
            {
				gameboard.Hacks = true;
            }

			// Add ships to list
			ships.Add(Destroyer1);
			ships.Add(Destroyer2);
			ships.Add(Submarine1);
			ships.Add(Submarine2);
			ships.Add(Battleship);
			ships.Add(Carrier);

			// Check that all ships have not been sunk
			while (ShipsAfloat())
            {
				// Display board ask for game input
				PlayerMoveOptions();

				// Process Coordinate info
				AskRowInput();
				int row = UserCoordinateInput();
				AskColInput();
				int col = UserCoordinateInput();

				// Check if coordinate hit ship
				CheckHit(row, col);

				// Update if any ships are sunk
				UpdateSunkShips();
            }

			// All Ships have been sunk
			AllShipsSunk();

			// Ask if they want to play again
			PlayAgain();
			ProcessPlayAgain();
		}

		/// <summary>
		/// Processes input given if the player wants to play the game again
		/// </summary>
		private void ProcessPlayAgain()
        {
			switch (PlayAgainInput())
            {
				case 1:     // Yes
                    // Rerun game object to play again
                    Game game = new Game();
                    game.Run();
					break;
				case 2:		// No
                    Console.WriteLine("Goodbye!");
					break;
				default:	// Error case
                    Console.WriteLine("THIS IS A PROBLEM");
					break;
            }
        }

		/// <summary>
		/// Receive input that was given for either playing again or exiting program
		/// </summary>
		/// <returns></returns>
        private int PlayAgainInput()
        {
			bool validAnswer = false;

			// Loop until a valid answer is given
			while (!validAnswer)
            {
				// Initialize answer that was given in command line
				int answer = int.Parse(Console.ReadLine());

				// Determine if answer was valid
				if (answer == 1 || answer == 2)
                {
					return answer;
                }
                else
                {
                    Console.WriteLine("Invalid selection, please try again:");
                }
            }
			// Default return incase of error
			return -1;
        }

		/// <summary>
		/// Displays menu asking if player would like to play another game
		/// </summary>
		private void PlayAgain()
        {
            Console.WriteLine("Would you like to play again?");
            Console.WriteLine("1) Yes");
            Console.WriteLine("2) No");
        }

		/// <summary>
		/// Displays message telling player that they have officially sunk all of the computer's ships
		/// </summary>
		private void AllShipsSunk()
        {
			Console.WriteLine();
			Console.WriteLine("******************************************************");
			Console.WriteLine("***** CONGRATULATIONS! YOU SUNK ALL OF THE SHIPS *****");
			Console.WriteLine("******************************************************");
			Console.WriteLine();
		}

		/// <summary>
		/// Loops through ship list and update any ships that have been recently sunk
		/// </summary>
		private void UpdateSunkShips()
        {
            // Loop through ship list
            for (int i = 0; i < ships.Count; i++)
            {
				// Check if ship is afloat
				CheckAfloat(ships[i]);
            }
        }

		/// <summary>
		/// Checks if the ship being looked at has been hit enough times to sink it. Then updates afloat boolean correctly
		/// </summary>
		/// <param name="ship"></param>
		/// <returns></returns>
		private bool CheckAfloat(Ship ship)
        {
			// Check if ship has already been sunk
			if (ship.Afloat == true)
            {
				// Check orientation of ship
				if (ShipOrientation(ship) == "right")
				{
					// Loop from bow to stern checking hits
					for (int i = 0; i < ship.Length; i++)
					{
						// Check if ship has been hit at location
						if (gameboard.Board[ship.Bow[0], ship.Bow[1] + i] != 'O')
						{
							return true;
						}
					}
				}
				else if (ShipOrientation(ship) == "left")
				{
					// Loop from bow to stern checking hits
					for (int i = 0; i < ship.Length; i++)
					{
						// Check if ship has been hit at location
						if (gameboard.Board[ship.Bow[0], ship.Bow[1] - i] != 'O')
						{
							return true;
						}
					}
				}
				else if (ShipOrientation(ship) == "down")
				{
					// Loop from bow to stern checking hits
					for (int i = 0; i < ship.Length; i++)
					{
						// Check if ship has been hit at location
						if (gameboard.Board[ship.Bow[0] + i, ship.Bow[1]] != 'O')
						{
							return true;
						}
					}
				}
				else
				{
					// Loop from bow to stern checking hits
					for (int i = 0; i < ship.Length; i++)
					{
						// Check if ship has been hit at location
						if (gameboard.Board[ship.Bow[0] - i, ship.Bow[1]] != 'O')
						{
							return true;
						}
					}
				}

				// Declare ship sunk to console and update afloat boolean
				Console.WriteLine($"***** YOU SUNK A { ship.ShipName }! *****");
				ship.Afloat = false;
				return false;
			}

			// Default return
			return false;

		}

		/// <summary>
		/// Determines ship orientation so that it can then be checked for afloat status
		/// </summary>
		/// <param name="ship"></param>
		/// <returns></returns>
		private String ShipOrientation(Ship ship)
        {
			// Determine ship orientation based on bow and stern relative locations
			if (ship.Bow[1] < ship.Stern[1])
            {
				return "right";
            }
			else if (ship.Bow[1] > ship.Stern[1])
            {
				return "left";
            }
			else if (ship.Bow[0] < ship.Stern[0])
            {
				return "down";
            }
            else
            {
				return "up";
            }
        }

		/// <summary>
		/// Determines if the coordinates that player attacked hit or missed the computer's ships
		/// </summary>
		/// <param name="row"></param>
		/// <param name="col"></param>
		private void CheckHit(int row, int col)
        {
            // Create space on console
            Console.WriteLine();

			// Check if ship occupies location
			if (gameboard.Board[row, col] == 'S')
            {
				// Change location to a hit
				gameboard.Board[row, col] = 'O';

                // Display hit message
                Console.WriteLine("***** YOU HIT A SHIP! *****");
            }
            else
            {
				// Display miss on board and missed message to player
				gameboard.Board[row, col] = 'X';
				Console.WriteLine("***** YOU MISSED! *****");
            }
        }

		/// <summary>
		/// Displayes message for row coordinate input
		/// </summary>
		private void AskRowInput()
        {
            Console.Write("Row: ");
        }

		/// <summary>
		/// Displayes message for column coordinate input
		/// </summary>
		private void AskColInput()
        {
            Console.Write("Column: ");
        }

		/// <summary>
		/// Receives the row or column input from the command console
		/// </summary>
		/// <returns></returns>
		private int UserCoordinateInput()
        {
			bool validAnswer = false;

			// Loop until valid answer is given
			while (!validAnswer)
            {
				// Initializing answer from command console
				int coord = int.Parse(Console.ReadLine());

				// Check if coord is valid
				if (coord >= 0 && coord <= 9)
                {
					return coord;
                }
                else
                {
                    Console.Write("Invalid coordinate, please try again:");
                }

            }
			// Default return incase of error
			return -1;
        }

		/// <summary>
		/// Displayes the gameboard and then asks for player to enter attack coordinates
		/// </summary>
		private void PlayerMoveOptions()
        {
            Console.WriteLine();
            Console.WriteLine("      GAMEBOARD");
			gameboard.Display();
            Console.WriteLine();
            Console.WriteLine("Which coordinates would you like to attack?");
        }

		/// <summary>
		/// Determines if any ships in the list are still afloat
		/// </summary>
		/// <returns></returns>
		private bool ShipsAfloat()
        {
            // Loop through ship list
            for (int i = 0; i < ships.Count; i++)
            {
				// Determine if ship has been sunk
				if (ships[i].Afloat == true)
                {
					return true;
                }
            }
			return false;
        }

        #endregion

        #region Placeing Ships on Board

        /// <summary>
        /// Method to randomly place a ship in an open spot on the gameboard
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public int[] PlaceShip(int length)
		{
			// Initialize return array and pick random bow location
			int[] location = new int[4];

			// Creating random number object and initializing bow location
			Random random = new Random();
			int bowRow = 0;
			int bowCol = 0;

			// Boolean to set to false when both locations work and stern array
			Boolean searching = true;
			int sternRow = 0;
			int sternCol = 0;

			// Initialize orientation of ship
			int direction;

			// Loop until a viable location has been found
			while (searching)
            {
				// Choose random location to check
				bowRow = random.Next(0, 10);
				bowCol = random.Next(0, 10);

				// Randomly choose ship orientation
				direction = random.Next(1, 5);

				// Check different directions for stern placement
				// Check right
				if (direction == 1 && bowCol + length < 10)
                {
					sternRow = bowRow;
					sternCol = bowCol + length;

					// Check that no other ships will be intersected
					if (CheckForShips(bowRow, bowCol, length, "right"))
					{
						// Add ship to board
						for (int i = 0; i < length; i++)
						{
							gameboard.Board[bowRow, bowCol + i] = 'S';
						}

						// Stop searching
						searching = false;

						// Add bow and stern location to return array
						location[0] = bowRow;
						location[1] = bowCol;
						location[2] = sternRow;
						location[3] = sternCol;
					}
				}
				// Check left
				else if (direction == 2 && bowCol - length >= 0)
                {
					sternRow = bowRow;
					sternCol = bowCol - length;

					// Check that no other ships will be intersected
					if (CheckForShips(bowRow, bowCol, length, "left"))
					{
						// Add ship to board
						for (int i = 0; i < length; i++)
						{
							gameboard.Board[bowRow, bowCol - i] = 'S';
						}

						// Stop searching
						searching = false;

						// Add bow and stern location to return array
						location[0] = bowRow;
						location[1] = bowCol;
						location[2] = sternRow;
						location[3] = sternCol;
					}
				}
				// Check Up
				else if (direction == 3 && bowRow - length >= 0)
                {
					sternRow = bowRow - length;
					sternCol = bowCol;

					// Check that no other ships will be intersected
					if (CheckForShips(bowRow, bowCol, length, "up"))
					{
						// Add ship to board
						for (int i = 0; i < length; i++)
						{
							gameboard.Board[bowRow - i, bowCol] = 'S';
						}

						// Stop searching
						searching = false;

						// Add bow and stern location to return array
						location[0] = bowRow;
						location[1] = bowCol;
						location[2] = sternRow;
						location[3] = sternCol;
					}
				}
				// Check down
				else if (direction == 4 && bowRow + length < 10)
                {
					sternRow = bowRow + length;
					sternCol = bowCol;

					// Check that no other ships will be intersected
					if (CheckForShips(bowRow, bowCol, length, "down"))
					{
						// Add ship to board
						for (int i = 0; i < length; i++)
						{
							gameboard.Board[bowRow + i, bowCol] = 'S';
						}

						// Stop searching
						searching = false;

						// Add bow and stern location to return array
						location[0] = bowRow;
						location[1] = bowCol;
						location[2] = sternRow;
						location[3] = sternCol;
					}
				}
            }

			return location;

		}

		/// <summary>
		/// Method determines if the ship location will intersect any ships already on the board
		/// </summary>
		/// <param name="row"></param>
		/// <param name="col"></param>
		/// <param name="length"></param>
		/// <param name="direction"></param>
		/// <returns></returns>
		public bool CheckForShips(int row, int col, int length, string direction)
        {
			// Check which direction needs to be looked at
			if (direction == "right")
            {
				// Loop through board checking that it is empty
				for (int i = 0; i < length; i++)
                {
					if (gameboard.Board[row, col + i] != ' ')
                    {
						return false;
                    }
                }
            }
			else if (direction == "left")
            {
				// Loop through board checking that it is empty
				for (int i = 0; i < length; i++)
				{
					if (gameboard.Board[row, col - i] != ' ')
					{
						return false;
					}
				}
			}
			else if (direction == "up")
            {
				// Loop through board checking that it is empty
				for (int i = 0; i < length; i++)
				{
					if (gameboard.Board[row - i, col] != ' ')
					{
						return false;
					}
				}
			}
            else
            {
				// Loop through board checking that it is empty
				for (int i = 0; i < length; i++)
				{
					if (gameboard.Board[row + i, col] != ' ')
					{
						return false;
					}
				}
			}

			return true;

        }

		#endregion

	}

}
