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
    private GameObject plane = null;

	private Dictionary<string, Human> _humans;

	void Start () {
		_humans = new Dictionary<string, Human>();
	}

	void Update () {

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
            Debug.DrawLine(_surface.SurfaceBottomLeft, _surface.SurfaceBottomRight);
            Debug.DrawLine(_surface.SurfaceBottomLeft, _surface.SurfaceTopLeft);
            Debug.DrawLine(_surface.SurfaceTopLeft, _surface.SurfaceTopRight);
            Debug.DrawLine(_surface.SurfaceTopRight, _surface.SurfaceBottomRight);

            Plane p = new Plane(_surface.SurfaceBottomLeft, _surface.SurfaceBottomRight, _surface.SurfaceTopRight);
            if (plane == null)
            {
                plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
                plane.name = "WALL";
                plane.transform.position = _surface.Center;
                plane.transform.rotation = _surface.Perpendicular;
            }
            

            Vector3 direction = rightHand.transform.position - head.transform.position;
            Ray ray = new Ray(head.transform.position, direction);

            float rayDistance;
            if (p.Raycast(ray, out rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);
                markerObject.transform.position = point;
                Debug.DrawLine(rightHand.transform.position, markerObject.transform.position, Color.red);



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
}
