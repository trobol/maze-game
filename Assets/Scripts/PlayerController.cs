using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	Vector2 input;
	Rigidbody2D rb;
	public float speed;
	float rot = 0;
	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	void Start()
	{

	}

	void Update()
	{
		input.x = Input.GetAxis("Horizontal");
		input.y = Input.GetAxis("Vertical");
		rot = (rot + (Time.deltaTime * Mathf.Clamp(input.magnitude, 0, 1)) * 20);
		transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(rot) * 23);
		transform.localScale = new Vector3(Mathf.Abs(Mathf.Cos(rot)) * 0.3f + 0.7f, 1, 1);
	}


	void FixedUpdate()
	{
		rb.velocity = input * speed;
	}
}
