using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoGoZone : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter (Collider coll) {
		if (coll.gameObject.tag == "Food") {
			var food = coll.GetComponent<FoodScript>();
			food.RePosition();
		}
		if (coll.gameObject.tag == "Player") {
			var player = coll.GetComponent<MoveController>();
			player.Die();
		}
	}

	void OnTriggerStay (Collider coll) {
		if (coll.gameObject.tag == "Food") {
			var food = coll.GetComponent<FoodScript>();
			food.RePosition();
		}
	}
}
