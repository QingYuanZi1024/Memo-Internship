using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using static UnityEditor.ShaderData;
using static UnityEditor.VersionControl.Asset;

public class SnakeHead : MonoBehaviour
{
    public float FallSpeed = 8f;

    // Corresponding to four states
    // public bool Existing;
    public string States;  
    // public enum States { Existing, Spraying, Falling };
    // 0 represents UP, 1 represents DOWN, 2 represnts LEFT, 3 represents RIGHT
    // Texture of the snake's head

    public List<Sprite> NormalSnakeHead;  // 

    public List<Sprite> HappySnakeHead;  // eat banana

    public List<Sprite> SadSnakeHead;  // miss food

    public List<Sprite> FallSnakeHead;  // when falling

    public List<Sprite> SpraySnakeHead;  // when spraying

    public List<SnakeBody> SnakeBodies = new List<SnakeBody>();

    public SnakeTail SnakeTail;

    public GameObject SnakeSeqPrefab;

    public GameObject FireEffectPrefab;

    // Direction of the snake's head
    public Vector2 SnakeHeadDirection;

    // Direction of the snake's movement
    public Vector2 SnakeMoveDirection;

    // public List<Sprite> SnakeTail;

    public LayerMask detectLayer;

    private SortingGroup sortingGroup;

    public bool HangOrNot;

    // Start is called before the first frame update
    void Start()
    {
        States = "Existing";
        GetSnakeHead();
    }

    // Update is called once per frame
    void Update()
    {
        // GetKeyValue();

        MoveJudge();
        CrashJudgeAndMove();
        MoveAction();

        UpdateSprite();

        JudgeFalling();
        RealiseFalling();

    }


    void GetSnakeHead()
    {
        int dir = 0;
        for (int i = 0; i < NormalSnakeHead.Count; i++)
        {
            if (GetComponent<SpriteRenderer>().sprite == NormalSnakeHead[i])
                dir = i;
        }
        if (dir == 0)
            SnakeHeadDirection = Vector2.up;
        if (dir == 1)
            SnakeHeadDirection = Vector2.down;
        if (dir == 2)
            SnakeHeadDirection = Vector2.left;
        if (dir == 3)
            SnakeHeadDirection = Vector2.right;
    }



    private void MoveJudge()
    {
        if (States != "Existing")
            SnakeMoveDirection = Vector2.zero;
        else
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (SnakeHeadDirection != Vector2.down)
                    SnakeMoveDirection = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (SnakeHeadDirection != Vector2.up)
                    SnakeMoveDirection = Vector2.down;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                if (SnakeHeadDirection != Vector2.right)
                    SnakeMoveDirection = Vector2.left;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (SnakeHeadDirection != Vector2.left)
                    SnakeMoveDirection = Vector2.right;
            }
            else
                SnakeMoveDirection = Vector2.zero;
        }
    }

    private void CrashJudgeAndMove()
    {
        bool TempJudge;

        if (SnakeMoveDirection != Vector2.zero)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, SnakeMoveDirection, 1f, detectLayer);

            if (hit && (hit.collider.tag.Equals("Stone") || hit.collider.tag.Equals("Wood")))
            {
                SnakeMoveDirection = Vector2.zero;
            }
            if (hit && (hit.collider.tag.Equals("Banana") || hit.collider.tag.Equals("Pepper") || hit.collider.tag.Equals("Ice")))
            {
                // Debug.Log("666");
                TempJudge = hit.collider.GetComponent<CanBeMove>().ObjJudgeAndMove(SnakeMoveDirection);
                if (!TempJudge)
                {
                    SnakeMoveDirection = Vector2.zero;
                }
            }
        }
    }

    private void MoveAction()
    {
        //
        if(SnakeMoveDirection != Vector2.zero)
        {
            List<Vector3> TargetPos = new List<Vector3>() { transform.position };

            foreach (SnakeBody b in SnakeBodies)
            {
                TargetPos.Add(b.transform.position);
            }

            // Head Move

            transform.Translate(SnakeMoveDirection);

            // Body Move

            for (int i = 0; i < SnakeBodies.ToArray().Length; i++)
            {
                    SnakeBodies[i].transform.position = TargetPos[i];
            }

            // Tail Move

            SnakeTail.transform.position = TargetPos[SnakeBodies.ToArray().Length];

            //convert
            SnakeHeadDirection = SnakeMoveDirection;

            // reset
            SnakeMoveDirection = Vector2.zero;
        }

    }

    public void UpdateSprite()
    {
        UpdateHeadSprite();
        UpdateBodySprite();
        UpdateTailSprite();
    }


    public void UpdateHeadSprite()  // should send SnakeHeadDirection
    {
        if (States == "Existing")
        {
            if (SnakeHeadDirection == Vector2.up)
            {
                GetComponent<SpriteRenderer>().sprite = NormalSnakeHead[0];
            }
            if (SnakeHeadDirection == Vector2.down)
            {
                GetComponent<SpriteRenderer>().sprite = NormalSnakeHead[1];
            }
            if (SnakeHeadDirection == Vector2.left)
            {
                GetComponent<SpriteRenderer>().sprite = NormalSnakeHead[2];
            }
            if (SnakeHeadDirection == Vector2.right)
            {
                GetComponent<SpriteRenderer>().sprite = NormalSnakeHead[3];
            }
        }
        if (States == "Falling")
        {
            if (SnakeHeadDirection == Vector2.up)
            {
                GetComponent<SpriteRenderer>().sprite = FallSnakeHead[0];
            }
            if (SnakeHeadDirection == Vector2.down)
            {
                GetComponent<SpriteRenderer>().sprite = FallSnakeHead[1];
            }
            if (SnakeHeadDirection == Vector2.left)
            {
                GetComponent<SpriteRenderer>().sprite = FallSnakeHead[2];
            }
            if (SnakeHeadDirection == Vector2.right)
            {
                GetComponent<SpriteRenderer>().sprite = FallSnakeHead[3];
            }
        }
        if (States == "Spraying")
        {
            if (SnakeHeadDirection == Vector2.up)
            {
                GetComponent<SpriteRenderer>().sprite = SpraySnakeHead[0];
            }
            if (SnakeHeadDirection == Vector2.down)
            {
                GetComponent<SpriteRenderer>().sprite = SpraySnakeHead[1];
            }
            if (SnakeHeadDirection == Vector2.left)
            {
                GetComponent<SpriteRenderer>().sprite = SpraySnakeHead[2];
            }
            if (SnakeHeadDirection == Vector2.right)
            {
                GetComponent<SpriteRenderer>().sprite = SpraySnakeHead[3];
            }
        }
        if (States == "Happiness")
        {
            if (SnakeHeadDirection == Vector2.up)
            {
                GetComponent<SpriteRenderer>().sprite = HappySnakeHead[0];
            }
            if (SnakeHeadDirection == Vector2.down)
            {
                GetComponent<SpriteRenderer>().sprite = HappySnakeHead[1];
            }
            if (SnakeHeadDirection == Vector2.left)
            {
                GetComponent<SpriteRenderer>().sprite = HappySnakeHead[2];
            }
            if (SnakeHeadDirection == Vector2.right)
            {
                GetComponent<SpriteRenderer>().sprite = HappySnakeHead[3];
            }
        }
        if (States == "Sadness")
        {
            if (SnakeHeadDirection == Vector2.up)
            {
                GetComponent<SpriteRenderer>().sprite = SadSnakeHead[0];
            }
            if (SnakeHeadDirection == Vector2.down)
            {
                GetComponent<SpriteRenderer>().sprite = SadSnakeHead[1];
            }
            if (SnakeHeadDirection == Vector2.left)
            {
                GetComponent<SpriteRenderer>().sprite = SadSnakeHead[2];
            }
            if (SnakeHeadDirection == Vector2.right)
            {
                GetComponent<SpriteRenderer>().sprite = SadSnakeHead[3];
            }
        }
    }

    public void UpdateBodySprite()
    {
        //Debug.Log("asd");
        if (States == "Existing")
        {
            for (int i = 0; i < SnakeBodies.ToArray().Length; i++)
            {
                if (i == 0)
                {
                    SnakeBodies[i].UpdateSprite(transform.position, SnakeBodies[i + 1].transform.TransformPoint(Vector3.zero));
                }
                if (i > 0 && i < SnakeBodies.ToArray().Length - 1)
                {
                    SnakeBodies[i].UpdateSprite(SnakeBodies[i - 1].transform.TransformPoint(Vector3.zero), SnakeBodies[i + 1].transform.TransformPoint(Vector3.zero));
                }
                if (i == SnakeBodies.ToArray().Length - 1)
                {
                    SnakeBodies[i].UpdateSprite(SnakeBodies[i - 1].transform.TransformPoint(Vector3.zero), SnakeTail.transform.TransformPoint(Vector3.zero));
                }
            }
        }
    }

    public void UpdateTailSprite()
    {
        if (States == "Existing")
        {
            SnakeTail.UpdateSprite(SnakeBodies[SnakeBodies.ToArray().Length - 1].transform.position);
        } 
    }

    void JudgeFalling()
    {
        int tempcount = 0;

        if (HangOrNot && SnakeTail.HangOrNot)
        {
            foreach (SnakeBody b in SnakeBodies)
            {
                if (b.HangOrNot)
                {
                    tempcount++;
                }
            }
            if (tempcount == SnakeBodies.ToArray().Length && States != "Spraying")
            {
                States = "Falling";
                gameObject.layer = LayerMask.NameToLayer("-2");
                sortingGroup = GetComponent<SortingGroup>();
                sortingGroup.sortingLayerName = "-2";

                //gameObject.GetComponent<SortingGroup>.SortingLayer = LayerMask.NameToLayer("-2");
                // UpdateHeadSprite(SnakeHeadDirection);
            }
        }
    }

    void RealiseFalling()
    {
        if (States == "Falling")
        {
            transform.Translate(Vector3.down * FallSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("test02");
        if (other.gameObject.tag == "GravityRange")
        {
            // Debug.Log("test12345");
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
