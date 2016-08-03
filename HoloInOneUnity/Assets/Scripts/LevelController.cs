using UnityEngine;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {

    public GameObject Goal;
    public int numPlayers;
    public GameObject StartPoint;

    private List<Color> playerColors = new List<Color>() {
        Color.white,
        Color.blue,
        Color.cyan,
        Color.grey,
        Color.yellow,
        Color.magenta
    };

    private List<GameObject> golfBalls;
    private int currentPlayer;
    List<bool> activePlayers;
    List<int> playerScores;

    private void InitializeBand()
    {
        Debug.Log("Starting Band Controller");
        BandAPI.Instance.StartBandAPI();
    }

    public void StartLevel(int numberOfPlayers)
    {
        // Set up the band
        InitializeBand();

        if (numberOfPlayers > 6)
        {
            throw new System.Exception("Too many players");
        }

        numPlayers = numberOfPlayers;
        currentPlayer = 0;
        playerScores = new List<int>( new int[numPlayers] );
        activePlayers = new List<bool>( new bool[numPlayers] );
        golfBalls = new List<GameObject>( new GameObject[numPlayers] );

        Object golfballPrefab = Resources.Load("Prefabs/GolfBall");

        for (int i = 0; i < numPlayers; i++)
        {
            playerScores[i] = 0;
            activePlayers[i] = true;

            // Crate a golfball for each player
            GameObject golfBall = Instantiate(golfballPrefab) as GameObject;
            golfBall.transform.position = StartPoint.transform.position;
            golfBall.SetActive(false);
            golfBall.GetComponent<Renderer>().material.color = playerColors[i];

            golfBalls[i] = golfBall;
        }

        golfBalls[0].SetActive(true);
    }

    public int NextTurn()
    {
        // golfBalls[currentPlayer].SetActive(false);
        playerScores[currentPlayer]++;

        // Iterate through our list of players a max of the number of players looking for the next player
        for (int i = 0; i < numPlayers; i ++)
        {
            currentPlayer++;
            if (currentPlayer == numPlayers)
            {
                currentPlayer = 0;
            }

            if (activePlayers[currentPlayer])
            {
                golfBalls[currentPlayer].SetActive(true);
                return currentPlayer;
            }
        }

        // If we have reached this far, then we have not found an active player so game is done...
        return -1;
    }

    // Returns the next player's turn or -1 if the game is done...
    public int PlayerWins(int playerIndex)
    {
        activePlayers[playerIndex] = false;
        int nextPlayer = NextTurn();

        return nextPlayer;
    }

    public int GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public List<int> GetScores()
    {
        return playerScores;
    }

    IEnumerator<WaitForSeconds> PushCurrentPlayerBall()
    {
        golfBalls[currentPlayer].GetComponent<BallPhysics>().PushBall();
        yield return new WaitForSeconds(3);
        // NextTurn();
    }

	// Use this for initialization
	void Start () {
        StartLevel(4);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            golfBalls[currentPlayer].GetComponent<BallPhysics>().PushBall();
            NextTurn();
            print("PushedBall");
            // PushCurrentPlayerBall();
        }

        double accel = BandAPI.Instance.GetAcceleration();
        // Debug.Log(string.Format("ACCEL - {0}", accel));
        if (accel > 2)
        {
            BandAPI.Instance.CauseVibration(BandAPI.VibrateBand.HitBall);
            Debug.Log("Current Player wants to hit the ball. Player: " + currentPlayer.ToString());
            Debug.Log("Number of golf balls: " + golfBalls.Count);
            var golfBall = golfBalls[currentPlayer];
            Debug.Log("Getting the BallPhysics Component");
            var ballPhysics = golfBall.GetComponent<BallPhysics>();
            Debug.Log("Got the physics component, trying to push the ball: " + currentPlayer.ToString());
            float acclF = (float)accel;
            float pushForce = 40.0f * (acclF / 20.0f);
            ballPhysics.PushBall(pushForce);
            // NextTurn();
        }
    }
}
