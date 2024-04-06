using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pepper : CanBeMove
{
    private Rigidbody2D rb_pepper;

    // public float FallSpeed = 8f;

    //public string States;

    // public SnakeHead SnakeHead;

    // public LayerMask detectLayer;

    // Start is called before the first frame update
    void Start()
    {
        States = "Existing";
        rb_pepper = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        JudgeFalling();
    }
}