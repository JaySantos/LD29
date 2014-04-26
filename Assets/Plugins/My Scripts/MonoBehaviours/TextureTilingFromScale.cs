using UnityEngine;
using System.Collections;

public class TextureTilingFromScale : EnhancedBehaviour {

	/// <summary>
	/// The default scale.
	/// </summary>
	[SerializeField]
	Vector3 defaultScale = Vector3.one;

	[SerializeField]
	Renderer myRenderer;

	public Renderer MyRenderer {
		get {
			if(!myRenderer)
				myRenderer = renderer;
			return myRenderer;
		}
	}

	/// <summary>
	/// My transform.
	/// </summary>
	[SerializeField]
	Transform myTransform;

	public Transform MyTransform {
		get {
			if(!myTransform)
				myTransform = transform;
			return myTransform;
		}
	}

	/// <summary>
	/// The last scale.
	/// </summary>
	Vector3 lastScale;

	protected override void EnhancedUpdate ()
	{
		base.EnhancedUpdate ();

		Vector3 localScale = MyTransform.localScale;

		if(lastScale != localScale) {

			Vector2 tiling = MyRenderer.sharedMaterial.mainTextureScale;
			tiling.x = localScale.x / defaultScale.x;
			tiling.y = localScale.y / defaultScale.y;
			MyRenderer.sharedMaterial.mainTextureScale = tiling;

			lastScale = localScale;
		}
	}
}
