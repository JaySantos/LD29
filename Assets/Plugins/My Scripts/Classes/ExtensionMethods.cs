using UnityEngine;
using System.Collections;

static public class ExtensionMethods {
	
	/// <summary>
	/// Gets or add a component. Usage example:
	/// BoxCollider boxCollider = transform.GetOrAddComponent<BoxCollider>();
	/// </summary>
	static public T GetOrAddComponent<T> (this GameObject child, bool searchChild = false) where T: Component {
		
		T result = null;
		
		if(!searchChild) {
			result = child.GetComponent<T>();
		}
		else {
			result = child.GetComponentInChildren<T>();
		}
		if (result == null) {
			result = child.AddComponent<T>();
		}
		return result;
	}

	/// <summary>
	/// Gets or add a component. Usage example:
	/// BoxCollider boxCollider = transform.GetOrAddComponent<BoxCollider>();
	/// </summary>
	static public T GetOrAddComponent<T> (this Component child, bool searchChild = false) where T: Component {
		return child.gameObject.GetOrAddComponent<T>(searchChild);
	}

	/// <summary>
	/// Gets or add a component. Usage example:
	/// BoxCollider boxCollider = transform.GetOrAddComponent<BoxCollider>();
	/// </summary>
	static public K GetOrAddComponent<K,V> (this GameObject child, bool searchChild = false)
		where K : Component 
		where V : Component {
		
		K result = null;
		
		if(!searchChild) {
			result = child.GetComponent<K>();
		}
		else {
			result = child.GetComponentInChildren<K>();
		}
		if (result == null) {
			result = child.AddComponent<V>() as K;
		}
		return result;
	}

	static public void LookAt2D(this Transform transform, Vector3 target) {
		LookAt2D(transform, target, Vector3.up);
	}
	
	static public void LookAt2D(this Transform transform, Vector3 target, Vector3 upAxis) {
		transform.eulerAngles = upAxis * (Mathf.Atan2((target.y - transform.position.y), 
		                                              (target.x - transform.position.x)) * Mathf.Rad2Deg);
	}
	
	static public IEnumerator Fade(this SpriteRenderer spriteRenderer, float fadeTime) {
		
		fadeTime = fadeTime < 0 ? 0 : fadeTime;
		
		Color color = spriteRenderer.color;
		float max = color.a;
		color.a = 0f;
		
		for (float t = 0; t < fadeTime; t += Time.deltaTime) {
			color.a = Mathf.Lerp(0f, max, t / fadeTime);
			spriteRenderer.color = color;
			yield return null;
		}
		
		color.a = max;
		spriteRenderer.color = color;
	}

	static public IEnumerator ColorTo(this SpriteRenderer spriteRenderer, Color targetColor, float time) {
		
		time = time < 0 ? 0 : time;

		Color initColor = spriteRenderer.color;
		
		for (float t = 0; t <= time; t += Time.deltaTime) {
			
			spriteRenderer.color = Color.Lerp(initColor, targetColor, t / time);
			yield return null;
		}
		
		spriteRenderer.color = initColor;
	}

	static public IEnumerator ColorTo(this SpriteRenderer spriteRenderer, Color[] targetColors, float time) {

		if(targetColors.Length > 0) {

			time /= targetColors.Length;
			time = time < 0 ? 0 : time;
			
			Color initColor = spriteRenderer.color;

			for (float t = 0; t <= time; t += Time.deltaTime) {
				
				spriteRenderer.color = Color.Lerp(initColor, targetColors[0], t / time);
				yield return null;
			}

			for (int i = 1; i < targetColors.Length; i++) {

				for (float t = 0; t <= time; t += Time.deltaTime) {
					
					spriteRenderer.color = Color.Lerp(targetColors[i - 1], targetColors[i], t / time);
					yield return null;
				}
			}

			spriteRenderer.color = initColor;
		}

		yield return null;
	}

	/// <summary>
	/// Fades the out.
	/// </summary>
	/// <returns>The out.</returns>
	/// <param name="spriteRenderer">Sprite renderer.</param>
	/// <param name="fadeTime">Fade time.</param>
	static public IEnumerator FadeOut(this SpriteRenderer spriteRenderer, float fadeTime, bool clampForever = false) {
		
		fadeTime = fadeTime < 0 ? 0 : fadeTime;
		
		Color color = spriteRenderer.color;
		float min = color.a;
		color.a = 1f;
		
		for (float t = 0; t < fadeTime; t += Time.deltaTime) {
			color.a = Mathf.Lerp(min, 0f, t / fadeTime);
			spriteRenderer.color = color;
			yield return null;
		}

		if(!clampForever) {
			color.a = min;
			spriteRenderer.color = color;
		}
	}
	
	static public IEnumerator Blink(this Renderer renderer, float blinkTime, int blinksPerSecond) {
		
		float blinkInterval = blinkTime / blinksPerSecond;
		
		for (float t = 0; t < blinkTime; t += blinkInterval) {
			
			renderer.enabled = false;
			yield return new WaitForSeconds(blinkInterval / 2f);
			renderer.enabled = true;
			yield return new WaitForSeconds(blinkInterval / 2f);
		}
		
		renderer.enabled = true;
	}
	
	static public bool CompareLayer(this GameObject gameObject, string name) {
		return gameObject.layer == LayerMask.NameToLayer(name);
	}
	
	/// <summary>
	/// Copy the specified transform.
	/// </summary>
	/// <param name="transform">Transform.</param>
	static public TransformCopy Copy(this Transform transform) {
		return new TransformCopy(transform.position, transform.rotation, transform.localScale);
	}
	
	/// <summary>
	/// Builds from copy.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="copy">Copy.</param>
	static public void BuildFromCopy(this Transform transform, TransformCopy copy) {
		transform.position = copy.Position;
		transform.rotation = copy.Rotation;
		transform.localScale = copy.LocalScale;
	}
	
	/// <summary>
	/// Containses the layer.
	/// </summary>
	/// <returns><c>true</c>, if layer was containsed, <c>false</c> otherwise.</returns>
	/// <param name="layerMask">Layer mask.</param>
	/// <param name="layer">Layer.</param>
	static public bool ContainsLayer(this LayerMask layerMask, int layer) {
		return ((layerMask >> layer) & 1) == 1;
	}


	/// <summary>
	/// Outsides the unit circle.
	/// </summary>
	/// <returns>The unit circle.</returns>
	static public Vector3 OutsideUnitCircle() {
		float randAngle = Random.value * Mathf.PI * 2f;
		return new Vector3(Mathf.Sin(randAngle), -Mathf.Cos(randAngle), 0f);
	}


	/// <summary>
	/// Outsides the unit circle.
	/// </summary>
	/// <returns>The unit circle.</returns>
	static public Vector3 OutsideUnitCircle(float minAngle, float maxAngle) {

		minAngle *= Mathf.Deg2Rad;
		maxAngle *= Mathf.Deg2Rad;

		float randAngle = Random.value * (maxAngle - minAngle) + minAngle;
		return new Vector3(Mathf.Sin(randAngle), -Mathf.Cos(randAngle), 0f);
	}

	/// <summary>
	/// Clamps the angle.
	/// </summary>
	/// <returns>The angle.</returns>
	/// <param name="angle">Angle.</param>
	/// <param name="from">From.</param>
	/// <param name="to">To.</param>
	static public float ClampAngle(float angle, float from, float to) {

		if(angle > 180) angle = 360 - angle;

		angle = Mathf.Clamp(angle, from, to);

		if(angle < 0) angle = 360 + angle;
		
		return angle;
	}

	/// <summary>
	/// Boundses to screen rect.
	/// </summary>
	/// <returns>The to screen rect.</returns>
	/// <param name="bounds">Bounds.</param>
	static public Rect BoundsToScreenRect(this Renderer renderer, Camera camera)
	{
		Bounds bounds = renderer.bounds;

		// Get mesh origin and farthest extent (this works best with simple convex meshes)
		Vector3 origin = camera.WorldToScreenPoint(new Vector3(bounds.min.x, bounds.max.y, 0f));
		Vector3 extent = camera.WorldToScreenPoint(new Vector3(bounds.max.x, bounds.min.y, 0f));
		
		// Create rect in screen space and return - does not account for camera perspective
		return new Rect(origin.x, camera.pixelHeight - origin.y, extent.x - origin.x, origin.y - extent.y);
	}

	/// <summary>
	/// Mirror the specified transform and mirrored.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="mirrored">Mirrored.</param>
	static public void Mirror(this Transform transform, Axis axis, ref bool mirrored) {

		transform.Mirror (axis);
		mirrored = !mirrored;
	}

	static public void Mirror(this Transform transform, Axis axis) {
		
		Vector3 localScale = transform.localScale;

		switch(axis) {
			case Axis.X:
				localScale.x = -localScale.x;
				break;
			case Axis.Y:
				localScale.y = -localScale.y;
				break;
			default:
				localScale.z = -localScale.z;
				break;
		}

		transform.localScale = localScale;
	}

	static public void DebugLog(this Object obj) {
		Debug.Log(obj.ToString());
	}

	static public void DebugLog(this object obj) {
		Debug.Log(obj.ToString());
	}

	static public void AddForce(this Rigidbody2D rigidbody2D, Vector2 force, ForceMode forceMode) {

		switch (forceMode) {
			case ForceMode.Force:
				rigidbody2D.AddForce(force);
				break;
			case ForceMode.Impulse:
				rigidbody2D.AddForce(force / Time.fixedDeltaTime);
				break;
			case ForceMode.Acceleration:
				rigidbody2D.AddForce(force * rigidbody2D.mass);
				break;
			case ForceMode.VelocityChange:	
				rigidbody2D.AddForce(force * rigidbody2D.mass / Time.fixedDeltaTime);
				break;
		}
	}
}