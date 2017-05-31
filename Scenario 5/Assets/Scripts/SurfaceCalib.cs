using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceCalib : MonoBehaviour {

    public Vector3 TL;
    public Vector3 TR;
    public Vector3 BL;
    public Vector3 BR;

    private float width;
    private float height;

    public float Width
    {
        get
        {
            return width;
        }
    }

    public float Height
    {
        get
        {
            return height;
        }
    }

    // Use this for initialization
    public void Calc ()
    {
        // Position and Orientation
        Vector3 up = TL - BL;
        Vector3 right = TR - TL;
        Vector3 center = (TL + BR) * 0.5f;

        transform.rotation = Quaternion.LookRotation(Vector3.Cross(right, up), up);
        transform.position = center;

        width = (TR - TL).magnitude;
        height = (TR - BR).magnitude;

        // Camera
        Camera.main.orthographicSize = (TL - BL).magnitude * 0.5f;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
