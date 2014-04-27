using UnityEngine;
using System.Collections;

public class ScenesManager : MonoBehaviour 
{
	private GameObject mainMenuScene;
	private GameObject gameScene;
	private GameObject settingsScene;
	private GameObject aboutScene;
	// Use this for initialization
	void Start () 
	{
		mainMenuScene = GameObject.Find("MainMenu");
		gameScene = GameObject.Find("Game");
		settingsScene = GameObject.Find("Settings");
		aboutScene = GameObject.Find("About");

		gameScene.SetActive(false);
		settingsScene.SetActive(false);
		aboutScene.SetActive(false);
		//gameScene.SetActive(false);
		//settingsScene.SetActive(false);
		//aboutScene.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	public void StartGame()
	{
		gameScene.SetActive(true);
		mainMenuScene.SetActive(false);
		gameScene.GetComponent<GameManager>().StartGame();
	}
	
	public void Settings()
	{
	}
	
	public void About()
	{
		aboutScene.SetActive(true);
		mainMenuScene.SetActive(false);
	}

	public void BackToMainMenu()
	{
		gameScene.SetActive(false);
		settingsScene.SetActive(false);
		aboutScene.SetActive(false);
		mainMenuScene.SetActive(true);
	}
	
	public void Quit()
	{
		Application.Quit();
	}
}
