using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneLoading : MonoBehaviour
{

    [SerializeField] private Transform loadZone;
    [SerializeField] private Transform loadWallZone;

    [SerializeField] private Transform unloadZone;
    [SerializeField] private Transform unloadWallZone;
  





    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")

        {

         
           
              
                loadZone.gameObject.SetActive(true);
                unloadZone.gameObject.SetActive(false);
                loadWallZone.gameObject.SetActive(true);
                unloadWallZone.gameObject.SetActive(false);
            




        }

    }

}
