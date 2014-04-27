using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(SpawnController))]

public class SpawnControllerEditor : Editor
{
	private SpawnController sc = null;
	private int configWave = 0;
	private int configSpawner = 0;
	private int configEnemy = 0;

	public override void OnInspectorGUI()
	{
		sc = target as SpawnController;

		if ( Application.isPlaying )
		{
			// current wave
			EditorGUILayout.LabelField("Current Wave", (SpawnController.currentWave + 1).ToString());
			return;
		}

		// some space
		EditorGUILayout.Space();

		// play wave
		GUIContent[] playWaves = new GUIContent[sc.waves.Count];
		for ( int i = 0; i < sc.waves.Count; i++ )
			playWaves[i] = new GUIContent("Wave " + (i + 1).ToString());

		// play wave
		sc.playWave = EditorGUILayout.Popup(new GUIContent("Play Wave", "Plays this wave once initiated"), sc.playWave, playWaves);

		// play on start
		sc.playOnStart = EditorGUILayout.Toggle(new GUIContent("Play On Start", "Plays the waves once the Scene is ready to go (other wise use SpawnController.ResumeWave() when ready)"), sc.playOnStart);

		// restart wave
		GUIContent[] restartWaves = new GUIContent[sc.waves.Count + 1];
		restartWaves[0] = new GUIContent("Do Not Restart");
		for ( int i = 0; i < sc.waves.Count; i++ )
			restartWaves[i + 1] = new GUIContent("Wave " + (i + 1).ToString());

		sc.restart = EditorGUILayout.Popup(new GUIContent("Restart Wave At", "Once the last wave has finished"), sc.restart, restartWaves);

		// time before first wave
		sc.timeBeforeFirstWave = EditorGUILayout.FloatField(new GUIContent("Time Before First", "The amount of time before we start this wave"), sc.timeBeforeFirstWave);

		// time between waves
		sc.timeBetweenWaves = EditorGUILayout.FloatField(new GUIContent("Time Between Waves", "The amount of time between waves"), sc.timeBetweenWaves);

		// some space
		EditorGUILayout.Space();
		EditorGUILayout.Space();

		// add/remove waves
		GUILayout.BeginHorizontal();
			if ( GUILayout.Button("Add Wave") )
			{
				sc.waves.Add(new SpawnWave());
				configWave = sc.waves.Count - 1;
			}
			if ( GUILayout.Button("Remove Wave") && sc.waves.Count > 0 )
			{
				sc.waves.RemoveAt(configWave);
				configWave = sc.waves.Count - 1;
			}
		GUILayout.EndHorizontal();

		// don't continue editing
		if ( sc.waves.Count == 0 )
		{
			configWave = 0;
			return;
		}

		// list the waves
		string[] numWaves = new string[sc.waves.Count];
		for ( int i = 0; i < sc.waves.Count; i++ )
			numWaves[i] = "Wave " + (i + 1).ToString() + " (of " + sc.waves.Count.ToString() + ")";

		int bConfigWave = configWave;
		configWave = EditorGUILayout.Popup("Config Wave", configWave, numWaves);

		if ( bConfigWave != configWave )
		{
			configSpawner = 0;
			configEnemy = 0;
		}

		if ( configWave > sc.waves.Count - 1 )
			configWave = 0;

		// the wave we are editing
		SpawnWave wave = sc.waves[configWave] as SpawnWave;

		// wave type
		GUIContent[] waveTypes = new GUIContent[2];
		waveTypes[0] = new GUIContent("Time");
		waveTypes[1] = new GUIContent("Kill");
		wave.type = EditorGUILayout.Popup(new GUIContent("Type", "Time: spawned enemies until the desired Time has been reached\nKill: all spawned enemies must be killed before proceeding onto the next wave"), wave.type, waveTypes);

		// show options depending on type
		if ( wave.type == 0 ) // show Timed options
		{
			wave.length = EditorGUILayout.FloatField(new GUIContent("Time", "How long this wave will run"), wave.length);
			wave._length = wave.length;

			bool stranglers = false;
			for ( int i = 0; i < wave.spawners.Count; i++ )
				if ( (wave.spawners[i] as SpawnSpawner).type == 1 )
					stranglers = true;

			// they don't have a choice
			if ( stranglers )
			{
				EditorGUILayout.Toggle(new GUIContent("Allow Stranglers", "Will continue to spawnn enemies if Time has succeeded (Note: this will be checked if any of it's spawners have a Kill Type"), true);
				wave.stranglers = true;
			}
			// they have a choice
			else
				wave.stranglers = EditorGUILayout.Toggle(new GUIContent("Allow Stranglers", "Will continue to spawnn enemies if Time has succeeded (Note: this will be checked if any of it's spawners have a Kill Type"), wave.stranglers);
		}

		// some space
		EditorGUILayout.Space();
		EditorGUILayout.Space();

		// add/remove spawners
		GUILayout.BeginHorizontal();
			if ( GUILayout.Button("Add Spawner") )
			{
				wave.spawners.Add(new SpawnSpawner());
				configSpawner = wave.spawners.Count - 1;
			}
			if ( GUILayout.Button("Remove Spawner") && wave.spawners.Count > 0 )
			{
				wave.spawners.RemoveAt(configSpawner);
				configSpawner = wave.spawners.Count - 1;
			}
		GUILayout.EndHorizontal();

		// don't continue editing
		if ( wave.spawners.Count == 0 )
		{
			configSpawner = 0;
			return;
		}

		// show the spawners in the wave
		string[] listSpawners = new string[wave.spawners.Count];
		for ( int i = 0; i < wave.spawners.Count; i++ )
			listSpawners[i] = "Spawner " + (i + 1).ToString() + " (of " + wave.spawners.Count.ToString() + ")";

		int bConfigSpawner = configSpawner;
		configSpawner = EditorGUILayout.Popup("Config Spawner", configSpawner, listSpawners);

		if ( bConfigSpawner != configSpawner )
			configEnemy = 0;

		if ( configSpawner > wave.spawners.Count - 1 )
			configSpawner = 0;

		// the spawner we are editing
		SpawnSpawner spawner = wave.spawners[configSpawner] as SpawnSpawner;

		// spawner type
		GUIContent[] spawnerTypes = new GUIContent[2];
		spawnerTypes[0] = new GUIContent("Time");
		spawnerTypes[1] = new GUIContent("Kill");
		spawner.type = EditorGUILayout.Popup(new GUIContent("Type", "Timed: spawns enemies at the set Rate\nKill: spawned enemy must be killed in order to proceed onto the next enemy"), spawner.type, spawnerTypes);

		// endless
		spawner.endless = EditorGUILayout.Toggle(new GUIContent("Endless", "Will continuously spawn enemies until stopped by a script"), spawner.endless);

		// show options depending on type
		if ( spawner.type == 0 ) // show Timed options
		{
			spawner.rate = EditorGUILayout.FloatField(new GUIContent("Rate", "The timed rate at which enemies will be spawned"), spawner.rate);
			spawner._rate = spawner.rate;
			spawner.initDelay = EditorGUILayout.FloatField(new GUIContent("Initial Delay", "The amount of time before the first enemy is spawned"), spawner.initDelay);
			spawner._initDelay = spawner.initDelay;
		}
		else if ( spawner.type == 1 ) // show Kill options
		{
			spawner.rate = EditorGUILayout.FloatField(new GUIContent("Delay Between Spawns", "The amount of time between spawns"), spawner.rate);
			spawner._rate = spawner.rate;
		}

		// list the spawners
		GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
		GUIContent[] spawnerNames = new GUIContent[spawners.Length];
		int spawnerSelected;

		for ( int i = 0; i < spawners.Length; i++ )
			spawnerNames[i] = new GUIContent(spawners[i].name);

		// random
		spawner.isRandom = EditorGUILayout.Toggle(new GUIContent("Random Spawners?", "If you want the enemies to spawn at random Spawners"), spawner.isRandom);

		int count = spawner.isRandom ? spawner.objects.Count : 1;

		// fix
		if ( spawner.objects.Count == 0 )
			spawner.objects.Add(null);

		// shwo the spawner objects
		for ( int i = 0; i < count; i++ )
		{
			spawnerSelected = 0;

			if ( spawner.isRandom )
				for ( int j = 0; j < spawners.Length; j++ )
					spawnerSelected = spawner.objects[i].transform == spawners[j].transform ? j : spawnerSelected;

			GUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(i == 0 ? "Spawner" : "");
				spawner.objects[i] = spawners[EditorGUILayout.Popup(spawnerSelected, spawnerNames)].transform;

				// only show if it was random
				if ( spawner.isRandom && count > 1 )
				{
					if ( GUILayout.Button("-") )
					{
						spawner.objects.RemoveAt(i);
						break;
					}
				}
			GUILayout.EndHorizontal();
		}

		// only show if it was random
		if ( spawner.isRandom )
		{
			GUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("");
				if ( GUILayout.Button("Add Random Spawner") )
					spawner.objects.Add(spawners[0].transform);
			GUILayout.EndHorizontal();
		}

		// some space
		EditorGUILayout.Space();
		EditorGUILayout.Space();

		// add/remove enemies
		GUILayout.BeginHorizontal();
			if ( GUILayout.Button("Add Enemy") )
			{
				spawner.enemies.Add(new SpawnEnemy());
				configEnemy = spawner.enemies.Count - 1;
			}
			if ( GUILayout.Button("Remove Enemy") && spawner.enemies.Count > 0 )
			{
				spawner.enemies.RemoveAt(configEnemy);
				configEnemy = spawner.enemies.Count - 1;
			}
		GUILayout.EndHorizontal();

		// don't continue editing
		if ( spawner.enemies.Count == 0 )
		{
			configEnemy = 0;
			return;
		}

		// make the list of enemies
		string[] enemyList = new string[spawner.enemies.Count];
		for ( int i = 0; i < spawner.enemies.Count; i++ )
			enemyList[i] = "Enemy " + (i + 1).ToString() + " (of " + spawner.enemies.Count.ToString() + ")";

		configEnemy = EditorGUILayout.Popup("Config Enemy", configEnemy, enemyList);

		if ( configEnemy > spawner.enemies.Count - 1 )
			configEnemy = 0;


		// the enemy we are editing
		SpawnEnemy enemy = spawner.enemies[configEnemy] as SpawnEnemy;

		// enemy repeat
		enemy.repeat = EditorGUILayout.IntField(new GUIContent("Repeat", "The number of times to repeat this enemy"), enemy.repeat);
		enemy._repeat = enemy.repeat;

		// quick fix
		if ( enemy.objects.Count == 0 )
			enemy.objects.Add(null);

		// random
		enemy.isRandom = EditorGUILayout.Toggle(new GUIContent("Random Enemies?", "If you want the enemies to spawn to be random"), enemy.isRandom);

		// count var is already instantiated from above
		count = enemy.isRandom ? enemy.objects.Count : 1;

		// show the enemy
		for ( int i = 0; i < count; i++ )
		{
			GUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(i == 0 ? "Enemy" : "");
				enemy.objects[i] = EditorGUILayout.ObjectField(enemy.objects[i], typeof(Transform), true) as Transform;

				// only show if it was random
				if ( enemy.isRandom && count > 1 )
				{
					if ( GUILayout.Button("-") )
					{
						enemy.objects.RemoveAt(i);
						break;
					}
				}
			GUILayout.EndHorizontal();
		}

		// only show if it was random
		if ( enemy.isRandom )
		{
			GUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("");
				if ( GUILayout.Button("Add Random Enemy") )
					enemy.objects.Add(null);
			GUILayout.EndHorizontal();
		}

		// some space
		EditorGUILayout.Space();

		// enemy send message(s)
		EditorGUILayout.BeginHorizontal();
			GUILayout.Label("Send Messages (" + enemy.sendMessages.Count.ToString() + ")");
			if ( GUILayout.Button("Add") )
			{
				enemy.sendMessages.Add("");
				enemy.sendMessagesValues.Add("");
			}
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
			GUILayout.Label(new GUIContent("Name", "The name of the function to execute"), GUILayout.Width(120));
			GUILayout.Label(new GUIContent("Value", "The value to pass to the function (will be passed as a String)"), GUILayout.Width(120));
			GUILayout.Label(" ");
		EditorGUILayout.EndHorizontal();

		for ( int i = 0; i < enemy.sendMessages.Count; i++ )
		{
			EditorGUILayout.BeginHorizontal();
				enemy.sendMessages[i] = GUILayout.TextField(enemy.sendMessages[i], GUILayout.Width(120));
				enemy.sendMessagesValues[i] = GUILayout.TextField(enemy.sendMessagesValues[i], GUILayout.Width(120));
				if ( GUILayout.Button("Remove") )
				{
					enemy.sendMessages.RemoveAt(i);
					enemy.sendMessagesValues.RemoveAt(i);
				}
			EditorGUILayout.EndHorizontal();
		}


		// save it
		if ( GUI.changed )
			EditorUtility.SetDirty(target);
	}

	public void OnSceneGUI()
	{
		if ( Application.isPlaying )
			return;

		if ( sc == null )
			return;

		if ( sc.waves.Count == 0 )
			return;

		// the wave we are editing
		SpawnWave wave = sc.waves[configWave] as SpawnWave;

		if ( wave.spawners.Count == 0 )
			return;

		// the spawner we are editing
		SpawnSpawner spawner = wave.spawners[configSpawner] as SpawnSpawner;

		for ( int i = 0; i < spawner.objects.Count; i++ )
			Handles.PositionHandle((spawner.objects[i] as Transform).position, Quaternion.identity);
	}
}
