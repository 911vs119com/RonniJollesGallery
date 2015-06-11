using UnityEngine;
using System.Collections;

public class PopulateArtFrames : MonoBehaviour {
	public ImageStruct[] images;

	void Start () {
		int imageIndex = 0;
		foreach (Transform artwork in transform) {
			GameObject art = artwork.FindChild("Image").gameObject;
			if (images[imageIndex].image != null) {
				PopulateByImage( art, images[imageIndex].image );
			} else {
				Debug.Log ("by name: " + images[imageIndex].imageName);
				PopulateWithB2M( art, images[imageIndex].imageName );
			}

			art.transform.localScale = FitImageScale( art.transform.localScale, images[imageIndex].size.x, images[imageIndex].size.y ); 

			imageIndex++;
			if (imageIndex == images.Length) imageIndex = 0;
		}
	}

	private void PopulateByImage( GameObject art, Texture texture ) {
		Renderer rend = art.GetComponent<Renderer>();
		Shader shader = Shader.Find("Standard");
		Material mat = new Material( shader );
		mat.mainTexture = texture;
		rend.material = mat; // will clone the material 
	}

	private void PopulateWithB2M( GameObject art, string name ) {
		string folder = "JollesArtPhotos/";

		//PopulateByImage (art, albedo);

		Renderer rend = art.GetComponent<Renderer>();
		Shader shader = Shader.Find("Standard");
		Material mat = new Material( shader );

		Texture tex;
		tex = Resources.Load (name + "_Base_Color") as Texture;
		mat.mainTexture = tex;						// Albedo
		mat.SetFloat ("_Glossiness", 0f); 			// Smoothness
		tex = Resources.Load (name + "_Normal") as Texture;
		mat.SetTexture ("_BumpMap", tex );			// Normal Map
		tex = Resources.Load ( name + "_Height") as Texture;
		mat.SetTexture ("_ParallaxMap", tex );		// Height Map
		tex = Resources.Load ( name + "_Ambient_Occlusion") as Texture;
		mat.SetTexture ("_OcclusionMap", tex);		// Occlusion

		rend.material = mat; // will clone the material 

	}

	private Vector3 FitImageScale( Vector3 scale, float imageX, float imageY ) {
		float frame_aspect = scale.y / scale.x; // same for all but whatever...
		float image_aspect = imageY / imageX;
		if (image_aspect > frame_aspect) {
			scale.x *= frame_aspect / image_aspect;
		} else {
			scale.y *= image_aspect / frame_aspect;
		}
		return scale;
	}
}
