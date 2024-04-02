using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class Stone : MonoBehaviour
{
    public string States;    // "Existing" "Falling"
    // Start is called before the first frame update
    void Start()
    {
        States = "Existing";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
