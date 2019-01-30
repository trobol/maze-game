using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager gm;
	public GameObject canvas;
	public GameObject player;
	public Level level;

	public float time = 0;
	public GameObject counter;
	public LevelGenerator levelGenerator;
	bool playing = false;
	void Awake()
	{
		//create static, non-level dependent game controller
		if (gm == null)
			gm = this;
		else if (gm != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);

	}
	public void StartGame()
	{
		Debug.Log("ACTIVE");
		canvas.SetActive(false);
		levelGenerator.Load();
		levelGenerator.Gen();
		player.transform.position = new Vector3(levelGenerator.points[0].x, levelGenerator.points[0].y, 0);
		player.SetActive(true);
		playing = true;
		time = 0;
	}
	public void Win()
	{
		playing = false;
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

	public void Update()
	{
		if (playing)
		{
			time += Time.deltaTime;
		}
		counter.GetComponent<Text>().text = time.ToString(); ;
	}
}
