using UnityEngine;
using System.Collections;

public class MouseTransform : EnhancedBehaviour {

	/// <summary>
	/// My camera.
	/// </summary>
	Camera myCamera;

	public Camera MyCamera {
		get {
			if(!myCamera) 
				myCamera = Camera.main;
			return myCamera;
		}
	}


	/// <summary>
	/// My transform.
	/// </summary>
	Transform myTransform;

	public Transform MyTransform {
		get {
			if(!myTransform) 
				myTransform = transform;
			return myTransform;
		}
	}



	protected override void EnhancedOnEnable ()
	{
		base.EnhancedOnEnable ();

#if !UNITY_EDITOR && !UNITY_STANDALONE
		gameObject.SetActive(false);
#endif
		Screen.showCursor = false;
	}


	Vector3 velocity = Vector3.zero;


	protected override void EnhancedLateUpdate ()
	{
		base.EnhancedLateUpdate ();
//		Vector2 mousePosition = MyCamera.ScreenToWorldPoint(Input.mousePosition);

		velocity.x = Input.GetAxis("Mouse X");
		velocity.y = Input.GetAxis("Mouse Y");

//		MyTransform.position = mousePosition;
		MyTransform.Translate(velocity);
	}
}
