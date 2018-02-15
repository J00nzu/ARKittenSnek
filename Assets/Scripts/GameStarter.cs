using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class GameStarter : MonoBehaviour, ITrackableEventHandler {
	TrackableBehaviour mTrackableBehaviour;

	public List<GameObject> startWhenFound;

	public bool kittenPresent;

	// Use this for initialization
	void Start () {
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
			mTrackableBehaviour.RegisterTrackableEventHandler(this);

	}


	public void StartTheGame () {

		FindObjectOfType<MoveController>().StartMoving();
	}

	public void OnTrackableStateChanged (TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus) {
		if (newStatus == TrackableBehaviour.Status.TRACKED) {
			kittenPresent = true;
			Debug.Log("kitten acquired!");
			
		} else if (newStatus == TrackableBehaviour.Status.NOT_FOUND) {
			kittenPresent = false;
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
