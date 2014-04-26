using UnityEngine;
using System.Collections;

public class FollowTransform : EnhancedBehaviour {


	[SerializeField]
	Transform target;

	public Transform Target {
		get {
			return target;
		}
		protected set {
			target = value;
		}
	}

	/// <summary>
	/// Follow axis.
	/// </summary>
	[SerializeField]
	protected bool followX = true, followY = true, followZ = true;


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
	


	protected override void EnhancedLateUpdate ()
	{
		base.EnhancedLateUpdate ();

		if(target) {
			
			Vector3 myPos = MyTransform.position;
			myPos.x = followX ? target.position.x : MyTransform.position.x;
			myPos.y = followY ? target.position.y : MyTransform.position.y;
			myPos.z = followZ ? target.position.z : MyTransform.position.z;
			MyTransform.position = myPos;
		}
	}
}
