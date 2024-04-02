using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance
    {
        //return reference to private instance
        get
        {
            return instance;
        }
    }
    private static GameManager instance = null;

    public int PepperNumber = 0;

    public int BananaNumber = 0;

    //Is game paused
    public bool IsPaused = false;

    //Is player input allowed
    public bool InputAllowed = true;

    // Use this for initialization
    void Awake()
    {
        //Check if any existing instance of the class exists in the scene
        //If so, then destroy this instance
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }

        //Make this active and only instance
        instance = this;

        //Make game manager persistent
        DontDestroyOnLoad(gameObject);
    }
    //-----------------------------------------
}
