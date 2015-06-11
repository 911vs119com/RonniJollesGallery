using UnityEngine;
using System.Collections;

public class FlippinDashboard : MonoBehaviour {
	private HeadGesture gesture;
	private GameObject dashboard;
	private bool isOpen = true;
	private Vector3 startRotation;
	private float timer = 0.0f;
	private float timerReset = 2.0f;

	void Start() {
		gesture = GetComponent<HeadGesture> ();
		dashboard = GameObject.Find ("Dashboard");
		startRotation = dashboard.transform.eulerAngles;
		CloseDashboard ();
	}

	void Update() {
		if (gesture.isMovingDown) {
			//Debug.Log ("down");
			OpenDashboard ();

		} else if (!gesture.isFacingDown) {
			//Debug.Log ("Up");
			timer -= Time.deltaTime;
			if (timer <= 0.0f) {
				CloseDashboard ();
			}
		} else {
			timer = timerReset;
		}
	}

	private void CloseDashboard() {
		if (isOpen) {
			dashboard.transform.eulerAngles = new Vector3 (180f, startRotation.y, startRotation.z);
			isOpen = false;
		}
	}

	private void OpenDashboard() {
		if (!isOpen) {
			dashboard.transform.eulerAngles = startRotation;
			isOpen = true;
		}
	}
}
