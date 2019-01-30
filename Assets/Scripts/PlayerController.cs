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
	GameObject flashlight, pointer;
	Vector2 endPoint;
	float offset = 2;
	void Awake()
	{
		fTile = Resources.Load<GameObject>("flashlight");
		flashlight = transform.Find("flashlight").gameObject;
		layout = levelGen.layout;
		endPoint = levelGen.endPoint;
		//pointer = transform.Find("pointer").gameObject;
	}
	void Start()
	{
		
	}
	public int[,] layout;
	void Update()
	{

		input.x = Input.GetAxis("Horizontal");
		input.y = Input.GetAxis("Vertical");

		Vector3 diff = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		//if(diff.magnitude > 1)
			diff.Normalize();

		flashlight.transform.localPosition = new Vector3(diff.x, diff.y, 0) * offset;
		
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

		//pointer.transform.rotation = Quaternion.Euler(0f, 0f, rot_z2 - 90);

		//rot = (rot + (Time.deltaTime * Mathf.Clamp(input.magnitude, 0, 1)) * 20);
		//transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(rot) * 23);
		//transform.localScale = new Vector3(Mathf.Abs(Mathf.Cos(rot)) * 0.3f + 0.7f, 1, 1);

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
}
