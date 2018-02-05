using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour {

	public float speed;
	private Vector3 moveVec;
	private Vector3 tempMove;

	private Vector3 tempVec;
	private Vector3 turnVec;
	public string tempDir;
	private string dir;
	private bool canAdd;
	private int childs;
	private Vector3 birthPlace;


	public float timer;
	public GameObject catPrefab;

	void Start () {

		canAdd = false;
		childs = 0;
		dir = "right";
		tempDir = dir;

	}
	
	void Update () {

		timer -= Time.deltaTime;

		switch (dir) {

		case "right":
			tempMove = Vector3.right / 30;
			break;

		case "left":
			tempMove = Vector3.left / 30;
			break;

		case "up":
			tempMove = Vector3.forward / 30;
			break;

		case "down":
			tempMove = Vector3.back / 30;
			break;

		}

		tempVec = transform.position;

		if (timer <= 0) {
			tempDir = dir;
			moveVec = tempMove;
			turnVec = transform.position;
			if (canAdd == true) {
				Prosper ();
			}
			timer = 0.5f;
		}

		transform.Translate (moveVec);

		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			dir = "up";
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			dir = "down";
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			dir = "right";
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			dir = "left";
		}

	}

	public void GetTurn(Vector3 turn, string direction) {
		turn = turnVec;
		direction = tempDir;
	}

	void Prosper() {
		childs++;
		tempVec = transform.position;
		switch (tempDir) {

		case "right":
			birthPlace = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
			break;

		case "left":
			birthPlace = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
			break;

		case "up":
			birthPlace = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
			break;

		case "down":
			birthPlace = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
			break;

		}
	
		GameObject clone = Instantiate (catPrefab, birthPlace, Quaternion.identity);
		canAdd = false;
	}

	void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "Food") {
			Destroy (coll.gameObject);
			canAdd = true;
		}
	}

}
