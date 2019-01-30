using System.Collections;
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
	GameObject[] fTiles;
	List<GameObject> fTileList = new List<GameObject>();
	void Awake()
	{
		fTile = Resources.Load<GameObject>("flashlight");

		layout = levelGen.layout;

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

		rot = (rot + (Time.deltaTime * Mathf.Clamp(input.magnitude, 0, 1)) * 20);
		transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(rot) * 23);
		transform.localScale = new Vector3(Mathf.Abs(Mathf.Cos(rot)) * 0.3f + 0.7f, 1, 1);

		for (int i = 0; i < fTiles.Length; i++)
		{
			GameObject b = fTiles[i];

			b.SetActive(layout[(int)(b.transform.position.x * levelGen.resolution), (int)(b.transform.position.y * levelGen.resolution)] == 1);
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
		int radius = 10,
		count = radius * radius;
		for (int y = 0; y < radius; y++)
		{
			for (int x = 0; x < radius; x++)
			{
				if (x < y)
				{

					// (x, y), (x, -y), (-x , y), (-x, -y) are in the circle
					GameObject[] a = {
						Instantiate(fTile, transform, false),
						Instantiate(fTile, transform, false)
					};
					a[0].transform.localPosition = new Vector3((float)x / 10f, (float)y / 10f, 0);
					a[1].transform.localPosition = new Vector3((float)x / -10f, (float)y / 10f, 0);
					for (int i = 0; i < 2; i++)
					{
						GameObject b = a[i];
						fTileList.Add(b);
					}
				}
			}
		}
		fTiles = fTileList.ToArray();
	}
}
