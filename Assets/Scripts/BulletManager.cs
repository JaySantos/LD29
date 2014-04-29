using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour 
{
	public float bulletSpeed = 10f;
	public Vector2 bulletDirection;
	public WeaponType weaponType;
	public enum WeaponType {DEFAULT, MACHINE_GUN, SHOTGUN, ROCKET_LAUNCHER};
	public GameObject explosionPrefab;

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
		if (weaponType != WeaponType.SHOTGUN)
		{
			myTransform.rigidbody2D.velocity = bulletDirection * bulletSpeed;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (weaponType == WeaponType.SHOTGUN)
		{
			myTransform.Translate(bulletDirection * bulletSpeed * Time.deltaTime);
		}
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
				Instantiate(explosionPrefab, myTransform.position, Quaternion.Euler(0f, 0f, Random.Range(0.0f, 360.0f)));
				Collider2D[] colls = Physics2D.OverlapCircleAll(myTransform.position, areaDamage);
				foreach (Collider2D c in colls)
				{
					//Debug.Log("Objeto atingido: " + c.gameObject.name);
					if (c.gameObject.tag == "Enemy")
					{
						c.gameObject.GetComponent<Enemy>().RocketKill();
					}
				}
			}
			Destroy();
		}
	}
}
