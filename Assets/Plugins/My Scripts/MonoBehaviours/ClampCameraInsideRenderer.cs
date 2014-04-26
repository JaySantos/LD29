using UnityEngine;
using System.Collections;

public class ClampCameraInsideRenderer : EnhancedBehaviour {
	
	/// <summary>
	/// A transformada do mapa.
	/// </summary>
	[SerializeField]
	Renderer targetRenderer;

	/// <summary>
	/// My camera.
	/// </summary>
	Camera myCamera;
	
	public Camera MyCamera {
		get {
			if(!myCamera)
				myCamera = camera;
			return myCamera;
		}
	}
	
	float VerticalExtent {
		get {
			return MyCamera.orthographicSize;
		}
	}
	
	float HorizontalExtent {
		get {
			return MyCamera.orthographicSize * MyCamera.aspect;
		}
	}
	
	Vector2 minBounds;
	Vector2 maxBounds;
	
	protected override void EnhancedLateUpdate ()
	{
		base.EnhancedLateUpdate ();
		minBounds.x = (HorizontalExtent - targetRenderer.bounds.size.x / 2) + targetRenderer.transform.position.x;
		minBounds.y = (VerticalExtent - targetRenderer.bounds.size.y / 2) + targetRenderer.transform.position.y;
		
		maxBounds.x = ((targetRenderer.bounds.size.x / 2) - HorizontalExtent) + targetRenderer.transform.position.x;
		maxBounds.y = ((targetRenderer.bounds.size.y / 2) - VerticalExtent) + targetRenderer.transform.position.y;
		
		Vector3 position = transform.position;
		position.x = Mathf.Clamp(position.x, minBounds.x, maxBounds.x);
		position.y = Mathf.Clamp(position.y, minBounds.y, maxBounds.y);
		transform.position = position;
	}
}
