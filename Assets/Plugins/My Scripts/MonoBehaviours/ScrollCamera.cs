using UnityEngine;
using System.Collections;

public class ScrollCamera : EnhancedBehaviour {

	[SerializeField]
	Camera myCamera;
	
	public Camera MyCamera {
		get {
			if(!myCamera)
				myCamera = Camera.main;
			return myCamera;
		}
	}

	[SerializeField]
	float maxSpeed = 10f;

	[SerializeField]
	float offsetScreen = 0.1f;

	protected override void EnhancedLateUpdate ()
	{
		base.EnhancedLateUpdate ();
		
		Vector3 viewportPos = MyCamera.WorldToViewportPoint(transform.position);
		Vector3 currentPos = MyCamera.transform.position;
		
		if(viewportPos.x < offsetScreen || viewportPos.x > 1 - offsetScreen) {
			currentPos.x = Mathf.MoveTowards(currentPos.x, transform.position.x, maxSpeed * Time.deltaTime);
		}

		if(viewportPos.y < offsetScreen || viewportPos.y > 1 - offsetScreen) {
			currentPos.y = Mathf.MoveTowards(currentPos.y, transform.position.y, maxSpeed * Time.deltaTime);
		}

		MyCamera.transform.position = currentPos;
	}
}
