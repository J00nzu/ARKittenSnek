using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour {

	public GameObject[,] grid = new GameObject[10,10];

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 10; i++) {
			for (int j = 0; j < 10; j++) {
				GameObject go = GameObject.Find("Cube" + (i + 1) + "-" + (j + 1));
				grid[i, j] = go;
			}
		}
	}

	public Vector3 getTarget (int x, int y) {
		return grid[x, y].transform.position;
	}
}
