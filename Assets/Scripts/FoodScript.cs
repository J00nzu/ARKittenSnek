using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour {

	GridScript grid;

	// Use this for initialization
	void Start () {
		grid = FindObjectOfType<GridScript>();
		StartCoroutine(RePosStart());
	}

	IEnumerator RePosStart () {
		yield return null;
		RePosition();
	}

	public void RePosition () {
		int x = Random.Range(0, grid.getGridSize());
		int y = Random.Range(0, grid.getGridSize());
		Vector3 pos = grid.getTarget(x, y);
		transform.position = pos;
	}
}
