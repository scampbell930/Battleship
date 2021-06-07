using System;

namespace SCampbe8Battleship
{
	public class Ship
	{

		private int length;
		private string shipName;
		private int[] bow;
		private int[] stern;
		private bool afloat;


		/// <summary>
		/// Constructor for any ship
		/// </summary>
		/// <param name="length"></param>
		/// <param name="shipName"></param>
		/// <param name="bow"></param>
		/// <param name="stern"></param>
		public Ship(int length, string shipName, int[] bow, int[] stern)
		{
			Length = length;
			ShipName = shipName;
			Bow = bow;
			Stern = stern;
			Afloat = true;
		}


		/// <summary>
		/// Ship properties
		/// </summary>
		public int Length
        {
			get { return length; }
            set
            {
				// Checking if a valid length was entered
				if (value >= 2 && value <= 5)
                {
					length = value;
                }
            }
        }
        public string ShipName { get => shipName; set => shipName = value; }
        public int[] Bow { get => bow; set => bow = value; }
        public int[] Stern { get => stern; set => stern = value; }
        public bool Afloat { get => afloat; set => afloat = value; }
    }

}

