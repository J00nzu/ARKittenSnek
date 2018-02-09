using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildMove : MonoBehaviour {

	ChildMove child;

	Vector3 lastTarget;
	Vector3 targetPosition;
	Vector2 lastDir;
	Vector2 tempDir;
	public float speed;



	void Start () {
		StartCoroutine(Move());
	}

	void OnTriggerEnter (Collider coll) {
		if (coll.gameObject.tag == "Player") {
			var player = coll.GetComponent<MoveController>();
			if(player != null)
				player.Die();
		}
	}

	IEnumerator Move () {
		yield return null;
		
		while (true) {
			while (Vector3.Distance(transform.position, targetPosition) > 0.01) {
				transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
				yield return null;
			}
			yield return null;
		}
	}

	public void nextTarget (Vector3 target, Vector2 dir) {
		lastDir = tempDir;
		lastTarget = targetPosition;
		tempDir = dir;
		targetPosition = target;

		DoTurn(tempDir);

		if (child != null) {
			child.nextTarget(lastTarget, lastDir);
		}
	}

	public void DoTurn (Vector2 turn) {
		Vector3 turn3 = new Vector3(turn.x, 0, turn.y);
		transform.LookAt(transform.position + turn3);
	}

	public void SpawnFollower (GameObject prefab, float cellLen) {
		if (child != null) {
			child.SpawnFollower(prefab, cellLen);
		} else {
			GameObject c = Instantiate(prefab, transform.parent);
			Vector3 minusLastDir3D = new Vector3(-lastDir.x, 0, -lastDir.y);
			c.transform.position = transform.position + (minusLastDir3D * cellLen);
			child = c.GetComponent<ChildMove>();
			child.nextTarget(lastTarget, lastDir);
			child.speed = speed;
		}
	}
}
