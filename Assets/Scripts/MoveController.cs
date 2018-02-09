using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveController : MonoBehaviour {

	Animator anim;
	ChildMove child;

	Vector2 tempDir = new Vector2(0, 1);
	Vector2 lastDir;

	Vector3 targetPosition;
	Vector3 lastTarget;
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

	private float o_speed;

	void Start () {
		grid = FindObjectOfType<GridScript>();
		gc = GameObject.Find ("GameController").GetComponent<GameController>();

		anim = GetComponent<Animator>();
		gc = GameObject.Find ("GameController").GetComponent<GameController>();

		o_speed = speed;
		speed = 0;

		upClicked = false;
		downClicked = false;
		rightClicked = false;
		leftClicked = false;

		Button buttonUp = upBut.GetComponent<Button>();
		buttonUp.onClick.AddListener(UpOnClick);

		Button buttonDown = downBut.GetComponent<Button>();
		buttonDown.onClick.AddListener(DownOnClick);

		Button buttonRight = rightBut.GetComponent<Button>();
		buttonRight.onClick.AddListener(RightOnClick);

		Button buttonLeft = leftBut.GetComponent<Button>();
		buttonLeft.onClick.AddListener(LeftOnClick);

		StartCoroutine(Move());
	}

	IEnumerator Move () {
		yield return new WaitForSeconds(0.1f);
		Debug.Log(targetX + " " + targetY);
		targetPosition = grid.getTarget(targetX, targetY);
		Debug.Log(targetPosition);

		transform.position = targetPosition;
		while (true) {
			while (Vector3.Distance(transform.position, targetPosition) > 0.01) {
				transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed*Time.deltaTime);
				yield return null;
			}
			targetX += (int)tempDir.x;
			targetY += (int)tempDir.y;
			if (targetX < 0 || targetX > grid.getGridSize()-1 || targetY < 0 || targetY > grid.getGridSize()-1) {
				targetX = (int)Mathf.Clamp(targetX, 0, grid.getGridSize()-1);
				targetY = (int)Mathf.Clamp(targetY, 0, grid.getGridSize()-1);

				yield return null;
				continue;
			}
			if (child != null) {
				child.nextTarget(lastTarget, lastDir);
			}
			DoTurn(tempDir);
			lastTarget = targetPosition;
			targetPosition = grid.getTarget(targetX, targetY);
			disabledDir = -tempDir;
			lastDir = tempDir;
		}
	}
	
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow) || upClicked) {
			if(!disabledDir.Equals(new Vector2(0, 1))) {
				tempDir = new Vector2(0, 1);
				upClicked = false;
			}
		}
		if (Input.GetKeyDown (KeyCode.DownArrow) || downClicked) {
			if (!disabledDir.Equals(new Vector2(0, -1))) {
				tempDir = new Vector2(0, -1);
				downClicked = false;
			}
		}
		if (Input.GetKeyDown (KeyCode.RightArrow) || rightClicked) {
			if (!disabledDir.Equals(new Vector2(1, 0))) {
				tempDir = new Vector2(1, 0);
				rightClicked = false;
			}
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow) || leftClicked) {
			if (!disabledDir.Equals(new Vector2(-1, 0))) {
				tempDir = new Vector2(-1, 0);
				leftClicked = false;
			}
		}

	}

	IEnumerator Accelerate () {
		while (speed != o_speed) {
			speed = Mathf.MoveTowards(speed, o_speed, Time.deltaTime*0.1f);
			anim.SetFloat("walkspeed", speed / o_speed);

			yield return null;
		}
	}

	public void StartMoving () {
		StartCoroutine(Accelerate());
		Debug.Log("movecon");
	}

	public void DoTurn(Vector2 turn) {
		Vector3 turn3 = new Vector3(turn.x, 0, turn.y);
		transform.LookAt(transform.position+turn3);
	}

	public void Die () {
		targetPosition = new Vector3(0, 1000, 0);
		StartCoroutine(FindObjectOfType<HouseScript>().EndAnim());
	}

	void Prosper() {
		if (child != null) {
			child.SpawnFollower(catPrefab, grid.getCellLength());
		} else { 
			GameObject c = Instantiate(catPrefab, transform.parent);
			Vector3 disDir3D = new Vector3(disabledDir.x, 0, disabledDir.y);
			c.transform.position = transform.position + (disDir3D * grid.getCellLength());
			child = c.GetComponent<ChildMove>();
			child.nextTarget(lastTarget, lastDir);
			child.speed = speed;
		}
	}

	void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "Food") {
			var food = coll.GetComponent<FoodScript>();
			food.RePosition();
			Prosper();
			gc.ScoreCounter();
		}
	}


	void UpOnClick () {
		upClicked = true;
		downClicked = false;
		rightClicked = false;
		leftClicked = false;
	}
	void DownOnClick () {
		upClicked = false;
		downClicked = true;
		rightClicked = false;
		leftClicked = false;
	}
	void RightOnClick () {
		upClicked = false;
		downClicked = false;
		rightClicked = true;
		leftClicked = false;
	}
	void LeftOnClick () {
		upClicked = false;
		downClicked = false;
		rightClicked = false;
		leftClicked = true;
	}

}
