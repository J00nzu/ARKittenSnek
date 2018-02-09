using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public static AudioManager singleton;

	public AudioSource MenuMusic;
	public AudioSource PlayMusic;
	public AudioSource HouseCollapse;
	public AudioSource EatCat;
	public AudioSource DieCat;


	void Awake () {
		if (singleton != null) {
			Destroy(this.gameObject);
			return;
		}
		DontDestroyOnLoad(this);
		singleton = this;
	}

	public void PlayMenu () {
		MenuMusic.Play();
		PlayMusic.Stop();
	}

	public void PlayGame () {
		MenuMusic.Stop();
		PlayMusic.Play();
	}

	public void PlayCola () {
		HouseCollapse.Play();
	}

	public void PlayEat () {
		EatCat.Play();
	}

	public void PlayDie () {
		PlayMusic.Stop();
		DieCat.Play();
	}
}
