using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	int foodCount;
	public RawImage number1;
	public RawImage number2;
	public RawImage number3;
	char thingy;
	char thingy2;
	char thingy3;


	// Use this for initialization
	void Start () {
		number1 = GameObject.Find ("Number1").GetComponent<RawImage> ();
		number2 = GameObject.Find ("Number2").GetComponent<RawImage> ();
		number3 = GameObject.Find ("Number3").GetComponent<RawImage> ();


		number1.gameObject.SetActive (false);
		number2.gameObject.SetActive (false);
		number3.gameObject.SetActive (false);


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ScoreCounter() {
		foodCount++;
	}

	public void deathCat() {
		FinalScore (foodCount);
	}

	public void FinalScore(int num) {

		char[] numer = num.ToString ().ToCharArray ();

		if (num < 10) {
			thingy = '0';
			thingy2 = '0';
			thingy3 = numer [0];

		} else if (num > 9 && num < 100) {
			thingy = '0';
			thingy2 = numer [0];
			thingy3 = numer [1];
		} else if (num > 99 && num < 1000) {
			thingy = numer [0];
			thingy2 = numer [1];
			thingy3 = numer [2];
		}

		number1.texture = (Texture)Resources.Load(thingy.ToString());
		number2.texture = (Texture)Resources.Load(thingy2.ToString());
		number3.texture = (Texture)Resources.Load(thingy3.ToString());

		number1.gameObject.SetActive (true);
		number2.gameObject.SetActive (true);
		number3.gameObject.SetActive (true);

	}
}
