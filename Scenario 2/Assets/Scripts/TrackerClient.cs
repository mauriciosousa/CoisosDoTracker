using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class TrackerClient : MonoBehaviour {

	private Dictionary<string, Human> _humans;
    public List<GameObject> users;
    public List<GameObject> projectedUsers;

    public Transform floor;

	void Start () {
		_humans = new Dictionary<string, Human>();
        users = new List<GameObject>();
        projectedUsers = new List<GameObject>();

        for (int i = 0; i < 10; i++)
        {
            GameObject user = new GameObject();
            user.name = "User " + (i + 1);
            user.SetActive(false);
            users.Add(user);

            GameObject projectedUser = (GameObject) Instantiate(Resources.Load("Prefabs/ProjectedUser"), floor);
            projectedUser.name = "ProjectedUser " + (i + 1);
            projectedUser.GetComponent<FloorUser>().user = user.transform;
            //projectedUser.transform.parent = floor;
            projectedUser.SetActive(false);
            projectedUsers.Add(projectedUser);
        }
	}

	void Update () {
        
        int i = 0;

		foreach (Human h in _humans.Values)
		{
            users[i].SetActive(true);
            projectedUsers[i].SetActive(true);
            users[i].transform.position = h.body.Joints[BodyJointType.spineBase];
            users[i].transform.forward = h.Forward;
            i += 1;
        }

        for (; i < 10; i++)
        {
            users[i].SetActive(false);
            projectedUsers[i].SetActive(false);
        }
        
		_cleanDeadHumans();
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
			catch (Exception) { }
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
		//GUI.Label(new Rect(10, 10, 200, 35), "Number of users: " + _humans.Count);
	}
}
