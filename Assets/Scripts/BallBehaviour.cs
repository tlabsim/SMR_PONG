using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BallBehaviour : MonoBehaviour {

	GameObject Scorer;
	GameObject Ready;
	GameObject Go;

	private Rigidbody rb;
	private Vector3 vel;

	int player1_score;
	int player2_score;

	bool collided = false;
	float ball_slowed_down_time = 0.0f;

	void Start () {

		rb = GetComponent<Rigidbody>();

		Scorer = GameObject.FindGameObjectWithTag ("Scorer");
		Ready = GameObject.Find ("Board/CountDown/Ready");
		Go = GameObject.Find ("Board/CountDown/Go");

		player1_score = 0;
		player2_score = 0;

		UpdatePlayerScores ();
	}

	void Update()
	{
		if (collided) {
			float x = Mathf.Abs (rb.velocity.x);
			//float z = Mathf.Abs (rb.velocity.z);
			if (x < 0.5)
			{
				ball_slowed_down_time += Time.deltaTime;
			}
			else
			{
				ball_slowed_down_time = 0.0f;
			}

			if(ball_slowed_down_time >= 3.0f)
			{
				Invoke ("RestartGame", 0.1f);
			}
		}
	}

	void SayReady()
	{
		Ready.SetActive (true);
		Invoke ("SayGo", 1.0f);
	}

	void SayGo()
	{
		Ready.SetActive (false);
		Go.SetActive (true);
		Invoke("GoBall", 1.0f);
	}

	void GoBall()
	{
		Go.SetActive (false);

		float rand1 = Random.Range(0.0f, 2.0f);
		float rand2 = Random.Range (0.0f, 2.0f);
		
		float x = Random.Range (150, 175);
		float z = Random.Range (0, 50);

		x += z; //make x force bigger if z is bigger

		//Debug.Log ("Inital force> X = " + x + ", Z = " +z);
		
		if (rand1 < 1.0f) {
			x *= -1;
		}
		if (rand2 < 1.0f) {
			z *= -1;
		}
			
		rb.AddForce(new Vector3(x, 0, z));
	}
	
	void ResetBall()
	{
		if (rb != null) {
			rb.isKinematic = true;

			rb.velocity = new Vector3 (0, 0, 0);
			rb.angularVelocity = new Vector3 (0, 0, 0);
			gameObject.transform.position = new Vector3 (0, 5, 0);
			gameObject.transform.rotation = new Quaternion (0, 0, 0, 0);

			rb.isKinematic = false;
		}
	}
	
	void RestartGame()
	{
		collided = false;
		ResetBall();
		Invoke("SayReady", 1.0f);
	}
	
	void OnCollisionEnter (Collision coll) 
	{
		if (coll.collider.CompareTag ("Player1")) 
		{
			float zv = coll.collider.attachedRigidbody.velocity.z;
			float abs_zv = Mathf.Abs (zv);
			float z = 50.0f * ((rb.velocity.z / 1.2f) + (zv / 3.0f));
			if(z>200) z=200;else if(z<-200) z= -200;
			rb.AddForce (new Vector3 (-250 - abs_zv, 0, z));

			collided = true;
		} 
		else if (coll.collider.CompareTag ("Player2")) 
		{
			float zv = coll.collider.attachedRigidbody.velocity.z;
			float abs_zv = Mathf.Abs (zv);
			float z = 50.0f * ((rb.velocity.z / 1.2f) + (zv / 3.0f));		
			if(z>200) z=200;else if(z<-200) z= -200;
			rb.AddForce (new Vector3 (250 + abs_zv, 0, z));

			collided = true;
		} 
		else if (coll.collider.CompareTag ("ArenaPlayer1Border")) 
		{
			Debug.Log (coll.collider.gameObject.name);

			player2_score++;
			Invoke("UpdatePlayerScores", 0.0f);

			Invoke("RestartGame", 0.1f);
		} 
		else if (coll.collider.CompareTag ("ArenaPlayer2Border")) 
		{
			Debug.Log (coll.collider.gameObject.name);

			player1_score++;
			Invoke("UpdatePlayerScores", 0.0f);

			Invoke("RestartGame", 0.1f);
		}
	}

	void UpdatePlayerScores()
	{
		Scorer.SendMessage ("SetScore", new int[]{player1_score, player2_score});
	}

	public void Roll()
	{
		collided = false;
		Invoke("SayReady", 1.0f);
	}

	public void Reset()
	{
		player1_score = 0;
		player2_score = 0;

		ResetBall ();
	}
}
