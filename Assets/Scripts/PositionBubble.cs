using UnityEngine;
using System.Collections;

public class CameraFacing : MonoBehaviour {
	public Transform target;
	void Update () {
		transform.LookAt (target);	
	}
}
