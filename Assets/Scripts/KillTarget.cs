using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KillTarget : MonoBehaviour {
	public GameObject target;
	public ParticleSystem hitEffect;
	public GameObject killEffect;
	public float timeToSelect = 3.0f;
	public int score;
	public GameObject scoreBoard;

	private Camera camera;
	private float countDown;
	private Text scoreText;

	// Use this for initialization
	void Start () {
		camera = GameObject.FindWithTag ("MeCamera").GetComponent<Camera>();
		score = 0;
		countDown = timeToSelect;
		hitEffect.enableEmission = false;

		scoreText = scoreBoard.transform.Find ("Text").GetComponent<Text> ();
		scoreText.text = "Score: 0";
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = new Ray (camera.transform.position, camera.transform.rotation * Vector3.forward);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit) && (hit.collider.gameObject == target)) {
			if (countDown > 0.0f) {
				// on target
				countDown -= Time.deltaTime;
				//	print (countDown);
				hitEffect.transform.position = hit.point;
				hitEffect.enableEmission = true;
			} else {
				// killed
				Instantiate( killEffect, target.transform.position, target.transform.rotation );
				score += 1;
				scoreText.text = "Score: " + score;
				countDown = timeToSelect;
				SetRandomPosition();
			}
		} else {
			// reset
			countDown = timeToSelect;
			hitEffect.enableEmission = false;
		}
	}

	void SetRandomPosition() {
		float x = Random.Range (-5.0f, 5.0f);
		float z = Random.Range (-5.0f, 5.0f);
		target.transform.position = new Vector3 (x, 0.0f, z);
	}
}
