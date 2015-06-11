using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ButtonTest : MonoBehaviour {
	public Camera camera;

	private int counter = 0;
	private GameObject currentButton;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(Input.inputString);
		Ray ray = new Ray (camera.transform.position, camera.transform.rotation * Vector3.forward);
		RaycastHit hit;
		PointerEventData data = new PointerEventData (EventSystem.current);
		if (Physics.Raycast (ray, out hit)) {
			//Debug.Log ( hit.collider.gameObject.name );
			if (hit.transform.gameObject.tag == "Button") {
				//Debug.Log ("Hit ");
				GameObject button = hit.transform.parent.gameObject; // this is getting a null ref error
				if (button != currentButton) { // unhover
					ExecuteEvents.Execute<IPointerExitHandler> (button, data, ExecuteEvents.pointerExitHandler);
					currentButton = button;
				}
				if (Input.anyKey) { // click
					ExecuteEvents.Execute<IPointerClickHandler> (button, data, ExecuteEvents.pointerClickHandler);
				} else { // hover
					ExecuteEvents.Execute<IPointerEnterHandler> (button, data, ExecuteEvents.pointerEnterHandler);
				}
			} else if (currentButton != null) { // unhover
				ExecuteEvents.Execute<IPointerExitHandler> (currentButton, data, ExecuteEvents.pointerExitHandler);
				currentButton = null;
			}
		}
//		Ray ray = new Ray (camera.transform.position, camera.transform.rotation * Vector3.forward);
//		RaycastHit hit;
//		if (Physics.Raycast (ray, out hit)) {
//			//			hitObject = hit.collider.gameObject;
//			if (hit.collider.tag == "GameController") {
//				Debug.Log ("Screen " + Screen.width + ", " + Screen.height );
//				//counter++;
//				//Debug.Log ("hits: " + counter);
//				PointerEventData pointer = new PointerEventData( EventSystem.current );
//				pointer.Reset();
//				//pointer.position = camera.WorldToScreenPoint( hit.point );
//				pointer.position = new Vector2( 853.0f/2.0f, 539.0f/2.0f );
//				Debug.Log ( pointer.position );
//
//				List<RaycastResult> results = new List<RaycastResult>();
//				EventSystem.current.RaycastAll( pointer, results );
//				if (results.Count > 0) {
//					Debug.Log ("rcount " + results.Count);
//				}
//			}
//		}

//		Debug.Log ("Screen " + Screen.width + ", " + Screen.height );
//		PointerEventData pointer = new PointerEventData( EventSystem.current );
//		pointer.Reset();
//		pointer.position = new Vector2( 853.0f/2.0f, 539.0f/2.0f );
//		//Debug.Log ( pointer.position );
//
//		List<RaycastResult> results = new List<RaycastResult>();
//		EventSystem.current.RaycastAll( pointer, results );
//		if (results.Count > 0) {
//			Debug.Log ("rcount " + results.Count);
//			for (int i=0; i<results.Count; i++) {
//				Debug.Log (results[i]);
//			}
//		}

	}
}
