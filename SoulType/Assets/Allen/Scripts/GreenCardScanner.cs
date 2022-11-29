using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GreenCardScanner : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    public AudioClip clip2;
    public AudioClip error;

    public bool isNotAt = true;

    [SerializeField] private Transform ePrompt;
    [SerializeField] private Transform errormess;
    [SerializeField] private Transform cardinventorymessa;
    [SerializeField] private Animator myDoor = null;


    public GreenKeycardGet GreenCard;
    public bool GreenCard1;

    public bool opened = false;




UnityEvent onInteract;


    private void Update()
    {







        GreenCard1 = GreenCard.GetCard();


        if (!isNotAt)

        {

            if (Input.GetKeyDown(KeyCode.E))
            {

                if (GreenCard1 == true)
                {

                    if (!opened)
                    {

                        myDoor.Play("DoorSlide", 0, 0.0f);
                        source.PlayOneShot(clip);
                        opened = true;
                        source.PlayOneShot(clip2);
                        cardinventorymessa.gameObject.SetActive(false);

                    }




                }

                else

                {
                    errormess.gameObject.SetActive(true);
                    source.PlayOneShot(error);



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
                errormess.gameObject.SetActive(false);

            }

        }



    
        

}


