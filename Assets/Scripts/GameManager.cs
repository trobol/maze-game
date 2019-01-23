using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager gm;
	public GameObject canvas;
	public GameObject player;
	void Awake()
	{
		//create static, non-level dependent game controller
		if (gm == null)
			gm = this;
		else if (gm != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);

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
