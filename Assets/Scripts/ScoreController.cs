using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ScoreController : NetworkBehaviour {
	GameObject Game;

	public int WinScore = 10;

	TextMesh Player1Side_Player1Score;
	TextMesh Player1Side_Player2Score;
	TextMesh Player2Side_Player1Score;
	TextMesh Player2Side_Player2Score;

    [SyncVar]
    int Player1Score = 0;

    [SyncVar]
    int Player2Score = 0;


	bool gohi = false;

	void Start()
	{
		if (!gohi) {
			GetGameObjectHandles ();
		}
	}

	void GetGameObjectHandles()
	{
		Game = GameObject.FindGameObjectWithTag ("Game");
		
		Player1Side_Player1Score = GameObject.Find ("Player1Side/Player1Score").GetComponent<TextMesh>();
		Player1Side_Player2Score = GameObject.Find ("Player1Side/Player2Score").GetComponent<TextMesh>();
		Player2Side_Player1Score = GameObject.Find ("Player2Side/Player1Score").GetComponent<TextMesh>();
		Player2Side_Player2Score = GameObject.Find ("Player2Side/Player2Score").GetComponent<TextMesh>();

		gohi = true;
	}

    [Server]
	public void SetScore(int[] scores)
	{
		if (scores.Length == 2) {

		    Player1Score = scores [0];
			Player2Score = scores [1];

			if (!gohi) {
				GetGameObjectHandles ();
			}

			Player1Side_Player1Score.text = Player1Score.ToString ();
			Player1Side_Player2Score.text = Player2Score.ToString ();
			Player2Side_Player1Score.text = Player1Score.ToString ();
			Player2Side_Player2Score.text = Player2Score.ToString ();

			if (Player1Score >= WinScore) {
				Game.SendMessage ("ShowWinMenu", "Player 1 Won!");

				Debug.Log ("Player 1 Won!");
			} 
			else if (Player2Score >= WinScore) {
				Game.SendMessage ("ShowWinMenu", "Player 2 Won!");

				Debug.Log ("Player 1 Won!");
			}

			Debug.Log (scores [0] + " -- " + scores [1]);
		}
	}

    [ClientCallback]
    void Update()
    {
        if (!gohi)
        {
            GetGameObjectHandles();
        }

        Player1Side_Player1Score.text = Player1Score.ToString();
        Player1Side_Player2Score.text = Player2Score.ToString();
        Player2Side_Player1Score.text = Player1Score.ToString();
        Player2Side_Player2Score.text = Player2Score.ToString();

        if (Player1Score >= WinScore)
        {
            Game.SendMessage("ShowWinMenu", "Player 1 Won!");

            Debug.Log("Player 1 Won!");
        }
        else if (Player2Score >= WinScore)
        {
            Game.SendMessage("ShowWinMenu", "Player 2 Won!");

            Debug.Log("Player 1 Won!");
        }

        Debug.Log(Player1Score + " -- " + Player2Score);
    }
}
