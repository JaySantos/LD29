using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour 
{
	public float bulletSpeed = 10f;
	public Vector2 bulletDirection;
	public WeaponType weaponType;
	public enum WeaponType {DEFAULT, MACHINE_GUN, SHOTGUN, ROCKET_LAUNCHER};

	private Transform myTransform;
	private float areaDamage = 1.0f;


	// Use this for initialization
	void Start () 
	{
		myTransform = transform;
	}

	void OnEnable()
	{
		Invoke ("Destroy", 2f);
		myTransform = transform;
		myTransform.rigidbody2D.velocity = bulletDirection * bulletSpeed;
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	void Destroy()
	{
		gameObject.SetActive(false);
	}

	void OnDisable()
	{
		CancelInvoke();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Enemy")
		{
			if (weaponType == WeaponType.ROCKET_LAUNCHER)
			{
				Collider2D[] colls = Physics2D.OverlapCircleAll(myTransform.position, areaDamage);;
				foreach (Collider2D c in colls)
				{
					Debug.Log("Objeto atingido: " + c.gameObject.name);
				}
			}
			Destroy();
		}
	}
}
