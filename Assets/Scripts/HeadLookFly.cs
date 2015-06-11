using UnityEngine;
using System.Collections;

public class HeadLookFly : MonoBehaviour {
	public float velocity = 0.7f;
	private Camera camera;

	void Start () {
		camera = GameObject.FindWithTag ("MeCamera").GetComponent<Camera>();
	}

	void Update () {
		Vector3 moveDirection = camera.transform.forward;
		moveDirection *= velocity * Time.deltaTime;
		transform.position += moveDirection;
	}
}
