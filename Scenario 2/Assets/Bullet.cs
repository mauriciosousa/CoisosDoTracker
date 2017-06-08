using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float ttl = 2.0f;
    float myLife = 0.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        myLife += Time.deltaTime;
        if(myLife > ttl)
        {
            Object.Destroy(this.gameObject);
        }
	}


  
}
