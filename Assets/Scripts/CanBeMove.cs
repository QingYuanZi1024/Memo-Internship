using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.VersionControl.Asset;

public class CanBeMove : MonoBehaviour
{
    private Rigidbody2D rb;

    public float FallSpeed = 8f;

    public string States;  // Existing Falling Spraying

    //public SnakeHead SnakeHead;

    public LayerMask detectLayer;

    public SortingGroup sortingGroup;

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

    public void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("test02");
        if (other.gameObject.tag == "GravityRange")
        {
            States = "Falling";
            // rb.bodyType = RigidbodyType2D.Dynamic;
            gameObject.layer = LayerMask.NameToLayer("-2");
            sortingGroup = GetComponent<SortingGroup>();
            sortingGroup.sortingLayerName = "-2";
        }

        if (other.gameObject.tag == "Boundary")
            Destroy(gameObject);
    }

    public void JudgeFalling()
    {
        if (States == "Falling")
        {
            transform.Translate(Vector3.down * FallSpeed * Time.deltaTime);
        }
    }


    public bool ObjJudgeAndMove(Vector3 MoveDirection)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + 1f * MoveDirection, MoveDirection, 0.25f, detectLayer);
        Debug.Log("hit");
        if (!hit || hit.collider.tag.Equals("SandPit") || hit.collider.tag.Equals("FinalHole"))
        {
            Debug.Log("xxx");
            transform.Translate(MoveDirection);
            return true;
        }
        else
        {
            // Debug.Log("777");
            if (hit.collider.tag.Equals("Banana") || hit.collider.tag.Equals("Pepper") || hit.collider.tag.Equals("Ice"))
            {
                RaycastHit2D next_hit = Physics2D.Raycast(hit.transform.position, MoveDirection, 1f, detectLayer);

                if (!next_hit || next_hit.collider.tag.Equals("SandPit") || next_hit.collider.tag.Equals("FinalHole"))
                {
                    hit.collider.gameObject.transform.Translate(MoveDirection);
                    transform.Translate(MoveDirection);
                    return true;
                }
                else if (next_hit.collider.tag.Equals("Stone") || next_hit.collider.tag.Equals("Wood"))
                {
                    if (tag.Equals("Pepper") && hit.collider.tag.Equals("Ice"))
                    {
                        Destroy(gameObject);
                        Destroy(hit.collider.gameObject);
                        return true;
                    }
                    else if (tag.Equals("Banana"))
                    {
                        Destroy(gameObject);
                        GameObject go = GameObject.Find("SnakeHead");
                        SnakeHead sh = (SnakeHead)go.GetComponent(typeof(SnakeHead));
                        sh.States = "Happiness";
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            if (hit.collider.tag.Equals("Stone") || hit.collider.tag.Equals("Wood"))
            {
                if (tag.Equals("Pepper") && hit.collider.tag.Equals("Wood"))
                {
                    Destroy(gameObject);
                    Destroy(hit.collider.gameObject);
                    return true;
                }
                else if (tag.Equals("Pepper") && hit.collider.tag.Equals("Stone"))
                {

                }
                else if (tag.Equals("Banana"))
                {
                    Destroy(gameObject);
                    GameObject go = GameObject.Find("SnakeHead");
                    SnakeHead sh = (SnakeHead)go.GetComponent(typeof(SnakeHead));
                    sh.States = "Happiness";
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        // Debug.Log("zzz");
        return false;
    }
}