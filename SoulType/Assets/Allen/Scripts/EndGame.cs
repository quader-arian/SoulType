using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class EndGame : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
  

    public bool isNotAt = true;

    [SerializeField] private Transform ePrompt;
    [SerializeField] private Transform congratsmess;






UnityEvent onInteract;


    private void Update()
    {


        if (!isNotAt)

        {

            if (Input.GetKeyDown(KeyCode.E))
            {

  

                 
                source.PlayOneShot(clip);
                congratsmess.gameObject.SetActive(true);
                //go to main menu







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
 

            }

        }



    
        

}


