using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseScript : MonoBehaviour {

	Animator anim;

	bool hasStarted = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		hasStarted = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown () {
		if (!hasStarted) {
			hasStarted = true;
			anim.SetTrigger("break");
			StartCoroutine(WaitAnim());
			AudioManager.singleton.PlayCola();
			AudioManager.singleton.PlayGame();
		}
	}

	IEnumerator WaitAnim (){
		yield return new WaitForSeconds(2);
		FindObjectOfType<GameStarter>().StartTheGame();
	}

	public IEnumerator EndAnim () {
		FindObjectOfType<GameController>().deathCat();
		yield return new WaitForSeconds(2);
		anim.SetTrigger("reverse");
		yield return new WaitForSeconds(3);
		AudioManager.singleton.PlayMenu();
		Time.timeScale = 1;
		SceneManager.LoadScene(0);
	}
}
