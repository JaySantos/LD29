using UnityEngine;
using System.Collections;

public class WeaponManager : MonoBehaviour 
{
	public float shootSpeed = 5f;
	public GameObject bulletPrefab;

	private bool fireWeapon;
	private float cooldownTime = 0.5f;
	private Vector2 shootDirection;
	private Transform myTransform;

	// Use this for initialization
	void Start () 
	{	
		fireWeapon = true;
		shootDirection = new Vector2(0f, 0f);
		myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		shootDirection = new Vector2(0f, 0f);

		//getting the shooting input. To be updated once we have dual sticks set up
		if (Input.GetKey(KeyCode.I))
		{
			shootDirection += Vector2.up;
		}
		if (Input.GetKey(KeyCode.K))
		{
			shootDirection -= Vector2.up;
		}
		if (Input.GetKey(KeyCode.J))
		{
			shootDirection -= Vector2.right;
		}
		if (Input.GetKey(KeyCode.L))
		{
			shootDirection += Vector2.right;
		}

		//shooting
		if (shootDirection != Vector2.zero && fireWeapon)
		{
			GameObject bp = Instantiate(bulletPrefab, myTransform.position, Quaternion.identity) as GameObject;
			BulletManager bm = bp.GetComponent<BulletManager>();
			bm.bulletDirection = shootDirection;
			fireWeapon = false;
			StartCoroutine("Cooldown");
		}
	}
	
	IEnumerator Cooldown()
	{
		yield return new WaitForSeconds(cooldownTime);
		fireWeapon = true;
	}
}
