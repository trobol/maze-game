using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	public int height, width;
	GameObject tile;
	public void Gen() {
		Transform t = gameObject.transform.Find("Tiles");
		if(t != null) {
			Destroy(t.gameObject);
		}
		GameObject tiles = new GameObject("Tiles");
		tiles.transform.parent = gameObject.transform;
		for(int x = 0; x < width; x++) {
			for(int y = 0; y < height; y++) {
				Instantiate(tile, tiles, new Vector3(x, y, 0));
			}
		}
		Debug.Log("Generate");
	}
	public void Load() {
		tile = Resources.Load<GameObject>("tile");
	}
}
