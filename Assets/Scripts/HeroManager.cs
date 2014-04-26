using UnityEngine;
using System.Collections;

public class HeroManager : EnhancedBehaviour 
{
	public float characterSpeed = 2f;
	public bool stealthEnabled = false;

	private Vector2 moveDirection;
	private Rigidbody2D body;

	public Rigidbody2D Body {
		get {
			if(!body) 
				body = rigidbody2D;
			return body;
		}
	}

	// Use this for initialization
	protected override void EnhancedStart ()
	{
		base.EnhancedStart ();
		moveDirection = new Vector2(0f, 0f);
	}
	
	protected override void EnhancedUpdate ()
	{
		base.EnhancedUpdate ();

		//resetting moving and shooting vectors
		moveDirection = new Vector2(0f, 0f);
		
		//getting the moving input
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		
		//Moving the character
		moveDirection = new Vector2(h, v);
	}

	protected override void EnhancedFixedUpdate ()
	{
		base.EnhancedFixedUpdate ();
		Body.velocity = moveDirection * characterSpeed;
	}

	protected override void EnhancedOnCollisionEnter2D (Collision2D col)
	{
		base.EnhancedOnCollisionEnter2D (col);
		Debug.Log("Collision");
	}

	protected override void EnhancedOnTriggerEnter2D (Collider2D col)
	{
		base.EnhancedOnTriggerEnter2D (col);
		Debug.Log("Trigger");
	}
}
