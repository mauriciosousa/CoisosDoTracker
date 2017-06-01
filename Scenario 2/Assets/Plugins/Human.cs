using UnityEngine;
using System;
using System.Collections;

public class Human {

	private string _id;
	public string id { get { return _id; } }

	private Body _body;
	public Body body { get { return _body; } }

	private DateTime _lastUpdated;
	public DateTime lastUpdated { get { return _lastUpdated; } }

    private Vector3 _forward;
    public Vector3 Forward { get { return _forward; } }

    public Human()
	{
        _forward = Vector3.zero;
		_body = null;
		_id = null;
	}

	public void Update(Body newBody)
	{
		_body = newBody;
        _forward = calcForward();
		_id = _body.Properties[BodyPropertiesType.UID];
		_lastUpdated = DateTime.Now;
	}

    private Vector3 calcForward()
    {
        Vector3 right = _body.Joints[BodyJointType.rightShoulder] - _body.Joints[BodyJointType.spineShoulder];
        Vector3 up = _body.Joints[BodyJointType.spineShoulder] - _body.Joints[BodyJointType.spineMid];
        
        return Vector3.Cross(right, up);
    }
}
