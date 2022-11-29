using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class HauntedDoorClose : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;


    [SerializeField] private Transform doorClose;
    public bool opened = false;




    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player" && !opened)

        {


            doorClose.gameObject.SetActive(true);
            source.PlayOneShot(clip);
            opened = true;

        }

    }

    

       
        

}


