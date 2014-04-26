using UnityEngine;
using System.Collections;

public class SlideTexture : EnhancedBehaviour {

	/// <summary>
	/// The velocity.
	/// </summary>
	[SerializeField]
	Vector2 slideVelocity = Vector2.right;

	public Vector2 SlideVelocity {
		get {
			return slideVelocity;
		}
		set {
			slideVelocity = value;
		}
	}

	/// <summary>
	/// My renderer.
	/// </summary>
	Renderer myRenderer;

	protected override void EnhancedStart ()
	{
		base.EnhancedStart ();
		myRenderer = renderer;
		if (!myRenderer.material) {
			Debug.LogError ("O renderer precisa ter um material.");
		}
	}

	protected override void EnhancedLateUpdate ()
	{
		base.EnhancedLateUpdate ();
		Vector2 tiling = myRenderer.material.mainTextureOffset;
		tiling += slideVelocity * Time.deltaTime;
		tiling.x = (tiling.x % 1);
		tiling.y = (tiling.y % 1);
		myRenderer.material.mainTextureOffset = tiling;
	}
}
