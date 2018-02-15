using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class clickToChangeScene : MonoBehaviour {

	void Start () {
		Time.timeScale = 1;
		VuforiaBehaviour.Instance.enabled = false;
	}

	void OnMouseDown ()
    {
		VuforiaBehaviour.Instance.enabled = true;
		SceneManager.LoadScene("gameScene");
    }
}
