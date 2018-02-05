using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildMove : MonoBehaviour {

	private GameObject player;
	private MoveController moveCon;

	private Vector3 _turnSpot;
	private string _direction;

	void Start () {

		player = GameObject.Find ("Player");
		moveCon = player.GetComponent<MoveController> ();

	}
	
	void Update () {

		//moveCon.GetTurn (_turnSpot, _direction);

	}
}
