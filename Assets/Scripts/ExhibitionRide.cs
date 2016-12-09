using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ExhibitionRide : MonoBehaviour {
	public GameObject artworks;
	public float startDelay = 3f;
	public float transitionTime = 5f;
	public bool riding = false;
    public bool atEnd = false;

    private float startTime;
	private AnimationCurve xCurve, zCurve, rCurve;
	private float endX, endZ, endR;

	void Start () {
        Debug.Log("ExhibitionRide Start");
        startTime = Time.time;

		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		int count = artworks.transform.childCount + 1;
		Keyframe[] xKeys = new Keyframe[count];
		Keyframe[] zKeys = new Keyframe[count];
		Keyframe[] rKeys = new Keyframe[count];

		int i = 0;
		float time = startDelay;
		xKeys [0] = new Keyframe (time, transform.position.x);
		zKeys [0] = new Keyframe (time, transform.position.z);
		rKeys [0] = new Keyframe (time, transform.rotation.y);

		foreach (Transform artwork in artworks.transform) {
			i++;
			time += transitionTime;
			Vector3 pos = artwork.position - artwork.forward;
			xKeys[i] = new Keyframe( time, pos.x );
			zKeys[i] = new Keyframe( time, pos.z );
			rKeys[i] = new Keyframe( time, artwork.rotation.y );
		}
		endX = xKeys [count - 1].value;
		endZ = zKeys [count - 1].value;
		endR = rKeys [count - 1].value;
        Debug.Log("endXYR: " + endX + " " + endZ + " " + endR);
		xCurve = new AnimationCurve (xKeys);
		zCurve = new AnimationCurve (zKeys);
		rCurve = new AnimationCurve (rKeys);
	}
	
	void Update () {
		if (riding) {
            Debug.Log("Riding");
			transform.position = new Vector3 (xCurve.Evaluate (Time.time - startTime), transform.position.y, zCurve.Evaluate (Time.time - startTime));
			//Quaternion rot = transform.rotation;
			//rot.y = rCurve.Evaluate (Time.time);
			//transform.rotation = rot; 

			if (Mathf.Approximately( transform.position.x, endX) &&
			    Mathf.Approximately( transform.position.z, endZ)) {
				riding = false;
                atEnd = true;
			}
		} else if (Input.GetMouseButtonDown(1) || Cardboard.SDK.Triggered) {
            Debug.Log("Not riding + atEnd: " + atEnd);
            if (atEnd)
                RestartScene();
            else
                StartRide();
        }
	}

    private void StartRide() {
        Debug.Log("StartRide");
        GetComponent<AudioSource>().Play();
        riding = true;
        startTime = Time.time;
    }

    private void RestartScene() {
        Debug.Log("RestartScene");
        riding = false;
        atEnd = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

