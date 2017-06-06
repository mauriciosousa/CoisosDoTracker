using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerMaster : MonoBehaviour {

    public GameObject stalkingGO1;
    public GameObject stalkingGO2;

    public Transform user1;
    public Transform userProjection1;
    public Transform user2;
    public Transform userProjection2;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                Vector3 lh1, rh1, lh2, rh2;
                Hands hands;
                hands = user1.GetComponent<Hands>();
                lh1 = hands.leftHand;
                rh1 = hands.rightHand;
                hands = user2.GetComponent<Hands>();
                lh2 = hands.leftHand;
                rh2 = hands.rightHand;

                float dist1, dist2;
                dist1 = Mathf.Min(Vector3.Distance(lh1, hit.point), Vector3.Distance(rh1, hit.point));
                dist2 = Mathf.Min(Vector3.Distance(lh2, hit.point), Vector3.Distance(rh2, hit.point));

                if (dist1 < dist2)
                {
                    followUser(hit, ref stalkingGO1, ref userProjection1);
                }
                else
                {
                    followUser(hit, ref stalkingGO2, ref userProjection2);
                }
            }
        }	
	}

    private static void followUser(RaycastHit hit, ref GameObject stalkingGO, ref Transform userProjection)
    {
        TableStalker stalker = hit.collider.gameObject.GetComponent<TableStalker>();

        if (stalkingGO == null)
        {
            stalker.stalking = true;
            stalker.returning = false;
            stalkingGO = hit.collider.gameObject;
            stalkingGO.GetComponent<TableStalker>().userProjection = userProjection;
        }
        else if (stalkingGO == hit.collider.gameObject)
        {
            stalker.stalking = false;
            stalker.returning = true;
            stalkingGO.GetComponent<TableStalker>().userProjection = null;
            stalkingGO = null;
        }
        else
        {
            Transform currentUser = hit.collider.gameObject.GetComponent<TableStalker>().userProjection;
            if (currentUser != null && currentUser != userProjection)
                return;

            stalker.stalking = true;
            stalker.returning = false;
            stalker.userProjection = userProjection;

            stalker = stalkingGO.GetComponent<TableStalker>();
            stalker.stalking = false;
            stalker.returning = true;
            stalkingGO.GetComponent<TableStalker>().userProjection = null;

            stalkingGO = hit.collider.gameObject;
        }
    }
}
