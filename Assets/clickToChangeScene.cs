using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class clickToChangeScene : MonoBehaviour {

	void Start () {
		Time.timeScale = 1;
	}

    void OnMouseDown ()
    {
        SceneManager.LoadScene("gameScene");
    }
}
