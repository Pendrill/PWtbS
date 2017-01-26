using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftRight : MonoBehaviour {

	public Rigidbody playerRigidBody;
	public int playerSpeed;
	// Use this for initialization
	void Start () {
		playerRigidBody = GetComponent<Rigidbody> ();
		playerSpeed = 1000;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.D)) {
			playerRigidBody.AddForce (transform.right * playerSpeed * Time.deltaTime);
		} else if (Input.GetKey (KeyCode.A)) {
			playerRigidBody.AddForce (-transform.right * playerSpeed * Time.deltaTime);
		}
	}
}
