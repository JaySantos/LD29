using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	
	[SerializeField]
	Vector3 axis;

	public Vector3 Axis {
		get {
			return this.axis;
		}
		set {
			axis = value.normalized;
		}
	}	
	
	[SerializeField]
	float speed;

	public float Speed {
		get {
			return this.speed;
		}
		set {
			speed = value;
		}
	}
	
	/// <summary>
	/// My transform.
	/// </summary>
	Transform myTransform;
	
	void Start() {
		myTransform = transform;
		axis = axis.normalized;
	}
	
	// Update is called once per frame
	void Update () {
		myTransform.Rotate(axis, speed * Time.deltaTime);
	}
}
