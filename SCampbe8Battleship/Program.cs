/*
 * Sean Campbell, Feb 22, 2020
 * This is a battleship program that you can play in your command console.
 * The battleship game is one-sided where the user only playes against randomly placed ships
 * on the computers board. The game can be played normally, or with ship visibility hacks turned on.
 * Also, the game can be restarted once all of the computer's ships have been destroyed.
 */

using System;

namespace SCampbe8Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creating a game object and then running the game
            Game game1 = new Game();
            game1.Run();
        }
    }
}
