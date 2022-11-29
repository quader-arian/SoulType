using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BonfirePlayerPos : MonoBehaviour
{
    private BonfirePositions bf;



    void Start()
    {
        bf = GameObject.FindGameObjectWithTag("BF").GetComponent<BonfirePositions>();
        transform.position = bf.lastCheckPointPos;

    }


}
