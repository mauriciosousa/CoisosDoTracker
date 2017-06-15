using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CallType
{
    RECEIVING,
    CALLING,
    NONE
}

public enum PersonCalling
{
    A,
    B,
    NONE
}

public class ScreenDisplay : MonoBehaviour {

    public Transform screenCenter;

    public GameObject idle;
    public GameObject callingA;
    public GameObject receivingA;
    public GameObject callingB;
    public GameObject receivingB;

    public CallType callType = CallType.NONE;
    public PersonCalling person = PersonCalling.A;


    public float distance = 1.5f;


    public bool startRcv = false;

    void Start ()
    {
        screenCenter = null;
        hideAll();	
	}

    public void init()
    {
        if (screenCenter != null)
        {
            _centerObject(idle);
            _centerObject(callingA);
            _centerObject(receivingA);
            _centerObject(callingB);
            _centerObject(receivingB);
        }
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            callType = CallType.NONE;
            person = PersonCalling.A;
            hideAll();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            person = PersonCalling.A;   
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            person = PersonCalling.B;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            callType = CallType.RECEIVING;
            idle.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            callType = CallType.CALLING;
        }

        if (callType != CallType.NONE)
        {
            Human human = GameObject.Find("BodiesManager").GetComponent<BodiesManager>().human;

            if (callType == CallType.CALLING)
            {
                if (human == null || Vector3.Distance(screenCenter.position, human.body.Joints[BodyJointType.spineBase]) > distance)
                {
                    hideAll();
                    idle.SetActive(true);
                }
                else
                {
                    hideAll();
                    if (person == PersonCalling.A) callingB.SetActive(true);
                    else callingA.SetActive(true);
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // clear
                    hideAll();
                }
            }

            if (callType == CallType.RECEIVING)
            {
                if (!startRcv)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        startRcv = true;
                    }
                }

                if (startRcv)
                {
                    if (human != null && Vector3.Distance(screenCenter.position, human.body.Joints[BodyJointType.spineBase]) < distance)
                    {
                        hideAll();
                    }
                    else
                    {
                        if (person == PersonCalling.A) receivingA.SetActive(true);
                        else receivingB.SetActive(true);
                    }
                }
            }

        }
    }

    private void _centerObject(GameObject go)
    {
        go.transform.position = screenCenter.position;
        go.transform.forward = screenCenter.forward;
        go.transform.up = screenCenter.up;
        go.transform.right = screenCenter.right;
    }

    public void hideAll()
    {
        idle.SetActive(false);
        callingA.SetActive(false);
        callingB.SetActive(false);
        receivingA.SetActive(false);
        receivingB.SetActive(false);
    }
}
