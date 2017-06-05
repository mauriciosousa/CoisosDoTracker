using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class TrackerClient : MonoBehaviour {


    public bool debug = false;

    public GameObject head;
    public GameObject rightHand;
    public GameObject markerObject;

    private SurfaceRectangle _surface = null;
    public SurfaceCalib plane;

	private Dictionary<string, Human> _humans;

    private bool calibrated = false;

    public Vector2 cursor = Vector2.zero;

    void Start () {
		_humans = new Dictionary<string, Human>();
	}

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            debug = !debug;
        }

        foreach (Human h in _humans.Values)
        {
            // get human properties:
            string id = h.id;
            string handLeftState = h.body.Properties[BodyPropertiesType.HandLeftState];

            // get human joints positions:
            head.transform.position = h.body.Joints[BodyJointType.head];
            rightHand.transform.position = h.body.Joints[BodyJointType.rightHand];
            break;
        }

        // finally
        _cleanDeadHumans();


        if (_surface != null)
        {
            Plane p = new Plane(_surface.SurfaceBottomLeft, _surface.SurfaceBottomRight, _surface.SurfaceTopRight);
            Debug.DrawLine(_surface.SurfaceBottomLeft, _surface.SurfaceBottomRight);
            Debug.DrawLine(_surface.SurfaceBottomLeft, _surface.SurfaceTopLeft);
            Debug.DrawLine(_surface.SurfaceTopLeft, _surface.SurfaceTopRight);
            Debug.DrawLine(_surface.SurfaceTopRight, _surface.SurfaceBottomRight);

            if (!calibrated)
            {


                plane.BL = (_surface.SurfaceBottomLeft);
                plane.BR = (_surface.SurfaceBottomRight);
                plane.TL = (_surface.SurfaceTopLeft);
                plane.TR = (_surface.SurfaceTopRight);
                plane.Calc();
                calibrated = true;
            }

            Vector3 direction = rightHand.transform.position - head.transform.position;
            Ray ray = new Ray(head.transform.position, direction);

            float rayDistance;
            if (calibrated && p.Raycast(ray, out rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);


                Vector3 px = ProjectPointLine(point, _surface.SurfaceTopLeft, _surface.SurfaceTopRight);
                Vector3 py = ProjectPointLine(point, _surface.SurfaceTopLeft, _surface.SurfaceBottomLeft);

                float x = (px - _surface.SurfaceTopLeft).magnitude / (_surface.SurfaceTopRight - _surface.SurfaceTopLeft).magnitude;
                float y = (py - _surface.SurfaceTopLeft).magnitude / (_surface.SurfaceBottomLeft - _surface.SurfaceTopLeft).magnitude;

                if (x != 0 && y != 0)
                {
                    Debug.Log("" + x + " " + y);
                }

                cursor.x = x;
                cursor.y = y;

                //Debug.DrawLine(rightHand.transform.position, markerObject.transform.position, Color.red);
                Debug.DrawLine(rightHand.transform.position, point, Color.red);
                markerObject.transform.position = point;// ProjectPointLine(point, _surface.SurfaceTopLeft, _surface.SurfaceTopRight);

            }
            else
            {
                markerObject.transform.position = Vector3.zero;
            }
        }
	}

    public void setNewFrame (Body[] bodies)
	{

		foreach (Body b in bodies)
		{
			try
			{
			    string bodyID = b.Properties[BodyPropertiesType.UID];
			    if (!_humans.Keys.Contains(bodyID))
			    {
				    _humans.Add(bodyID, new Human());
			    }
			    _humans[bodyID].Update(b);
			}
			catch (Exception e)
            {
                Debug.Log(e.Message);
                Debug.Log(e.StackTrace);
            }
		}
	}

    internal void setSurface(SurfaceRectangle surface)
    {
        if (_surface == null)
        {
            _surface = surface;
        }
    }

    void _cleanDeadHumans ()
	{
		List<Human> deadhumans = new List<Human>();

		foreach (Human h in _humans.Values)
		{
			if (DateTime.Now > h.lastUpdated.AddMilliseconds(1000))
				deadhumans.Add(h);
		}

		foreach (Human h in deadhumans)
		{
			_humans.Remove(h.id);
		}
	}

	void OnGUI()
	{
		if (debug) GUI.Label(new Rect(10, 10, 200, 35), "Number of users: " + _humans.Count);
	}

    public static float DistancePointLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
    {
        return Vector3.Magnitude(ProjectPointLine(point, lineStart, lineEnd) - point);
    }
    public static Vector3 ProjectPointLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
    {
        Vector3 rhs = point - lineStart;
        Vector3 vector2 = lineEnd - lineStart;
        float magnitude = vector2.magnitude;
        Vector3 lhs = vector2;
        if (magnitude > 1E-06f)
        {
            lhs = (Vector3)(lhs / magnitude);
        }
        float num2 = Mathf.Clamp(Vector3.Dot(lhs, rhs), 0f, magnitude);
        return (lineStart + ((Vector3)(lhs * num2)));
    }
}
