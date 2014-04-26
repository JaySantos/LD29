using UnityEngine;
using System.Collections;

public class Enemy : EnhancedBehaviour {

	/// <summary>
	/// Aqui a gente armazena
	/// a posiçao do heroi.
	/// </summary>
	Transform hero;
	
	public Transform Hero {
		get {
			return hero;
		}
		set {
			hero = value;
		}
	}

	[SerializeField]
	Rigidbody2D body;

	public Rigidbody2D Body {
		get {
			if(!body) 
				body = rigidbody2D;
			return body;
		}
		set {
			body = value;
		}
	}

	public bool IsFrozen {
		get;
		set;
	}

	protected override void EnhancedFixedUpdate ()
	{
		base.EnhancedFixedUpdate ();
	}
}
