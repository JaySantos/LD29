using UnityEngine;
using System.Collections;

public class HeroManager : MonoBehaviour 
{
	public float characterSpeed = 2f;
	public bool stealthEnabled = false;
	public int hitPoints = 5;
	public float stealthTime = 5.0f;

	private Vector2 moveDirection;
	private Transform myTransform;

	// Use this for initialization
	void Start () 
	{
		moveDirection = new Vector2(0f, 0f);
		myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//resetting moving and shooting vectors
		moveDirection = new Vector2(0f, 0f);

		//getting the moving input
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

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

	IEnumerator DisableStealth()
	{
		yield return new WaitForSeconds(stealthTime);
		stealthEnabled = false;
		gameObject.GetComponent<SpriteRenderer>().material.color = new Color(gameObject.GetComponent<SpriteRenderer>().material.color.r, 
		                                                                     gameObject.GetComponent<SpriteRenderer>().material.color.g, 
		                                                                     gameObject.GetComponent<SpriteRenderer>().material.color.b, 1f);
	}
}
