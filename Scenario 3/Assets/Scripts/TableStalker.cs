using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableStalker : MonoBehaviour {

    public Transform userProjection;
    public GameObject table;

    private SurfaceCalib surface;

    public bool stalking;
    public bool returning;

    private Vector3 startingPos;
    private Quaternion startingRot;

    // Use this for initialization
    void Start ()
    {
        surface = table.GetComponent<SurfaceCalib>();

        stalking = false;
        returning = true;

        startingPos = transform.localPosition;
        startingRot = transform.localRotation;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 targetPosition = transform.localPosition;
        Quaternion targetRotation = transform.localRotation;

        if (stalking)
        {
            // Position
            targetPosition = new Vector3(
                Mathf.Clamp(userProjection.localPosition.x, -surface.Width * 0.5f + 0.2f, surface.Width * 0.5f - 0.2f),
                Mathf.Clamp(userProjection.localPosition.y, -surface.Height * 0.5f + 0.2f, surface.Height * 0.5f - 0.2f),
                0);

            // Orientation
            Vector3 up;
            if (targetPosition != userProjection.localPosition)
                up = (transform.localPosition - userProjection.localPosition).normalized;
            else
                up = (-userProjection.localPosition).normalized;
            Vector3 cross = Vector3.Cross(Vector3.up, up);
            targetRotation = Quaternion.AngleAxis(Vector3.Angle(Vector3.up, up) * (cross.z < 0 ? -1.0f : 1.0f), Vector3.forward);

            // To be on top
            targetPosition = new Vector3(targetPosition.x, targetPosition.y, -0.01f);
        }

        if(returning)
        {
            targetPosition = startingPos;
            targetRotation = startingRot;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, 0.3f);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, 0.3f);
    }
}
