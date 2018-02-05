using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveControllerJesse : MonoBehaviour {

	Vector2 tempDir = new Vector2(0, 1);

	Vector3 targetPosition;
	int targetX = 0, targetY = 0;
	GridScript grid;

	public float speed;
	public GameObject catPrefab;

	public Vector2 disabledDir;

	public Button upBut;
	public Button downBut;
	public Button rightBut;
	public Button leftBut;

	private bool upClicked;
	private bool downClicked;
	private bool rightClicked;
	private bool leftClicked;

	void Start () {

		upClicked = false;
		downClicked = false;
		rightClicked = false;
		leftClicked = false;

		Button buttonUp = upBut.GetComponent<Button> ();
		buttonUp.onClick.AddListener (UpOnClick);

		Button buttonDown = downBut.GetComponent<Button> ();
		buttonDown.onClick.AddListener (DownOnClick);

		Button buttonRight = rightBut.GetComponent<Button> ();
		buttonRight.onClick.AddListener (RightOnClick);

		Button buttonLeft = leftBut.GetComponent<Button> ();
		buttonLeft.onClick.AddListener (LeftOnClick);

		grid = FindObjectOfType<GridScript>();

		StartCoroutine(Move());
	}

	IEnumerator Move () {
		yield return null;
		targetPosition = grid.getTarget(targetX, targetY);
		transform.position = targetPosition;
		while (true) {
			while (Vector3.Distance(transform.position, targetPosition) > 0.01) {
				transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed*Time.deltaTime);
				yield return null;
			}
			targetX += (int)tempDir.x;
			targetY += (int)tempDir.y;
			if (targetX < 0 || targetX > 9 || targetY < 0 || targetY > 9) {
				targetX = (int)Mathf.Clamp(targetX, 0, 9);
				targetY = (int)Mathf.Clamp(targetY, 0, 9);

				yield return null;
				continue;
			}
			DoTurn(tempDir);
			targetPosition = grid.getTarget(targetX, targetY);
			disabledDir = -tempDir;
		}
	}
	
	void Update () {
		if (upClicked == true) {
			if(!disabledDir.Equals(new Vector2(0, 1))) {
				tempDir = new Vector2(0, 1);
			}
		}
		if (downClicked == true) {
			if (!disabledDir.Equals(new Vector2(0, -1))) {
				tempDir = new Vector2(0, -1);
			}
		}
		if (rightClicked == true) {
			if (!disabledDir.Equals(new Vector2(1, 0))) {
				tempDir = new Vector2(1, 0);
			}
		}
		if (leftClicked == true) {
			if (!disabledDir.Equals(new Vector2(-1, 0))) {
				tempDir = new Vector2(-1, 0);
			}
		}

	}

	public void DoTurn(Vector2 turn) {
		Vector3 turn3 = new Vector3(turn.x, 0, turn.y);
		transform.LookAt(transform.position+turn3);
	}

	void Prosper() {
		
	}

	void UpOnClick() {
		upClicked = true;
		downClicked = false;
		rightClicked = false;
		leftClicked = false;
	}
	void DownOnClick() {
		upClicked = false;
		downClicked = true;
		rightClicked = false;
		leftClicked = false;
	}
	void RightOnClick() {
		upClicked = false;
		downClicked = false;
		rightClicked = true;
		leftClicked = false;
	}
	void LeftOnClick() {
		upClicked = false;
		downClicked = false;
		rightClicked = false;
		leftClicked = true;
	}

	void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "Food") {
			Destroy (coll.gameObject);
			//canAdd = true;
		}
	}

}
