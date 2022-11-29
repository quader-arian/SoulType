using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class KeypadOpen : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    public AudioClip clip2;

    public bool isNotAt = true;

    [SerializeField] private Transform ePrompt;
    [SerializeField] private Animator myDoor = null;


    public bool opened = false;




UnityEvent onInteract;


    private void Update()
    {


        if (!isNotAt)

        {

            if (Input.GetKeyDown(KeyCode.E))
            {

  

                    if (!opened)
                    {

                        myDoor.Play("DoorSlide", 0, 0.0f);
                        source.PlayOneShot(clip);
                        opened = true;
                        source.PlayOneShot(clip2);

                    }

         




            }

        }


    }



        void OnTriggerEnter(Collider other)
        {

            if (other.gameObject.tag == "Player" && !opened)

            {


                ePrompt.gameObject.SetActive(true);
                isNotAt = false;



            }

        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")

            {

                ePrompt.gameObject.SetActive(false);
                isNotAt = true;
 

            }

        }



    
        

}


