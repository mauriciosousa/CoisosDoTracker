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
        transform.localRotation = Quaternion.AngleAxis(Vector3.Angle(-Vector3.forward, user.forward) * (Vector3.Cross(-Vector3.forward, user.forward).y < 0? 1.0f : -1.0f), Vector3.forward);//user.rotation;
        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.z, 0, 0);
    }
}
