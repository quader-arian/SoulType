using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class RedCardScanner : MonoBehaviour
{
    public AudioSource source;
    public AudioClip unlockingTerminal;
    public AudioClip terminalClick;
    public AudioClip vaultOpen;
    int counter = 0;

    public AudioClip error;

    public bool isNotAt = true;

    [SerializeField] private Transform ePrompt;
    [SerializeField] private Transform errormess;
    [SerializeField] private Transform vaultblockk;

    [SerializeField] private Transform cardinventorymessa;
    [SerializeField] private Animator myDoor = null;

    [SerializeField] private Transform screen1;

    [SerializeField] private Transform message1;
    [SerializeField] private Transform message2;
   


    public RedKeycardGet RedCard;
    public bool RedCard1;

    public bool opened = false;




UnityEvent onInteract;


    private void Update()
    {







        RedCard1 = RedCard.GetCard();


        if (!isNotAt)

        {

            if (Input.GetKeyDown(KeyCode.E))
            {


                if (RedCard1 == true)
                {

                    if (counter == 0)
                    {
                        counter++;
               
                        message2.gameObject.SetActive(false);
                        message1.gameObject.SetActive(true);
                        screen1.gameObject.SetActive(true);

                        source.PlayOneShot(unlockingTerminal);



                    }

                    else if (counter == 1)
                    {
                        counter++;
           
                        message1.gameObject.SetActive(false);
                        message2.gameObject.SetActive(true);
                        source.PlayOneShot(terminalClick);




                    }



                    else if (counter == 2)
                    {

                        if (!opened)
                        {
                            counter = 0;
                            myDoor.Play("Take 001", 0, 0.0f);
                            source.PlayOneShot(vaultOpen);
                            opened = true;
                         
                            cardinventorymessa.gameObject.SetActive(false);
                            vaultblockk.gameObject.SetActive(false);

                        }
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
                message1.gameObject.SetActive(false);
                message2.gameObject.SetActive(false);

        }

        }



    
        

}


