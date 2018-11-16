using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject Board;
	public GameObject MenuHUD;
	public GameObject ControlHUD;

	Text GameTitle;
	Text WinText;
	Button StartButton;
	Button RestartButton;
	Button QuitButton;

	GameObject Ball;
	public GameObject Player1;
	public GameObject Player2;

	GameObject Scorer;

	int MatchesHeld = 0;

	bool ShowOverlaidControls = true;

	// Use this for initialization
	void Start () {

		GameTitle = GameObject.Find ("GameTitle").GetComponent<Text>();
		WinText = GameObject.Find ("WinText").GetComponent<Text>();

		StartButton = GameObject.Find ("StartButton").GetComponent<Button>();
		RestartButton = GameObject.Find ("RestartButton").GetComponent<Button>();
		QuitButton = GameObject.Find ("QuitButton").GetComponent<Button>();

		Ball = GameObject.FindGameObjectWithTag ("Ball");
		Player1 = GameObject.FindGameObjectWithTag ("Player1");
		Player2 = GameObject.FindGameObjectWithTag ("Player2");

		Scorer = GameObject.FindGameObjectWithTag ("Scorer");

		MatchesHeld = 0;
		ShowStartMenu ();
	}

	public void ShowStartMenu()
	{
		Board.SetActive (false);
		ControlHUD.SetActive (false);
		MenuHUD.SetActive (true);

		GameTitle.gameObject.SetActive (true);
		WinText.gameObject.SetActive (false);

		StartButton.gameObject.SetActive (true);
		RestartButton.gameObject.SetActive (false);
		QuitButton.gameObject.SetActive (true);
	}

	public void ShowWinMenu(string win_text)
	{
		Board.SetActive (false);
		ControlHUD.SetActive (false);
		MenuHUD.SetActive (true);
		
		GameTitle.gameObject.SetActive (false);
		WinText.gameObject.SetActive (true);
		WinText.text = win_text;
		
		StartButton.gameObject.SetActive (false);
		RestartButton.gameObject.SetActive (true);
		QuitButton.gameObject.SetActive (true);

		MatchesHeld++;
	}

	public void StartNewGame()
	{
		MenuHUD.SetActive (false);
		Board.SetActive (true);
		if (ShowOverlaidControls) {
			ControlHUD.SetActive (true);
		} else {
			ControlHUD.SetActive (false);
		}

		ResetGame ();

		//Ball.SendMessage ("Roll");
	}

	public void ResetGame()
	{
        try
        {
            Ball.SendMessage("Reset");
            Player1.transform.position = new Vector3(45, 5, 0);
            Player2.transform.position = new Vector3(-45, 5, 0);
        }
        catch { }

        if (GameSettings.IsHost)
        {
            Scorer.SendMessage("SetScore", new int[] { 0, 0 });
        }
	}

	public void ExitGame()
	{
		Application.Quit ();
	}
}
