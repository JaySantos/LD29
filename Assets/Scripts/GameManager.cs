using UnityEngine;
using System.Collections;

public class GameManager : EnhancedBehaviour 
{
	public EnemySpawner enemySpawner;
	public PowerUpManager powerUpManager;
	public WeaponPickupManager weaponPickupManager;
	public LevelManager levelManager;
	public ScoreManager scoreManager;
	public HeroManager heroManager;

	protected override void EnhancedOnEnable ()
	{
		enemySpawner = GameObject.Find("Enemy Manager").GetComponent<EnemySpawner>();
		powerUpManager = GameObject.Find("PowerUpManagerPrefab").GetComponent<PowerUpManager>();
		weaponPickupManager = GameObject.Find("WeaponPickUpManagerPrefab").GetComponent<WeaponPickupManager>();
		levelManager = GetComponent<LevelManager>();
		scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
	}
	public void StartGame()
	{
		levelManager.BeginLevel();
		enemySpawner.StartSpawning();
		powerUpManager.Activate();
		weaponPickupManager.Activate();
		scoreManager.gameOverImage.SetActive(false);
		GameObject.Find("Player").transform.position = new Vector2(0f, 0f);
		GameObject.Find("Player").GetComponent<HeroManager>().EnableInput = true;
	}

	public void EndGame()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject obj in enemies)
		{
			obj.GetComponent<Enemy>().Recycle();
		}
		enemySpawner.StopSpawning();
		EnemySpawner.NumEnemies = 0;
	}
}
