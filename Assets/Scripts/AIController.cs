using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {
	public GameObject Ball;
	public float speed = 100.0f;

	float pz_limit = 30.0f;
	float nz_limit = -30.0f; 

	Rigidbody rb;
	Rigidbody brb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();

		if (Ball != null) 
		{
			brb = Ball.GetComponent<Rigidbody>();
		}
	}

	void Reset()
	{
		rb.transform.position = new Vector3 (0, 0, 0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(brb != null)
		{
			float z = brb.velocity.z * 0.01f;			
			float rbz = rb.transform.position.z;

			if(z < 5)
			{
				z += (brb.gameObject.transform.position.z - rbz) * 0.2f;
			}

			float rbdz = z * speed * Time.deltaTime;
			float rbfz = rbz + rbdz;
			
			//Debug.Log (z + " : " + rbdz);
			
			if (rbfz > pz_limit) 
			{
				rbdz = pz_limit - rbz;
			}
			else if (rbfz < nz_limit) 
			{
				rbdz = nz_limit - rbz;
			}
			
			rb.transform.position += new Vector3 (0, 0, rbdz);
		}
	}
}
