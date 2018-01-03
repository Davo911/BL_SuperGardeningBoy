using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonDamage : MonoBehaviour {
    public float damage = 1;
	// Use this for initialization
	void Start () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.SendMessage("ApplyDamage", damage);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
