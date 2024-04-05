using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTail : MonoBehaviour
{
    public bool HangOrNot;

    public List<Sprite> SnakeTailMode;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSprite(Vector3 before)
    {
        if ((before - transform.TransformPoint(Vector3.zero)) == Vector3.down)
        {
            GetComponent<SpriteRenderer>().sprite = SnakeTailMode[0];
        }
        if ((before - transform.TransformPoint(Vector3.zero)) == Vector3.up)
        {
            GetComponent<SpriteRenderer>().sprite = SnakeTailMode[1];
        }
        if ((before - transform.TransformPoint(Vector3.zero)) == Vector3.right)
        {
            GetComponent<SpriteRenderer>().sprite = SnakeTailMode[2];
        }
        if ((before - transform.TransformPoint(Vector3.zero)) == Vector3.left)
        {
            GetComponent<SpriteRenderer>().sprite = SnakeTailMode[3];
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
