using UnityEngine;
using System.Collections;

public class HeroManager : MonoBehaviour 
{
	public float characterSpeed = 2f;
	public bool stealthEnabled = false;
	public int hitPoints = 5;
	public float stealthTime = 5.0f;
	public GameObject legs;

	public Sprite up;
	public Sprite down;
	public Sprite left;
	public Sprite right;
	public Sprite upRight;
	public Sprite upLeft;
	public Sprite downRight;
	public Sprite downLeft;

	private Vector2 moveDirection;
	private Transform myTransform;
	private Animator legsAnim;

	// Use this for initialization
	void Start () 
	{
		moveDirection = new Vector2(0f, 0f);
		myTransform = transform;
		legsAnim = legs.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//resetting moving and shooting vectors
		moveDirection = new Vector2(0f, 0f);

		//getting the moving input
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		legsAnim.SetFloat("h", h);
		legsAnim.SetFloat("v", v);
		if (h > 0)
		{
			legsAnim.SetBool("LastRight", true);
		}
		else
		{
			legsAnim.SetBool("LastRight", false);
		}

		//Moving the character
		moveDirection = new Vector2(h, v);
		myTransform.Translate(moveDirection * characterSpeed * Time.deltaTime);
	}

	public void EnableStealth()
	{
		stealthEnabled = true;
		gameObject.GetComponent<SpriteRenderer>().material.color = new Color(gameObject.GetComponent<SpriteRenderer>().material.color.r, 
		                                                                     gameObject.GetComponent<SpriteRenderer>().material.color.g, 
		                                                                     gameObject.GetComponent<SpriteRenderer>().material.color.b, 0.5f);
		StartCoroutine("DisableStealth");
	}

	public void RestoreHealth(int health)
	{
		hitPoints += health;
		if (hitPoints > 5)
		{
			hitPoints = 5;
		}
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
	}
}
