using UnityEngine;
using System.Collections;

public class SetWorldScale : EnhancedBehaviour {

	/// <summary>
	/// The current aspect ratio.
	/// </summary>
	public float targetAspectRatio = 480f/800f;

	/// <summary>
	/// The last aspect ratio.
	/// </summary>
	float lastAspectRatio;

#if UNITY_METRO || UNITY_STANDALONE
	public bool forceAspectRatio = false;
#endif

	protected override void EnhancedUpdate ()
	{
		base.EnhancedUpdate();

		float newAspectRatio = Camera.main.aspect;

#if UNITY_METRO || UNITY_STANDALONE
		// Aqui a gente verifica se o aspecto nao e retrato
		if(!forceAspectRatio && newAspectRatio >= 1) {
			DefineAspectRatio(targetAspectRatio);
			return;
		}
#endif
		
		if(!Mathf.Approximately(lastAspectRatio, newAspectRatio)) {
			DefineAspectRatio(newAspectRatio);
		}
	}
	
	void DefineAspectRatio(float newAspectRatio) {
		
		Vector3 scale = transform.localScale;
		scale.x = newAspectRatio / targetAspectRatio;
		transform.localScale = scale;
		lastAspectRatio = newAspectRatio;
	}
}
