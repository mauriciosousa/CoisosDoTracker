using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ShapeType
{
    SQUARE,
    CIRCLE,
    TRIANGLE
}


public class Shape
{
    public ShapeType shape;
    public Color color;
    public Vector2 position;
    public bool show;
    public Shape(ShapeType s, Color c, Vector2 p)
    {
        shape = s;
        color = c;
        position = p;
    } 
}

public class PutThatThere : MonoBehaviour {

    private List<Shape> shapes;

    public Texture squareTex;
    public Texture circleTex;
    public Texture triangleTex;
    public Texture cursorTex;

    private Shape selectedShape = null;

    [Range(50, 200)]
    public int shapeSize;

    [Range(10, 100)]
    public int cursorSize;

    void Start ()
    {
        shapes = new List<Shape>();

	}
	
	void Update ()
    {
        Vector2 p = Input.mousePosition;

        


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("CREATE A YELLOW CIRCLE THERE");
            shapes.Add(new Shape(ShapeType.CIRCLE, Color.yellow, p));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("CREATE A CYAN TRIANGLE THERE");
            shapes.Add(new Shape(ShapeType.TRIANGLE, Color.cyan, p));
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("CREATE A MAGENTA SQUARE THERE");
            shapes.Add(new Shape(ShapeType.SQUARE, Color.magenta, p));
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("PUT THAT...");

            Vector2 pointing = Input.mousePosition;
            foreach (Shape s in shapes)
            {
                if (Vector2.Distance(pointing, s.position) < shapeSize / 2)
                {
                    Debug.Log("" + s.shape.ToString() + " " + s.color.ToString());
                    selectedShape = s;
                }
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log("There");
            if (selectedShape != null)
            {
                selectedShape.position = Input.mousePosition;
                selectedShape = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Debug.Log("CREATE A GREEN CIRCLE THERE.......SHIT");
            shapes.Add(new Shape(ShapeType.CIRCLE, Color.red, p));
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            shapes = new List<Shape>();
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            cursorSize = Clamp(cursorSize += 1, 10, 100);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            cursorSize = Clamp(cursorSize -= 1, 10, 100);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            shapeSize = Clamp(shapeSize += 1, 50, 200);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            shapeSize = Clamp(shapeSize -= 1, 50, 200);
        }

    }

    void OnGUI()
    {
        foreach (Shape shape in shapes)
        {
            Texture t = squareTex;
            if (shape.shape == ShapeType.CIRCLE)
            {
                t = circleTex;
            }
            else if (shape.shape == ShapeType.TRIANGLE)
            {
                t = triangleTex;
            }

            GUI.color = shape.color;
            GUI.DrawTexture(new Rect(shape.position.x - shapeSize / 2, Screen.height - shape.position.y - shapeSize / 2, shapeSize, shapeSize), t);
        }

        GUI.color = Color.white;
        GUI.DrawTexture(new Rect(Input.mousePosition.x - cursorSize / 2, Screen.height - Input.mousePosition.y - cursorSize / 2, cursorSize, cursorSize), cursorTex);
    }

    public static int Clamp(int value, int min, int max)
    {
        return (value < min) ? min : (value > max) ? max : value;
    }
}
