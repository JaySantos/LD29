using UnityEngine;
using System.Collections;

public class HeroManager : MonoBehaviour 
{
	public float characterSpeed = 2f;
	public bool stealthEnabled = false;

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

	void OnCollisionEnter(Collision collider)
	{
		Debug.Log("Parede");
	}
}
