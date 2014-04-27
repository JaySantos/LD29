using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
		// specify which wave you want to start/play
		//SpawnController.PlayWave(int);

		// if possible, plays the next wave in the waves (current wave + 1 > n (waves setup in the spawn controller) = not going to happen)
		//SpawnController.NextWave();

		// if possible, plays the previous wave in the waves (current wave - 1 < 0 (first index) = not going to happen)
		//SpawnController.PreviousWave();

		// if "Play On Start" is not checked. Calling 'PlayWave' alone will not start the wave if it is paused
		//SpawnController.ResumePlay();

		// pause the wave
		//SpawnController.PausePlay();

		// adding callbacks for all the event types (beforeWave, inWave, betweenWaves, finishedLast)
		SpawnController.EventAdd(SpawnController.SpawnStates.beforeWave, beforeWave);
		SpawnController.EventAdd(SpawnController.SpawnStates.inWave, inWave);
		SpawnController.EventAdd(SpawnController.SpawnStates.betweenWaves, betweenWaves);
		SpawnController.EventAdd(SpawnController.SpawnStates.finishedLast, finishedLast);

		// removes the callback(s) with the specified state (beforeWave, inWave, betweenWaves, finishedLast)
		//SpawnController.EventRemove(SpawnStates.(state of wave));

		// gets the current wave
		//Debug.Log(SpawnController.currentWave)

		// gets the current spawn state
		//Debug.Log(SpawnController.spawnState)

		// gets the total number of waves
		//Debug.Log(SpawnController.wavesLength)
	}

	void OnGUI()
	{
		GUI.Box(new Rect(10, 10, Screen.width - 20, 25), "Click on the Enemies that spawn to Kill them");

		string msg = "";
		Rect rec = new Rect(10, Screen.height - 35, Screen.width - 20, 25);

		if ( SpawnController.currentWave == 0 )
			msg = "Kill all the enemies that spawn from the RIGHT side";
		else if ( SpawnController.currentWave == 1 )
			msg = "Kill all the enemies that spawn from the LEFT side";
		else if ( SpawnController.currentWave == 2 )
			msg = "Kill all the enemies that spawn RANDOMLY from the LEFT and RIGHT side";
		else if ( SpawnController.currentWave == 3 )
			msg = "Will spawn enemies for 5 seconds then move onto the next wave";
		else if ( SpawnController.currentWave == 4 )
			msg = "This is the next wave... but an initial delay of 8 seconds";
		else if ( SpawnController.currentWave == 5 )
			msg = "Last Wave! Click him 5 times!";

		if ( SpawnController.spawnState == SpawnController.SpawnStates.finishedLast )
			msg = "Congratulations! I appreciate any and all feedback and suggestions!";

		GUI.Box(rec, msg);
	}

	void beforeWave()
	{
		Debug.Log("before wave");
	}
	void inWave()
	{
		Debug.Log("in wave");
	}
	void betweenWaves()
	{
		Debug.Log("between waves");
	}
	void finishedLast()
	{
		Debug.Log("finished last");
	}
}