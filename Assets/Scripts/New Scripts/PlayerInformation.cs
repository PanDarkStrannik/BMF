using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation
{
    private static PlayerInformation instance;
    private static object synchRoot = new object();

    public PlayerParamController PlayerParamController
    {
        get; set;
    }

    public PlayerController PlayerController
    {
        get; set;
    }


    public GameObject Player
    {
        get; set;
    }


    protected PlayerInformation()
    {
        
    }


    public static PlayerInformation GetInstance()
    {
        lock (synchRoot)
        {
            if (instance == null)
            {
                instance = new PlayerInformation();
            }
        }
        return instance;
    }

    

}
