using UnityEngine;
using System.Collections;

public class WavePoewrUpManager : MonoBehaviour 
{
	public float stealthTime = 5.0f;

	private GameObject player;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		player.GetComponent<HeroManager>().stealthEnabled = true;
	}

	IEnumerator DisableStealth()
	{
		yield return new WaitForSeconds(stealthTime);
		player.GetComponent<HeroManager>().stealthEnabled = false;
	}
}
