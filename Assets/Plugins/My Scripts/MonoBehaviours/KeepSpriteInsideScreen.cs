using UnityEngine;
using System.Collections;

public class KeepSpriteInsideScreen : EnhancedBehaviour {

	[SerializeField]
	Camera myCamera;
	
	public Camera MyCamera {
		get {
			if(!myCamera)
				myCamera = Camera.main;
			return myCamera;
		}
	}

	/// <summary>
	/// The sprite renderer.
	/// </summary>
	SpriteRenderer spriteRenderer;

	float widthRel = 0f;
	float heightRel = 0f;

	protected override void EnhancedStart ()
	{
		base.EnhancedStart ();
		spriteRenderer = GetComponent<SpriteRenderer>();

		if(spriteRenderer) {
			Rect rect = spriteRenderer.BoundsToScreenRect(MyCamera);
			widthRel = rect.width  / MyCamera.pixelWidth / 2f; //relative width
			heightRel = rect.height / MyCamera.pixelHeight / 2f; //relative height
		}
	}

	protected override void EnhancedLateUpdate ()
	{
		base.EnhancedLateUpdate ();

		Vector3 viewportPos = MyCamera.WorldToViewportPoint(transform.position);

		viewportPos.x = Mathf.Clamp(viewportPos.x, widthRel, 1f - widthRel);
		viewportPos.y = Mathf.Clamp(viewportPos.y, heightRel, 1f - heightRel);

		transform.position = MyCamera.ViewportToWorldPoint(viewportPos);
	}
}
