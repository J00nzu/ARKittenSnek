using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour {

    public GameObject botLeft, topRi;

	public int gridSize = 20;

	public Vector3[,] grid;

	public Vector3 positBot;
	public Vector3 positTop;

	// Use this for initialization
	void Start () {
        init();
	}

    public void init () {
        Transform bottomLeft = botLeft.transform;
        Transform topRight = topRi.transform;

        positBot = bottomLeft.position;
        positTop = topRight.position;

        float bottom = bottomLeft.position.z;
        float left = bottomLeft.position.x;
        float top = topRight.position.z;
        float right = topRight.position.x;

        float height = top - bottom;
        float width = right - left;

        grid = new Vector3[gridSize, gridSize];

        float zz = (bottomLeft.position.y + topRight.position.y) / 2;

        for (int i = 0; i < gridSize; i++) {
            for (int j = 0; j < gridSize; j++) {
                float xx = (float)(i + 0.5f) / gridSize;
                float yy = (float)(j + 0.5f) / gridSize;

                float x = left + xx * width;
                float y = bottom + yy * height;

                Vector3 pos = new Vector3(x, zz, y);
                grid[i, j] = pos;
            }
        }
    }

	public void Update () {
	}

	public Vector3 getTarget (int x, int y) {
		return grid[x, y];
	}

	public int getGridSize () {
		return gridSize;
	}

	public float getCellLength () {
		return Mathf.Abs(grid[0, 0].x - grid[1, 0].x);
	}
}
