using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour 
{
	public GameObject scoreText;
	public GameObject comboText;
	public GameObject hpText;
	public GameObject ammoText;
	public GameObject gameOverImage;
	//public GameObject comboSlider;

	private bool countDownCombo;
	private float comboTimerInitValue = 3.0f;
	private float comboTimer;
	private WeaponManager wm;
	private HeroManager hm;

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
		hm = GameObject.Find("Player").GetComponent<HeroManager>();
	}

	void Update()
	{
		if (countDownCombo)
		{
			comboTimer -= Time.deltaTime;
			//comboSlider.GetComponent<Slider>().value = comboTimer;
			if (comboTimer <= 0f)
			{
				combo = 1.0f;
				comboText.GetComponent<Text>().text = combo.ToString();
				countDownCombo = false;

			}
		}
	}
	
	void OnEnable()
	{
		countDownCombo = false;
		comboTimer = 1.5f;
		score = 0;
		combo = 1.0f;
		hp = 5;
		ammo = 0;
		UpdateScore();
	}

	public void RestartScore()
	{
		scoreText.GetComponent<Text>().text = "0";
		comboText.GetComponent<Text>().text = "1";
		hpText.GetComponent<Text>().text = "5";
		ammoText.GetComponent<Text>().text = "-";
		gameOverImage.SetActive(false);
		UpdateScore();
	}

	public void UpdateScore()
	{
		scoreText.GetComponent<Text>().text =  score.ToString();
		comboText.GetComponent<Text>().text = combo.ToString();
		if (hm)
		{
			hpText.GetComponent<Text>().text = hm.hitPoints.ToString();
		}
		if (wm)
		{
			if (wm.weaponType == WeaponManager.WeaponType.DEFAULT)
			{
				ammoText.GetComponent<Text>().text = "-";
			}
			else
			{
				ammoText.GetComponent<Text>().text = wm.ammo.ToString();
			}
		}
	}

	public void ShowGameOver()
	{
		//scoreText.GetComponent<Text>().text = "";
		//comboText.GetComponent<Text>().text = "";
		//hpText.GetComponent<Text>().text = "";
		//ammoText.GetComponent<Text>().text = "";
		gameOverImage.SetActive(true);
		GameObject.Find("GameOverScore").GetComponent<Text>().text = "Score:\n" + score.ToString();
	}
}
