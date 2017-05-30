using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceCalib : MonoBehaviour {

    public Transform TL;
    public Transform TR;
    public Transform BL;
    public Transform BR;

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
    void Start ()
    {
        // Position and Orientation
        Vector3 up = TL.position - BL.position;
        Vector3 right = TR.position - TL.position;
        Vector3 center = (TL.position + BR.position) * 0.5f;

        transform.rotation = Quaternion.LookRotation(Vector3.Cross(right, up), up);
        transform.position = center;

        width = (TR.position - TL.position).magnitude;
        height = (TR.position - BR.position).magnitude;

        // Camera
        Camera.main.orthographicSize = (TL.position - BL.position).magnitude * 0.5f;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
