using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    // Use this for initialization
    
    bool initialized = false;
    public GameObject[] replacementAsteroids;
   
 
    // Update is called once per frame
    void Update () {
        Vector3 b = Camera.main.OrthographicBounds().size;
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, 0);
        this.transform.up = Camera.main.transform.up;
        if (!initialized)
        {
            Vector3 iniForce = Camera.main.transform.up* Random.Range(-3.0f, 3.0f) + Camera.main.transform.right* Random.Range(-3.0f, 3.0f);
            this.transform.GetComponent<Rigidbody>().AddForce(iniForce);
            initialized = true;
        }

        Vector3 mb = GetComponent<SpriteRenderer>().bounds.size;
        // Teleport the game object
        if (transform.localPosition.x -mb.x/2 > b.x/2.0f)
        {
            transform.localPosition = new Vector3((-b.x / 2.0f) -mb.x/2 +0.01f, transform.position.y, 0);
            Debug.Log("1");
        }
        else if (transform.localPosition.x + mb.x/2 < -b.x / 2.0f)
        {
            transform.localPosition = new Vector3((b.x / 2.0f) + mb.x / 2 - 0.01f, transform.position.y, 0);
            Debug.Log("2");
        }

        else if (transform.localPosition.y -mb.y/2 > b.y / 2.0f)
        {
            transform.localPosition = new Vector3(transform.position.x, (-b.y / 2.0f) - mb.y / 2 + 0.01f, 0);
            Debug.Log("3");
        }

        else if (transform.localPosition.y+mb.y/2 < -b.y / 2.0f)
        {
            transform.localPosition = new Vector3(transform.position.x, (b.y / 2.0f) + mb.y / 2 - 0.01f, 0);
            Debug.Log("4");
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        Debug.Log("TEST");
        if (coll.gameObject.tag == "bullet")
        {
            int id1 = Random.Range(0, 3);
            int id2 = Random.Range(0, 3);
            Object.Destroy(coll.gameObject);
            Object.Destroy(this.gameObject);
            if (replacementAsteroids.Length != 0)
            {
                float rad =  gameObject.GetComponent<SphereCollider>().radius;
                Vector3 posa = new Vector3(transform.position.x + rad/2, transform.position.y + rad/2, transform.position.z) ;
                Vector3 posb = new Vector3(transform.position.x - rad/2, transform.position.y - rad/2, transform.position.z);
                //bool spawna = false;
                //bool spawnb = false;
                //if (!Physics2D.OverlapCircle(posa, rad / 2)) spawna = true;
                //if (!Physics2D.OverlapCircle(posb, rad / 2)) spawnb = true;

                //if(spawna) 
                GameObject a = Instantiate<GameObject>(replacementAsteroids[id1],posa,this.transform.rotation);
                //if(spawnb) 
                GameObject b = Instantiate<GameObject>(replacementAsteroids[id2], posb, this.transform.rotation);
              
            }
         
        }
            

    }

    void OnCollisionStay2D(Collision2D collisionInfo)
    {
        //Transform other = collisionInfo.transform;
        //Vector3 difference = other.position - transform.position;
        //float distance = collisionInfo.collider.bounds.size.magnitude + GetComponent<Collider2D>().bounds.size.magnitude;
        //Debug.Log("YADA");
        //if(difference.magnitude < distance)
        //{
        //    this.transform.position += difference * distance;
        //}
    }

}
