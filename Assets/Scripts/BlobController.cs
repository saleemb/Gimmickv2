﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobController : MonoBehaviour {

	public bool canMove;
	public const float maxMoveSpeed = 4f;		// The max speed (units/sec) at which the blob can move horizontally.
	public const float accel = 10f;				// Horizontal acceleration (units/sec^2) experienced by the blob when moving towards gimmick.
	private Rigidbody2D myRigidBody;
	public GameObject gimmick;

	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (canMove) {

			// Accelerate horizontally in the direction of Gimmick, capping horizontal speed with maxMoveSpeed
			float xVel = myRigidBody.velocity.x;
			if (transform.position.x < gimmick.transform.position.x) {
				xVel += accel * Time.deltaTime;
				xVel = Mathf.Min (xVel, maxMoveSpeed);
				transform.localScale = new Vector3 (1f, 1f, 1f);
			} else {
				xVel -= accel * Time.deltaTime;
				xVel = Mathf.Max (xVel, -maxMoveSpeed);
				transform.localScale = new Vector3 (-1f, 1f, 1f);
			}

			myRigidBody.velocity = new Vector3 (xVel, myRigidBody.velocity.y, 0f);
		}
	}

	void OnBecameVisible()
	{
		canMove = true;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "KillPlane")
		{
			gameObject.SetActive(false);

			// Destroy(gameObject);
		}
	}
	void OnEnable()
	{
		canMove = false;
	}
}
