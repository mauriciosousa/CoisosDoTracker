using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorUser : MonoBehaviour
{
    public Transform user;

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = user.position;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
        transform.rotation = user.rotation;
        transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);
    }
}
