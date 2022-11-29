using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ButtonDoorKeypad : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    public AudioClip clip2;
    public AudioClip error;


    public bool isNotAt = true;

    [SerializeField] private Transform ePrompt;
    [SerializeField] private Transform errormess;
  
    [SerializeField] private Animator myDoor = null;

    public Button1 Butt1;
    public bool Butt11;

    public Button2 Butt2;
    public bool Butt21;

    public Button3 Butt3;
    public bool Butt31;

    public bool opened = false;




UnityEvent onInteract;


    private void Update()
    {






        Butt11 = Butt1.GetButton();
        Butt21 = Butt2.GetButton();
        Butt31 = Butt3.GetButton();



        if (!isNotAt)

        {

            if (Input.GetKeyDown(KeyCode.E))
            {

                if (Butt11 == true && Butt21 == true && Butt31 == true)
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


