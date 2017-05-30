using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerMaster : MonoBehaviour {

    public GameObject stalkingGO;

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
                TableStalker stalker = hit.collider.gameObject.GetComponent<TableStalker>();

                if (stalkingGO == null)
                {
                    stalker.stalking = true;
                    stalker.returning = false;
                    stalkingGO = hit.collider.gameObject;
                }
                else if(stalkingGO == hit.collider.gameObject)
                {
                    stalker.stalking = false;
                    stalker.returning = true;
                    stalkingGO = null;
                }
                else
                {
                    stalker.stalking = true;
                    stalker.returning = false;

                    stalker = stalkingGO.GetComponent<TableStalker>();
                    stalker.stalking = false;
                    stalker.returning = true;

                    stalkingGO = hit.collider.gameObject;
                }
            }
        }	
	}
}
