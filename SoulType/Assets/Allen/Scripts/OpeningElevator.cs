using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningElevator : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    [SerializeField] private Animator myDoor = null;
    [SerializeField] private Transform loadZone2;
    [SerializeField] private Transform loadZone3;
    [SerializeField] private Transform loadWallZone2;
    [SerializeField] private Transform loadWallZone3;
  


    private bool opened = false;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")

        {

            if (!opened)
            {

                myDoor.Play("DoorSlide1", 0, 0.0f);
                source.PlayOneShot(clip);
                opened = true;
              
                loadZone2.gameObject.SetActive(false);
                loadZone3.gameObject.SetActive(false);
                loadWallZone2.gameObject.SetActive(false);
                loadWallZone3.gameObject.SetActive(false);
            }




        }

    }

}
