using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rand_plant : MonoBehaviour {

	public int SpriteNr = 3;

	private int rand = 1;

	// Use this for initialization
	void Start () {
		rand = Random.Range(1,SpriteNr);


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
