using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

	public Button level02Button, level03Button;
	int levelPassed;

	// Use this for initialization
	void Start()
	{
		
		levelPassed = PlayerPrefs.GetInt("LevelPassed");
		level02Button.interactable = false;
		level03Button.interactable = false;
		Debug.Log(levelPassed);
		switch (levelPassed)
		{
			case 1:
				level02Button.interactable = true;
				level02Button.transform.GetChild(1).gameObject.SetActive(false);
				break;
			case 2:
				level02Button.interactable = true;
				level03Button.interactable = true;
				level02Button.transform.GetChild(1).gameObject.SetActive(false);
				level03Button.transform.GetChild(1).gameObject.SetActive(false);
				break;
		}
	}

	public void levelToLoad(int level)
	{
		SceneManager.LoadScene(level);
	}

	public void resetPlayerPrefs()
	{
		level02Button.interactable = false;
		level03Button.interactable = false;

		PlayerPrefs.DeleteAll();
	}
}
