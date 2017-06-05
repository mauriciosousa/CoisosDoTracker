using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutThatThere : MonoBehaviour {

    private List<GameObject> shapes;
    private Transform selectedShape = null;


    private TrackerClient tracker;

    public Transform wall;
    public Transform userHead;
    public Transform userHand;
    public Transform cursor;

    public float scale;

    void Start ()
    {
        tracker = GameObject.Find("TrackerClientGO").GetComponent<TrackerClient>();
        shapes = new List<GameObject>();
    }

    private void instantiateShape(string path, Vector3 position, float scaleMultiplier = 1.0f)
    {
        GameObject go = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/" + path), wall);
        go.transform.position = position;
        go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y, -shapes.Count * 0.001f);
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = new Vector3(scale * scaleMultiplier, scale * scaleMultiplier, 1.0f);
        shapes.Add(go);
    }

    void Update()
    {
        Plane plane = new Plane(wall.forward, wall.position);
        Ray ray = new Ray(userHand.position, userHand.position - userHead.position);
        float distance;

        if (plane.Raycast(ray, out distance))
        {
            Vector3 p = ray.GetPoint(distance);

            cursor.position = p;
            cursor.localPosition = new Vector3(cursor.localPosition.x, cursor.localPosition.y, -9.0f);
            cursor.gameObject.SetActive(true);
            
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("CREATE A YELLOW CIRCLE THERE");
                instantiateShape("YellowCircle", p);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("CREATE A CYAN TRIANGLE THERE");
                instantiateShape("CyanTriangle", p);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("CREATE A MAGENTA SQUARE THERE");
                instantiateShape("MagentaSquare", p);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Debug.Log("PUT THAT...");
                RaycastHit hitInfo;
                if(Physics.Raycast(ray, out hitInfo))
                {
                    selectedShape = hitInfo.collider.gameObject.transform;
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                Debug.Log("There");
                if (selectedShape != null)
                {
                    selectedShape.position = p;
                    selectedShape = null;
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                Debug.Log("CREATE A GREEN CIRCLE THERE.......SHIT");
                instantiateShape("GreenCircle", p, 3.0f);
            }
        }
        else
        {
            cursor.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach(GameObject go in shapes)
            {
                Destroy(go);
            }
            shapes.Clear();
        }
    }

    public static int Clamp(int value, int min, int max)
    {
        return (value < min) ? min : (value > max) ? max : value;
    }
}
