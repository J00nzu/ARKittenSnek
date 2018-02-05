using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour {

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

	void Start () {
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
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			if(!disabledDir.Equals(new Vector2(0, 1))) {
				tempDir = new Vector2(0, 1);
			}
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			if (!disabledDir.Equals(new Vector2(0, -1))) {
				tempDir = new Vector2(0, -1);
			}
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			if (!disabledDir.Equals(new Vector2(1, 0))) {
				tempDir = new Vector2(1, 0);
			}
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			if (!disabledDir.Equals(new Vector2(-1, 0))) {
				tempDir = new Vector2(-1, 0);
			}
		}

	}

	public void DoTurn(Vector2 turn) {
		Vector3 turn3 = new Vector3(turn.x, 0, turn.y);
		transform.LookAt(transform.position+turn3);
	}

	public void Die () {
		targetPosition = new Vector3(0, 1000, 0);
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
		}
	}

}
