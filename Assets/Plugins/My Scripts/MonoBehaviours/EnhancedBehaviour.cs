using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnhancedBehaviour : MonoBehaviour {

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake() {
		EnhancedAwake();
	}

	protected virtual void EnhancedAwake() { }

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
		EnhancedStart();
	}

	protected virtual void EnhancedStart() { }
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () {
		EnhancedUpdate();
	}

	protected virtual void EnhancedUpdate() { }

	/// <summary>
	/// Fixeds the update.
	/// </summary>
	void FixedUpdate() {
		EnhancedFixedUpdate();
	}

	protected virtual void EnhancedFixedUpdate() { }

	/// <summary>
	/// Raises the enable event.
	/// </summary>
	void OnEnable() {
		EnhancedOnEnable();
	}

	protected virtual void EnhancedOnEnable() { }

	/// <summary>
	/// Raises the enable event.
	/// </summary>
	void OnDisable() {
		EnhancedOnDisable();
	}
	
	protected virtual void EnhancedOnDisable() { }

	/// <summary>
	/// Raises the application focus event.
	/// </summary>
	/// <param name="pauseStatus">If set to <c>true</c> pause status.</param>
	void OnApplicationFocus(bool pauseStatus) {
		EnhancedOnApplicationFocus(pauseStatus);
	}

	protected virtual void EnhancedOnApplicationFocus(bool pauseStatus) { }

	/// <summary>
	/// Lates the update.
	/// </summary>
	void LateUpdate() {
		EnhancedLateUpdate();
	}

	protected virtual void EnhancedLateUpdate() { }

	/// <summary>
	/// Raises the trigger enter2 d event.
	/// </summary>
	/// <param name="col">Col.</param>
	void OnTriggerEnter2D(Collider2D col) {
		EnhancedOnTriggerEnter2D (col);
	}

	protected virtual void EnhancedOnTriggerEnter2D(Collider2D col) { }

	/// <summary>
	/// Raises the collision enter 2d event.
	/// </summary>
	/// <param name="col">Col.</param>
	void OnCollisionExit2D(Collision2D col) {
		EnhancedOnCollisionExit2D(col);
	}
	
	protected virtual void EnhancedOnCollisionExit2D(Collision2D col) { }

	/// <summary>
	/// Raises the collision enter 2d event.
	/// </summary>
	/// <param name="col">Col.</param>
	void OnCollisionStay2D(Collision2D col) {
		EnhancedOnCollisionStay2D(col);
	}

	protected virtual void EnhancedOnCollisionStay2D(Collision2D col) { }

	/// <summary>
	/// Raises the collision enter 2d event.
	/// </summary>
	/// <param name="col">Col.</param>
	void OnCollisionEnter2D(Collision2D col) {
		EnhancedOnCollisionEnter2D(col);
	}
	
	protected virtual void EnhancedOnCollisionEnter2D(Collision2D col) { }

	/// <summary>
	/// Raises the trigger exit event.
	/// </summary>
	/// <param name="col">Col.</param>
	void OnTriggerExit(Collider col) {
		EnhancedOnTriggerExit(col);
	}

	protected virtual void EnhancedOnTriggerExit(Collider col) { }

	/// <summary>
	/// Raises the trigger stay event.
	/// </summary>
	/// <param name="col">Col.</param>
	void OnTriggerStay(Collider col) {
		EnhancedOnTriggerStay(col);
	}

	protected virtual void EnhancedOnTriggerStay(Collider col) { }

	/// <summary>
	/// Raises the trigger exit event.
	/// </summary>
	/// <param name="col">Col.</param>
	void OnTriggerEnter(Collider col) {
		EnhancedOnTriggerEnter(col);
	}
	
	protected virtual void EnhancedOnTriggerEnter(Collider col) { }

	/// <summary>
	/// Raises the became visible event.
	/// </summary>
	void OnBecameVisible() {
		EnhancedOnBecameVisible();
	}

	/// <summary>
	/// Raises the collision enter event.
	/// </summary>
	/// <param name="col">Col.</param>
	void OnCollisionEnter(Collision col) {
		EnhancedOnCollisionEnter(col);
	}
	
	protected virtual void EnhancedOnCollisionEnter(Collision col) { }

	protected virtual void EnhancedOnBecameVisible() { }

	void OnDestroy() {
		EnhancedOnDestroy();
	}

	protected virtual void EnhancedOnDestroy() { }

	/// <summary>
	/// Raises the application quit event.
	/// </summary>
	void OnApplicationQuit() {
		EnhancedOnApplicationQuit();
	}

	protected virtual void EnhancedOnApplicationQuit() { }

	void OnParticleCollision(GameObject other) {
		EnhancedOnParticleCollision(other);
	}

	protected virtual void EnhancedOnParticleCollision(GameObject other) { }
	
	/// <summary>
	/// Raises the became invisible event.
	/// </summary>
	void OnBecameInvisible() {
		EnhancedOnBecameInvisible();
	}
	
	protected virtual void EnhancedOnBecameInvisible() { }

	void OnDrawGizmos() { 
		EnhancedOnDrawGizmos();
	}

	protected virtual void EnhancedOnDrawGizmos() { }

	void OnDrawGizmosSelected() { 
		EnhancedOnDrawGizmosSelected();
	}
	
	protected virtual void EnhancedOnDrawGizmosSelected() { }

	/// <summary>
	/// Raises the level was loaded event.
	/// </summary>
	/// <param name="level">Level.</param>
	void OnLevelWasLoaded(int level) {
		EnhancedOnLevelWasLoaded (level);
	}

	protected virtual void EnhancedOnLevelWasLoaded(int level) { }

	/// <summary>
	/// Gets the or add component.
	/// </summary>
	/// <returns>The or add component.</returns>
	/// <param name="child">Child.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	protected T GetOrAddComponent<T> (bool searchChild = false) where T: Component {
		return gameObject.GetOrAddComponent<T>(searchChild);
	}

	/// <summary>
	/// Gets the or add component.
	/// </summary>
	/// <returns>The or add component.</returns>
	/// <param name="child">Child.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	protected K GetOrAddComponent<K,V> (bool searchChild = false) where K: Component where V : Component {
		return gameObject.GetOrAddComponent<K,V>(searchChild);
	}
}
