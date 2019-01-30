using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager gm;
	public GameObject canvas;
	public GameObject player;
	public Level level;

	public LevelGenerator levelGenerator;
	void Awake()
	{
		//create static, non-level dependent game controller
		if (gm == null)
			gm = this;
		else if (gm != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);

	}
	void Start()
	{
		levelGenerator.Load();
		levelGenerator.Gen();
		player.transform.position = new Vector3(levelGenerator.points[0].x, levelGenerator.points[0].y, 0);
		player.SetActive(true);
	}
	public void Win()
	{
		Debug.Log("You WIN!!!!");
		canvas.SetActive(true);
	}
	public void PlayAgain()
	{
		Debug.Log("Again");
		canvas.SetActive(false);
		player.transform.position = Vector3.zero;
	}
	public void Quit()
	{
		Debug.Log("Quit");
		Application.Quit();
	}


}
