﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	Vector2 input;

	public float speed;
	float rot = 0;
	public LevelGenerator levelGen;
	public Camera cam;
	GameObject fTile;
	GameObject[,] fTiles;
	List<GameObject> fTileList = new List<GameObject>();
	GameObject flashlight, pointer;
	Vector2 endPoint;
	void Awake()
	{
		fTile = Resources.Load<GameObject>("flashlight");
		flashlight = transform.Find("flashlight").gameObject;
		layout = levelGen.layout;
		endPoint = levelGen.endPoint;
		pointer = transform.Find("pointer").gameObject;
	}
	void Start()
	{
		BuildFlashlight();
	}
	public int[,] layout;
	void Update()
	{

		input.x = Input.GetAxis("Horizontal");
		input.y = Input.GetAxis("Vertical");

		Vector3 diff = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		diff.Normalize();

		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		flashlight.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
		rot = (rot + (Time.deltaTime * Mathf.Clamp(input.magnitude, 0, 1)) * 20);

		Vector3 add = new Vector3(input.x, input.y, 0) * speed,
		position = transform.position + add;

		if (layout[(int)(position.x * levelGen.resolution), (int)(transform.position.y * levelGen.resolution)] == 0)
		{
			add.x = 0;
		}
		if (layout[(int)(transform.position.x * levelGen.resolution), (int)(position.y * levelGen.resolution)] == 0)
		{
			add.y = 0;
		}
		transform.position += add;



		Vector3 diff2 = new Vector3(endPoint.x, endPoint.y, 0) - transform.position;
		diff.Normalize();
		float rot_z2 = Mathf.Atan2(diff2.y, diff2.x) * Mathf.Rad2Deg;
		pointer.transform.rotation = Quaternion.Euler(0f, 0f, rot_z2 - 90);
		//transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(rot) * 23);
		//transform.localScale = new Vector3(Mathf.Abs(Mathf.Cos(rot)) * 0.3f + 0.7f, 1, 1);

		for (int x = 0; x < fTiles.GetLength(1) / 2; x++)
		{
			bool a = true;
			for (int y = 0; y < fTiles.GetLength(0); y++)
			{

				GameObject b = fTiles[y, x];
				if (b == null) continue;
				if (a)
				{
					a = layout[(int)(b.transform.position.x * levelGen.resolution), (int)(b.transform.position.y * levelGen.resolution)] == 1;
				}
				b.SetActive(a);
			}
		}
		for (int x = fTiles.GetLength(1) / 2; x < fTiles.GetLength(1); x++)
		{
			bool a = true;
			for (int y = 0; y < fTiles.GetLength(0); y++)
			{

				GameObject b = fTiles[y, x];
				if (b == null) continue;
				if (b.transform.position.x > 0 && b.transform.position.x < fTiles.GetLength(1) && b.transform.position.y > 0 && b.transform.position.y < fTiles.GetLength(0))
				{
					a = layout[(int)(b.transform.position.x * levelGen.resolution), (int)(b.transform.position.y * levelGen.resolution)] == 1;
				}
				else
				{
					a = false;
				}
				b.SetActive(a);
			}
		}

		/* 
		Vector2 pos = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.rotation = Quaternion.Euler(0, 0, -Mathf.Atan2(pos.x, pos.y) * (180.0f / Mathf.PI) + 270);
		*/

	}
	void LateUpdate()
	{
		cam.transform.position = transform.position - new Vector3(0, 0, 10);
	}

	void FixedUpdate()
	{

	}

	void BuildFlashlight()
	{

		Debug.Log("BUILD");
		int radius = 200,
		count = radius * radius;
		fTiles = new GameObject[radius, radius * 2];
		for (int y = 0; y < radius; y++)
		{
			for (int x = -radius; x < radius; x++)
			{


				// (x, y), (x, -y), (-x , y), (-x, -y) are in the circle
				GameObject b = Instantiate(fTile, flashlight.transform, false);
				b.transform.localPosition = new Vector3((float)x / 100f, (float)y / 100f, 0);
				fTiles[y, radius + x] = b;


			}
		}
	}
}
