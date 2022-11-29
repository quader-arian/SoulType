using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonfirePositions : MonoBehaviour
{
    private static BonfirePositions instance;
    public Vector3 lastCheckPointPos; 

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);

        } else
        {
            Destroy(gameObject);
        }


    }







}
