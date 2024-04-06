using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : CanBeMove
{
    private Rigidbody2D rb_banana;

    // public float FallSpeed = 8f;

    //public string States;

    // public SnakeHead SnakeHead;

    // public LayerMask detectLayer;

    // Start is called before the first frame update
    void Start()
    {
        States = "Existing";
        rb_banana = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        JudgeFalling();
    }
}
