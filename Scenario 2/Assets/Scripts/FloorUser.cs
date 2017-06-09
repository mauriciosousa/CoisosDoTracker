using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorUser : MonoBehaviour
{
    public Transform user;
    public int shootRate;
    public GameObject bullet =null;

    private float lastShot;
    // Use this for initialization
    void Start ()
    {
        lastShot = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {

        transform.position = user.position;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
        transform.localRotation = Quaternion.AngleAxis(Vector3.Angle(Vector3.forward, user.forward) * (Vector3.Cross(Vector3.forward, user.forward).y < 0 ? 1.0f : -1.0f), Vector3.forward);//user.rotation;
        //transform.Rotate(transform.up, 180);
        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.z, 0, 0);
        lastShot += Time.deltaTime;
        if(lastShot > 1.0f / shootRate)
        {
            shoot();
            lastShot = 0;
        }
    }

    void shoot()
    {
        Vector3 rot = transform.up;
        rot.Normalize();
        BoxCollider col = this.GetComponent<BoxCollider>();
        float sizey = col.bounds.size.y + col.center.y + 0.3f;
        Debug.Log(sizey);
        GameObject b = Instantiate(bullet, this.transform.position+sizey*rot,this.transform.rotation);
        b.transform.parent = this.transform.parent;
       
        b.GetComponent<Rigidbody>().AddForce(rot*10);
        
    }
}
