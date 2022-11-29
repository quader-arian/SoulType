using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class MonsterRoomKeypad : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    public AudioClip clip2;
    int counter = 0;
    public bool isNotAt = true;

    [SerializeField] private Transform ePrompt;
    [SerializeField] private Transform message1;
    [SerializeField] private Transform message2;
    [SerializeField] private Transform message3;

    [SerializeField] private Animator myDoor = null;


    public bool opened = false;




UnityEvent onInteract;


    private void Update()
    {


        if (!isNotAt)

        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (counter == 0)
                {
                    counter++;
                    message3.gameObject.SetActive(false);
                    message2.gameObject.SetActive(false);
                    message1.gameObject.SetActive(true);



                }

                else if (counter == 1)
                {
                    counter++;
                    message3.gameObject.SetActive(false);
                    message1.gameObject.SetActive(false);
                    message2.gameObject.SetActive(true);




                }

                else if (counter == 2)
                {
                    counter++;
                    message1.gameObject.SetActive(false);
                    message2.gameObject.SetActive(false);
                    message3.gameObject.SetActive(true);


                }

                else if (counter == 3)
                {

                    if (!opened)
                    {
                        counter = 0;
                        myDoor.Play("DoorSlide", 0, 0.0f);
                        source.PlayOneShot(clip);
                        opened = true;
                        source.PlayOneShot(clip2);

                    }
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
            message2.gameObject.SetActive(false);
            message3.gameObject.SetActive(false);




        }

    }





}


