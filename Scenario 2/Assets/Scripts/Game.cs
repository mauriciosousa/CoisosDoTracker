using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public GameObject asteroidBundle = null;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp("space"))
        {
            Instantiate(asteroidBundle, this.transform.position, this.transform.rotation, this.transform.parent);
        }	
	}
}
