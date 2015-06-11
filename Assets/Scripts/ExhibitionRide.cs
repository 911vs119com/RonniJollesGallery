﻿using UnityEngine;
using System.Collections;

public class ExhibitionRide : MonoBehaviour {
	public GameObject artworks;
	public float startDelay = 3f;
	public float transitionTime = 5f;
	public bool riding = true;

	private AnimationCurve xCurve, zCurve, rCurve;
	private float endX, endZ, endR;

	void Start () {
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
		xCurve = new AnimationCurve (xKeys);
		zCurve = new AnimationCurve (zKeys);
		rCurve = new AnimationCurve (rKeys);
	}
	
	void Update () {
		if (riding) {
			transform.position = new Vector3 (xCurve.Evaluate (Time.time), transform.position.y, zCurve.Evaluate (Time.time));
			Quaternion rot = transform.rotation;
			rot.y = rCurve.Evaluate (Time.time);
			transform.rotation = rot; 

			if (Mathf.Approximately( transform.position.x, endX) &&
			    Mathf.Approximately( transform.position.z, endZ) &&
			    Mathf.Approximately( transform.rotation.y, endR)) {
				riding = false;
			}
		}
	}
}

