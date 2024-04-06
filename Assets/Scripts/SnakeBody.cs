using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    // public string States; // "Existing" "Falling" "Spraying"

    // public string Parts;   // "Body" "Cross" "Tail"

    // public List<Sprite> SnakeBody;

    public bool HangOrNot;

    public List<Sprite> SnakeBodyCorner;

    public List<Sprite> SnakeBodyStraight;

    // private Rigidbody2D rb;


    // public bool TailOrNot;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    public void UpdateSprite(Vector3 before, Vector3 next)
    {
        // Debug.Log("1234");
        if (((transform.TransformPoint(Vector3.zero) - next) == Vector3.left && (before - transform.TransformPoint(Vector3.zero)) == Vector3.down ) || ((transform.TransformPoint(Vector3.zero) - next) == Vector3.up && (before - transform.TransformPoint(Vector3.zero)) == Vector3.right))
        {
            GetComponent<SpriteRenderer>().sprite = SnakeBodyCorner[0];
        }
        if (((transform.TransformPoint(Vector3.zero) - next) == Vector3.down && (before - transform.TransformPoint(Vector3.zero)) == Vector3.right) || ((transform.TransformPoint(Vector3.zero) - next) == Vector3.left && (before - transform.TransformPoint(Vector3.zero)) == Vector3.up))
        {
            GetComponent<SpriteRenderer>().sprite = SnakeBodyCorner[1];
        }
        if (((transform.TransformPoint(Vector3.zero) - next) == Vector3.right && (before - transform.TransformPoint(Vector3.zero)) == Vector3.up) || ((transform.TransformPoint(Vector3.zero) - next) == Vector3.down && (before - transform.TransformPoint(Vector3.zero)) == Vector3.left))
        {
            GetComponent<SpriteRenderer>().sprite = SnakeBodyCorner[2];
        }
        if (((transform.TransformPoint(Vector3.zero) - next) == Vector3.up && (before - transform.TransformPoint(Vector3.zero)) == Vector3.left) || ((transform.TransformPoint(Vector3.zero) - next) == Vector3.right && (before - transform.TransformPoint(Vector3.zero)) == Vector3.down))
        {
            GetComponent<SpriteRenderer>().sprite = SnakeBodyCorner[3];
        }
        if ((transform.TransformPoint(Vector3.zero).y == before.y) && (transform.TransformPoint(Vector3.zero).y == next.y))
        {
            GetComponent<SpriteRenderer>().sprite = SnakeBodyStraight[0];
        }
        if ((transform.TransformPoint(Vector3.zero).x == before.x) && (transform.TransformPoint(Vector3.zero).x == next.x))
        {
            GetComponent<SpriteRenderer>().sprite = SnakeBodyStraight[1];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "GravityRange")
        {
            HangOrNot = true;
        }
        if (other.gameObject.tag == "Boundary")
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "GravityRange")
        {
            HangOrNot = false;
        }
    }
}
