using UnityEngine;
using System.Collections;

public class GameManager : EnhancedBehaviour 
{
	public EnemySpawner enemySpawner;
	public PowerUpManager powerUpManager;
	public WeaponPickupManager weaponPickupManager;
	public LevelManager levelManager;

	protected override void EnhancedOnEnable ()
	{
		enemySpawner = GameObject.Find("Enemy Manager").GetComponent<EnemySpawner>();
		powerUpManager = GameObject.Find("PowerUpManagerPrefab").GetComponent<PowerUpManager>();
		weaponPickupManager = GameObject.Find("WeaponPickUpManagerPrefab").GetComponent<WeaponPickupManager>();
		levelManager = GetComponent<LevelManager>();
	}
	public void StartGame()
	{
		levelManager.BeginLevel();
		enemySpawner.StartSpawning();
		powerUpManager.Activate();
		weaponPickupManager.Activate();
	}
}
