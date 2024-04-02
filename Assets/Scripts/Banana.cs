using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    public string States;
    // Start is called before the first frame update
    void Start()
    {
        States = "Existing";
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D AnotherObj)
    {
        if (AnotherObj.gameObject.CompareTag("Boundary"))
            Destroy(gameObject);
    }

}
