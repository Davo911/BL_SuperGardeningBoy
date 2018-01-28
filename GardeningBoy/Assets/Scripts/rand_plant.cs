using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rand_plant : MonoBehaviour {

	public int SpriteAnz = 3;

	private int rand = 1;
	private Animator anim;
	// Use this for initialization
	void Start () {
		rand = Random.Range(1,SpriteAnz);
		anim = GetComponent<Animator>();
		anim.SetInteger("rand", rand);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
