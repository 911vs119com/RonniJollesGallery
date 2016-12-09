using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RestartButton : MonoBehaviour {
    private Vector3 startPosition;
    private Quaternion startRotation;

	// Use this for initialization
	void Start () {
        GameObject camera = GameObject.Find("CardboardMain");
        startPosition = camera.transform.position;
        startRotation = camera.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        if (Cardboard.SDK.CardboardTriggered || Input.anyKeyDown)
            RestartScene();
	}

    public void RestartScene() {
        //Application.LoadLevel(Application.loadedLevel);
        //GameObject camera = GameObject.Find("CardboardMain");
        //camera.transform.position = startPosition;
        //camera.transform.rotation = startRotation;
        //camera.GetComponent<ExhibitionRide>().riding = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
