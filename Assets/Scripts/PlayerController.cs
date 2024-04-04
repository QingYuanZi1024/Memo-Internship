using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class PlayerController : MonoBehaviour
{
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

    // Direction of the snake's head
    public Vector2 SnakeHeadDirection;

    // Direction of the snake's movement
    public Vector2 SnakeMoveDirection;

    public List<Sprite> SnakeTail;

    public LayerMask detectLayer;

    // Start is called before the first frame update
    void Start()
    {
        States = "Existing";
        InitSnakeHead();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void InitSnakeHead()
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

    void GetKeyValue()
    {
        Vector2 MaybeMoveDirection = MoveJudge();

    }


    private Vector2 MoveJudge()
    {
        if (States != "Existing")
            return Vector2.zero;
        else
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (SnakeHeadDirection == Vector2.down) 
                    return Vector2.zero;
                return Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (SnakeHeadDirection == Vector2.up) 
                    return Vector2.zero;
                return Vector2.down;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                if (SnakeHeadDirection == Vector2.right) 
                    return Vector2.zero;
                return Vector2.left;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (SnakeHeadDirection == Vector2.left) 
                    return Vector2.zero;
                return Vector2.right;
            }
            else
                return Vector2.zero;
        }
    }

    private void CrashJudge()
    {

    }

    public void UpdateHeadSprite(Vector2 direction)
    {
        if (States == "Existing")
        {
            if (direction == Vector2.up)
            {
                GetComponent<SpriteRenderer>().sprite = NormalSnakeHead[0];
            }
            if (direction == Vector2.down)
            {
                GetComponent<SpriteRenderer>().sprite = NormalSnakeHead[1];
            }
            if (direction == Vector2.left)
            {
                GetComponent<SpriteRenderer>().sprite = NormalSnakeHead[2];
            }
            if (direction == Vector2.right)
            {
                GetComponent<SpriteRenderer>().sprite = NormalSnakeHead[3];
            }
        }
        if (States == "Falling")
        {
            if (direction == Vector2.up)
            {
                GetComponent<SpriteRenderer>().sprite = FallSnakeHead[0];
            }
            if (direction == Vector2.down)
            {
                GetComponent<SpriteRenderer>().sprite = FallSnakeHead[1];
            }
            if (direction == Vector2.left)
            {
                GetComponent<SpriteRenderer>().sprite = FallSnakeHead[2];
            }
            if (direction == Vector2.right)
            {
                GetComponent<SpriteRenderer>().sprite = NormalSnakeHead[3];
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("test02");
        if (other.gameObject.tag == "GravityRange")
        {
            States = "Falling";
            // rb.bodyType = RigidbodyType2D.Dynamic;
            gameObject.layer = LayerMask.NameToLayer("-2");
            UpdateHeadSprite(SnakeHeadDirection);
        }

        if (other.gameObject.tag == "Boundary")
            Destroy(gameObject);
    }
}
