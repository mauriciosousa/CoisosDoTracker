using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableStalker : MonoBehaviour {

    public Transform userProjection;
    public Transform tableCenter;
    public GameObject table;

    private SurfaceCalib calib;

    // Use this for initialization
    void Start ()
    {
        calib = table.GetComponent<SurfaceCalib>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Position
        transform.position = userProjection.position;
        transform.localPosition = new Vector3(
            Mathf.Clamp(transform.localPosition.x, -calib.Width * 0.5f + 0.2f, calib.Width * 0.5f - 0.2f), 
            Mathf.Clamp(transform.localPosition.y, -calib.Height * 0.5f + 0.2f, calib.Height * 0.5f - 0.2f), 
            0);

        // Orientation
        if (transform.position != userProjection.position)
        {
            Vector3 userVector = (transform.localPosition - userProjection.localPosition).normalized;
            transform.localRotation = Quaternion.AngleAxis(Vector3.Angle(Vector3.up, userVector), Vector3.Cross(Vector3.up, userVector));
        }
    }
}
