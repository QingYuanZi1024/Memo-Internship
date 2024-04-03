using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{

    public float FallSpeed = 5f;
    public string States;
    // Start is called before the first frame update
    void Start()
    {
        States = "Existing";
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("test01");
        JudgeFalling();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 检测到进入触发器的物体是你要控制的物体
        if (other.CompareTag("GravityRange"))
        {
            States = "Falling";
        }
    }

    void JudgeFalling()
    {
        if (States == "Falling")
        {
            gameObject.layer = LayerMask.NameToLayer("-2");
            transform.Translate(Vector3.down * FallSpeed * Time.deltaTime);
        }
    }
}
