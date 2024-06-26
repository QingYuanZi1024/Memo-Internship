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

    public bool JudgeFalling()
    {
        if (States == "Falling")
        {
            transform.Translate(Vector3.down * FallSpeed * Time.deltaTime);
            return true;
        }
        return false;
    }


    public bool ObjJudgeAndMove(Vector3 MoveDirection)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + 1f * MoveDirection, MoveDirection, 0.25f, detectLayer);
        Debug.Log(hit);
        if (!hit || hit.collider.tag.Equals("SandPit") || hit.collider.tag.Equals("FinalHole"))
        {
            Debug.Log("xxx");
            transform.Translate(MoveDirection);
            return true;
        }
        else
        {
            if (hit.collider.tag.Equals("Stone") || hit.collider.tag.Equals("Wood"))
            {
                Debug.Log("nnn");
                if (tag.Equals("Pepper") && hit.collider.tag.Equals("Wood"))
                {
                    gameObject.SetActive(false);
                    Destroy(hit.collider.gameObject);
                    return true;
                }
                else if (tag.Equals("Pepper") && hit.collider.tag.Equals("Stone"))
                {

                }
                else if (tag.Equals("Banana"))
                {
                    GameObject go = GameObject.Find("SnakeHead");
                    SnakeHead snakeHead = (SnakeHead)go.GetComponent(typeof(SnakeHead));
                    snakeHead.States = "Happiness";
                    snakeHead.NeedToGrow = true;
                    Debug.Log("mmm");
                    Destroy(gameObject);
                    return true;
                }
                else
                {
                    Debug.Log("false");
                    return false;
                }
            }
            else
            {
                Debug.Log("false");
                return false;
            }
        }
        // Debug.Log("zzz");
        return false;
    }
}