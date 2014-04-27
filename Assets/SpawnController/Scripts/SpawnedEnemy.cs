using UnityEngine;
using System.Collections;

public class SpawnedEnemy : MonoBehaviour
{
	public SpawnController spawnController;
	public int waveIndex;
	public int spawnerIndex;
	public BreedPool bp;

	public void Dead()
	{
		SpawnWave wave = spawnController.waves[waveIndex] as SpawnWave;
		SpawnSpawner spawner = wave.spawners[spawnerIndex] as SpawnSpawner;

		// killed type
		if ( wave.type == 1 )
			wave.spawnerEnemiesKilled++;

		// kill type
		if ( spawner.type == 1 )
		{
			spawner.enemiesKilled++;

			if ( spawner.enemiesKilled == spawner.enemiesTotal )
			{
				if ( spawner.endless )
					spawnController.ResetSpawner(wave, spawnerIndex);
				else
					spawner.complete = true;
			}
			else
			{
				spawner.spawned = false;
				spawner.rate = spawner._rate;
			}
		}

		// should do the trick
		bp.Unspawn(gameObject);
	}
}