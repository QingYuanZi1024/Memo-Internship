using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    private Rigidbody2D rb;
    public float FallSpeed = 8f;
    public string States;
    // Start is called before the first frame update
    void Start()
    {
        States = "Existing";
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(rb.bodyType);
        Debug.Log("123");
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.down * FallSpeed * Time.deltaTime);
        // Debug.Log("test01");
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
}
