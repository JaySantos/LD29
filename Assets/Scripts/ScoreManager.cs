using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour 
{
	public GameObject scoreText;
	public GameObject comboText;
	public GameObject hpText;
	public GameObject gameOverText;
	public GameObject ammoText;
	public GameObject comboSlider;

	private bool countDownCombo;
	private float comboTimerInitValue = 1.0f;
	private float comboTimer;
	private WeaponManager wm;

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
			if (combo > 1.0f)
			{
				countDownCombo = true;
				comboTimer = comboTimerInitValue;
			}
		}
	}

	[SerializeField]
	int hp;
	
	public int HP
	{
		get
		{
			return hp;
		}
		
		set
		{
			hp = value;
		}
	}

	[SerializeField]
	int ammo;
	
	public int Ammo
	{
		get
		{
			return ammo;
		}
		set
		{
			ammo = value;
		}
	}

	// Use this for initialization
	void Start () 
	{
		wm = GameObject.Find("Player").GetComponent<WeaponManager>();
		Debug.Log("wm = " + wm);
	}

	void Update()
	{
		if (countDownCombo)
		{
			comboTimer -= Time.deltaTime;
			comboSlider.GetComponent<Slider>().value = comboTimer;
			if (comboTimer <= 0f)
			{
				combo = 1.0f;
				comboText.GetComponent<Text>().text = "Combo: " + combo + "x";
				countDownCombo = false;

			}
		}
	}
	
	void OnEnable()
	{
		countDownCombo = false;
		comboTimer = 1.0f;
		score = 0;
		combo = 1.0f;
		hp = 5;
		ammo = 0;
		gameOverText.GetComponent<Text>().text = "";
		UpdateScore();
	}

	public void UpdateScore()
	{
		scoreText.GetComponent<Text>().text = "Score: " + score;
		comboText.GetComponent<Text>().text = "Combo: " + combo + "x";
		hpText.GetComponent<Text>().text = "HP: " + hp;
		if (wm)
		{
			if (wm.weaponType == WeaponManager.WeaponType.DEFAULT)
			{
				ammoText.GetComponent<Text>().text = "Ammo: -";
			}
			else
			{
				ammoText.GetComponent<Text>().text = "Ammo: " + wm.ammo;
			}
		}
	}

	public void ShowGameOver()
	{
		scoreText.GetComponent<Text>().text = "";
		comboText.GetComponent<Text>().text = "";
		hpText.GetComponent<Text>().text = "";
		gameOverText.GetComponent<Text>().text = "GAME OVER!!!";
	}
}
