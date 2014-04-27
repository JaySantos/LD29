using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour 
{
	public GameObject scoreText;
	public GameObject comboText;
	[SerializeField]
	int score;
	
	public int Score
	{
		get
		{
			return score;
		}
		set
		{
			score = value;
		}
	}
	
	[SerializeField]
	float combo;
	
	public float Combo
	{
		get
		{
			return combo;
		}
		
		set
		{
			combo = value;
		}
	}
	
	// Use this for initialization
	void Start () 
	{
	}
	
	void OnEnable()
	{
		score = 0;
		combo = 1.0f;
		UpdateScore();
	}

	public void UpdateScore()
	{
		scoreText.GetComponent<Text>().text = "Score: " + score;
		comboText.GetComponent<Text>().text = "Combo: " + combo + "x";
	}
}
