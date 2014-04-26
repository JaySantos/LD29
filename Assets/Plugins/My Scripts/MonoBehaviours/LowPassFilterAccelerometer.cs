using UnityEngine;
using System.Collections;

public class LowPassFilterAccelerometer : EnhancedBehaviour {

	static float AccelerometerUpdateInterval = 1.0f / 60.0f;
	static float LowPassKernelWidthInSeconds = 0.05f;

	float LowPassFilterFactor = AccelerometerUpdateInterval / LowPassKernelWidthInSeconds; // tweakable
	Vector3 lowPassValue = Vector3.zero;

	protected override void EnhancedStart ()
	{
		base.EnhancedStart ();
		lowPassValue = Input.acceleration;
	}

	protected override void EnhancedUpdate ()
	{
		base.EnhancedUpdate ();
	}

	public Vector3 Value {
		get {
			if(enabled) {
				lowPassValue = Vector3.Lerp(lowPassValue, Input.acceleration, LowPassFilterFactor);
			}
			return lowPassValue;
		}
	}
}
