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
		myTransform = transform;
	}

	void OnEnable()
	{
		Invoke ("Destroy", 2f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		myTransform.Translate(bulletDirection * bulletSpeed * Time.deltaTime);
	}

	void Destroy()
	{
		gameObject.SetActive(false);
	}

	void OnDisable()
	{
		CancelInvoke();
	}
}
