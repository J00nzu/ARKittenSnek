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
		Debug.Log("Meow!");
		if (!hasStarted) {
			hasStarted = true;
			anim.SetTrigger("break");
			StartCoroutine(WaitAnim());
		}
	}

	IEnumerator WaitAnim (){
		yield return new WaitForSeconds(2);
		FindObjectOfType<GameStarter>().StartTheGame();
	}

	public IEnumerator EndAnim () {
		yield return new WaitForSeconds(2);
		anim.SetTrigger("reverse");
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
