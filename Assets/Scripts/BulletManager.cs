using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour 
{
	public float bulletSpeed = 5f;
	public Vector2 bulletDirection;

	private Transform myTransform;

	// Use this for initialization
	void Start () 
	{
		//bulletDirection = Vector2.zero;
		myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		myTransform.Translate(bulletDirection * bulletSpeed * Time.deltaTime);
	}
}
