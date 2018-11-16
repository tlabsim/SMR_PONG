using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
	public float speed = 100.0f;

	float pz_limit = 30.0f;
	float nz_limit = -30.0f; 

	Rigidbody rb;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody>();
	}

    public override void OnStartLocalPlayer()
    {
        Debug.Log("Start local player");

        if (GameSettings.IsHost)
        {
            GetComponent<MeshRenderer>().material.color = Color.blue;

            GetComponent<Rigidbody>().transform.position = new Vector3(45, 5, 0);
            GetComponent<Rigidbody>().transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));

        }
        else
        {
            GetComponent<MeshRenderer>().material.color = Color.magenta;

            GetComponent<Rigidbody>().transform.position = new Vector3(-45, 5, 0);
            GetComponent<Rigidbody>().transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            return;
        }

		float dz = Input.GetAxis ("Horizontal");

		MoveHandle (dz);
	}

	public void MoveHandle(float dz)
	{
        if (!isLocalPlayer)
        {
            return;
        }

		float rbz = rb.transform.position.z;
		float rbdz = dz * speed * Time.deltaTime;
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
