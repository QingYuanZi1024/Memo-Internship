using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 0 represents UP, 1 represents DOWN, 2 represnts LEFT, 3 represents RIGHT
    // Texture of the snake's head
    public List<Sprite> NormalSnakeHead;
    // Direction of the snake's head
    public Vector2 SnakeHeadDirection;
    // Direction of the snake's movement
    public Vector2 SnakeMoveDirection;

    public List<Sprite> SnakeTail;

    // Start is called before the first frame update
    void Start()
    {
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
}
