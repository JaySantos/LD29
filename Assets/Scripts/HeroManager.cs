using UnityEngine;
using System.Collections;

public class HeroManager : EnhancedBehaviour 
{
	public float characterSpeed = 2f;
	public bool stealthEnabled = false;
	public int hitPoints = 5;
	public float stealthTime = 5.0f;
	public GameObject legs;
	public ScoreManager scoreManager;
	public GameObject invisiblePrefab;

	public Sprite up;
	public Sprite down;
	public Sprite left;
	public Sprite right;
	public Sprite upRight;
	public Sprite upLeft;
	public Sprite downRight;
	public Sprite downLeft;

	private Vector2 moveDirection;

	private Animator legsAnim;

	private bool invincibleAfterHit;

	[SerializeField]
	private bool enableInput;

	public bool EnableInput
	{
		get
		{
			return enableInput;
		}
		set
		{
			enableInput = value;
		}
	}

	private float invincibleAfterHitTime = 2.0f;

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

		legsAnim = legs.GetComponent<Animator>();
		scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
	}

	protected override void EnhancedOnEnable()
	{
		hitPoints = 5;
		invincibleAfterHit = false;
		enableInput = true;
	}
	
	protected override void EnhancedUpdate ()
	{
		base.EnhancedUpdate ();

		//resetting moving and shooting vectors
		moveDirection = new Vector2(0f, 0f);
		
		//getting the moving input
		float h;
		float v;
		if (enableInput)
		{
			h = Input.GetAxis("Horizontal");
			v = Input.GetAxis("Vertical");
		}
		else
		{
			h = 0f;
			v = 0f;
		}

		legsAnim.SetFloat("h", h);
		legsAnim.SetFloat("v", v);


		//Moving the character
		moveDirection = new Vector2(h, v);

	}


	public void EnableStealth()
	{
		stealthEnabled = true;
		gameObject.GetComponent<SpriteRenderer>().material.color = new Color(gameObject.GetComponent<SpriteRenderer>().material.color.r, 
		                                                                     gameObject.GetComponent<SpriteRenderer>().material.color.g, 
		                                                                     gameObject.GetComponent<SpriteRenderer>().material.color.b, 0.5f);
		legs.GetComponent<SpriteRenderer>().material.color = new Color(gameObject.GetComponent<SpriteRenderer>().material.color.r, 
		                                                               gameObject.GetComponent<SpriteRenderer>().material.color.g, 
		                                                               gameObject.GetComponent<SpriteRenderer>().material.color.b, 0.5f);
		GameObject invisible = (GameObject)Instantiate(invisiblePrefab, transform.position, Quaternion.identity);
		invisible.transform.parent = transform;
		StartCoroutine("DisableStealth");
	}

	public void RestoreHealth(int health)
	{
		hitPoints += health;
		if (hitPoints > 5)
		{
			hitPoints = 5;
		}
		scoreManager.UpdateScore();
	}

	public void SetBodySprite(Vector2 v)
	{
		SpriteRenderer s = GetComponent<SpriteRenderer>();
		if (v.x > 0) //right
		{
			if (v.y > 0) //up
			{
				s.sprite = upRight;
			}
			else if (v.y < 0) //down
			{
				s.sprite = downRight;
			}
			else //zero
			{
				s.sprite = right;
			}
		}
		else if (v.x < 0) //Left
		{
			if (v.y > 0) //up
			{
				s.sprite = upLeft;
			}
			else if (v.y < 0)
			{
				s.sprite = downLeft;
			}
			else //zero
			{
				s.sprite = left;
			}
		}
		else //zero
		{
			if (v.y > 0) //up
			{
				s.sprite = up;
			}
			else if (v.y < 0) //down
			{
				s.sprite = down;
			}
		}
	}

	IEnumerator DisableStealth()
	{
		yield return new WaitForSeconds(stealthTime);
		stealthEnabled = false;
		gameObject.GetComponent<SpriteRenderer>().material.color = new Color(gameObject.GetComponent<SpriteRenderer>().material.color.r, 
		                                                                     gameObject.GetComponent<SpriteRenderer>().material.color.g, 
		                                                                     gameObject.GetComponent<SpriteRenderer>().material.color.b, 1f);
		foreach(Transform t in transform)
		{
			if (t.gameObject.tag == "InvisibleFX")
			{
				Destroy (t.gameObject);
			}
		}
	}

	protected override void EnhancedFixedUpdate ()
	{
		base.EnhancedFixedUpdate ();
		Body.velocity = moveDirection * characterSpeed;
	}

	protected override void EnhancedOnCollisionEnter2D (Collision2D col)
	{
		base.EnhancedOnCollisionEnter2D (col);
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		//base.EnhancedOnTriggerEnter2D (col);
		if (col.gameObject.tag == "Enemy" && !invincibleAfterHit && !stealthEnabled)
		{
			gameObject.GetComponent<Animation>().Play("HitFlash");
			hitPoints--;
			scoreManager.HP--;
			scoreManager.UpdateScore();
			if (hitPoints > 0)
			{
				invincibleAfterHit = true;
				StartCoroutine("DisableInvincibleAfterHit");
			}
			else
			{
				scoreManager.ShowGameOver();
				enableInput = false;
			}
		}
	}

	IEnumerator DisableInvincibleAfterHit()
	{
		yield return new WaitForSeconds(invincibleAfterHitTime);
		invincibleAfterHit = false;
	}
}
