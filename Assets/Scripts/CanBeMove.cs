using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class CanBeMove : MonoBehaviour
{
    private Rigidbody2D rb;

    public float FallSpeed = 8f;

    public string States;

    public SnakeHead SnakeHead;

    public LayerMask detectLayer;

    // Start is called before the first frame update
    void Start()
    {
        States = "Existing";
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        JudgeFalling();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("test02");
        if (other.gameObject.tag == "GravityRange")
        {
            States = "Falling";
            // rb.bodyType = RigidbodyType2D.Dynamic;
            gameObject.layer = LayerMask.NameToLayer("-2");
        }

        if (other.gameObject.tag == "Boundary")
            Destroy(gameObject);
    }

    void JudgeFalling()
    {
        if (States == "Falling")
        {
            transform.Translate(Vector3.down * FallSpeed * Time.deltaTime);
        }
    }

    public string RecursionJudge(Vector3 MoveDirection)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, MoveDirection, 1f, detectLayer);
        // Debug.Log("hit");

        if (hit)
        {
            if (hit.collider.tag.Equals("Stone") || hit.collider.tag.Equals("Wood"))
            {
                return hit.collider.tag;
            }
            if (hit.collider.tag.Equals("Ice") || hit.collider.tag.Equals("Banana") || hit.collider.tag.Equals("Pepper"))
            {
                return RecursionJudge(MoveDirection);
            }
            else
            {
                return "canmove";
            }
        }
        else
        {
            return "canmove";
        }
    }
}