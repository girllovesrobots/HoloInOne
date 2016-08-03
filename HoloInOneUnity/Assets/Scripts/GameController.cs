using System;

namespace GameController
{
	public class GameController
	{
        private static GameController instance;

        public static GameController Instance
        {
            get
            {
                if (instance == null)
                {
                    throw new ArgumentNullException("Instance not started!");
                }
                return instance;
            }
        }

        private GameController()
        {
        }

        int currentPlayer;
		int number_of_players;
		bool[] Activity;
		int[] scores;

        // Resets the instance and returns the player whose turn it is.
        public int StartGame(int num_players)
        {
            instance = new GameController();
            instance.currentPlayer = 0;
            instance.number_of_players = num_players;
            instance.scores = new int[number_of_players];
            instance.Activity = new bool[number_of_players];

            for (int i = 0; i < num_players; i ++)
            {
                scores[i] = 0;
                Activity[i] = true;
            }

            return instance.GetCurrentPlayer();
        }

        // returns the next player
		public int NextTurn()
		{   
			scores[currentPlayer] += 1;
			return gotoNextPlayer();
		}

		private int gotoNextPlayer()
		{
			bool foundNextPlayer = false;
            int originalPlayer = currentPlayer;
			//Loop to the next Active player
			do 
			{
                // Exit if we have gone through the whole player list...

                if (currentPlayer == number_of_players - 1)
					currentPlayer = 0;
				else
					currentPlayer = currentPlayer + 1;

				if (Activity[currentPlayer] == true)
					foundNextPlayer = true;

                // We have gone through the whole list and could not find an active player so return -1 indicating we are done....
                if (currentPlayer == originalPlayer)
                {
                    return -1;
                }
			} while (!foundNextPlayer);

            return currentPlayer;
		}

		//Takes in the position of a winning player
		//Switches him to inactive, moves current player to next active player
		//Returns wheteher the game is over
		public bool PlayerWins(int playa)
		{
            Activity[playa] = false;
            int nextPlayer = NextTurn();

            return nextPlayer == -1;
		}

		public int[] GetScore()
		{
			return scores;
		}
		public int GetCurrentPlayer()
		{
			return currentPlayer;
		}
		public bool[] GetActivity()
		{
			return Activity;
		}	
	}
}
