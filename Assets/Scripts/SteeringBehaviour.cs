using UnityEngine;
using System.Collections;

public abstract class SteeringBehaviour : EnhancedBehaviour {

	/// <summary>
	/// Propriedade da força resultante
	/// do comportamento do inimigo.
	/// </summary>
	/// <value>The steering force.</value>
	public abstract Vector2 SteeringForce {
		get;
	}
}
