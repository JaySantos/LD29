﻿using UnityEngine;
using System.Collections;

public class WeaponPickupManager : MonoBehaviour 
{
	private const int MACHINE_GUN = 0;
	private const int SHOTGUN = 1;
	private const int ROCKET_LAUNCHER = 2;
	
	public int machineGunAmmo = 30;
	public int shotgunAmmo = 10;
	public int rocketLauncherAmmo = 5;
	
	public float chanceToDraw = 100;
	public float timeBetweenDrews = 5.0f;
	
	private float minX = -4.3f;
	private float maxX = 4.3f;
	private float minY = -2.7f;
	private float maxY = 1.3f;
	
	private int powerUp;
	
	private WeaponManager wm;
	private Animator anim;
	
	// Use this for initialization
	void Start () 
	{
		wm = GameObject.Find("Player").GetComponent<WeaponManager>();
		anim = GetComponent<Animator>();
	}
	
	public void Activate()
	{
		gameObject.GetComponent<BoxCollider2D>().enabled = false;
		gameObject.GetComponent<SpriteRenderer>().enabled = false;
		InvokeRepeating ("TryToShowWeapon", 0.0f, timeBetweenDrews);
	}
	
	void TryToShowWeapon()
	{
		if (!gameObject.GetComponent<BoxCollider2D>().enabled && !gameObject.GetComponent<SpriteRenderer>().enabled)
		{
			int draw = Random.Range (0, 100);
			if (draw < chanceToDraw)
			{
				powerUp = Random.Range(0, 3);
				
				switch (powerUp)
				{
				case MACHINE_GUN:
					anim.SetTrigger("MachineGun");
					break;
					
				case SHOTGUN:
					anim.SetTrigger("Shotgun");
					break;
					
				case ROCKET_LAUNCHER:
					anim.SetTrigger("Rocket");
					break;
				}
			}
			
			float x = Random.Range(minX, maxX);
			float y = Random.Range(minY, maxY);
			
			gameObject.transform.position = new Vector2(x, y);
			
			gameObject.GetComponent<BoxCollider2D>().enabled = true;
			gameObject.GetComponent<SpriteRenderer>().enabled = true;
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			switch (powerUp)
			{
			case MACHINE_GUN:
				if (wm.weaponType != WeaponManager.WeaponType.MACHINE_GUN)
				{
					wm.weaponType = WeaponManager.WeaponType.MACHINE_GUN;
					Debug.Log("Machine Gun Ammo = " + machineGunAmmo);
					wm.ammo = machineGunAmmo;
				}
				else
				{
					wm.ammo += machineGunAmmo;
				}
				break;
				
			case SHOTGUN:
				if (wm.weaponType != WeaponManager.WeaponType.SHOTGUN)
				{
					wm.weaponType = WeaponManager.WeaponType.SHOTGUN;
					wm.ammo = shotgunAmmo;
				}
				else
				{
					wm.ammo += shotgunAmmo;
				}
				break;
				
			case ROCKET_LAUNCHER:
				if (wm.weaponType != WeaponManager.WeaponType.ROCKET_LAUNCHER)
				{
					wm.weaponType = WeaponManager.WeaponType.ROCKET_LAUNCHER;
					wm.ammo = rocketLauncherAmmo;
				}
				else
				{
					wm.ammo += rocketLauncherAmmo;
				}

				break;
			}
			anim.SetTrigger("Empty");
			gameObject.GetComponent<BoxCollider2D>().enabled = false;
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("ScoreManager").GetComponent<ScoreManager>().UpdateScore();
		}
	}
}
