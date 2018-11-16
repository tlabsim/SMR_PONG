using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManualController : MonoBehaviour {

	GameObject Player;

	public float MoveSpeed = 1.0f;

	bool IsLeftButtonDown = false;
	bool IsRightButtonDown = false;

	void Start()
	{
        Player = GameObject.FindGameObjectWithTag(GameSettings.LocalPlayerTag);
	}
	
	// Update is called once per frame
	void Update () {
        if(Player == null)
        {
            Player = GameObject.FindGameObjectWithTag(GameSettings.LocalPlayerTag);
        }

        if (Player != null)
        {
            if (IsLeftButtonDown)
            {
                Player.SendMessage("MoveHandle", -MoveSpeed);
            }
            if (IsRightButtonDown)
            {
                Player.SendMessage("MoveHandle", MoveSpeed);
            }
        }
	}

	public void SetLeftButtonState(bool is_down)
	{
		IsLeftButtonDown = is_down;
	}

	public void SetRightButtonState(bool is_down)
	{
		IsRightButtonDown = is_down;
	}
}
