using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : CanBeMove
{
    private Rigidbody2D rb_ice;

    // public float FallSpeed = 8f;

    //public string States;

    // public SnakeHead SnakeHead;

    // public LayerMask detectLayer;

    // Start is called before the first frame update
    void Start()
    {
        States = "Existing";
        rb_ice = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        JudgeFalling();
    }
}
