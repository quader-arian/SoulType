using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class FirstKeypadOpen : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    public AudioClip clip2;
    public AudioClip error;


    public bool isNotAt = true;
    private bool opened = false;

    [SerializeField] private Transform ePrompt;
    [SerializeField] private Animator myDoor = null;
    [SerializeField] private Transform message1;

    public TutTerminal2 keyPadG;
    public bool keyPad1;



    UnityEvent onInteract;


    private void Update()
    {
        keyPad1 = keyPadG.GetKeypad();
        if (!isNotAt)

        {

            if (Input.GetKeyDown(KeyCode.E))
            {
           
                if (keyPad1 == true) 
                {

                    if (!opened)
                    {

                        myDoor.Play("DoorSlide", 0, 0.0f);
                        source.PlayOneShot(clip);
                        opened = true;
                        source.PlayOneShot(clip2);


                    }

             


                }

                else

                {
                    message1.gameObject.SetActive(true);
                    source.PlayOneShot(error);


                }




            }

        }
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")

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
            message1.gameObject.SetActive(false);





        }

    }






}

