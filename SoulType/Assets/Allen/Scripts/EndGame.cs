using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;


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
                

                {
                    StartCoroutine(Delay());
                }

                IEnumerator Delay()
                {
                    congratsmess.gameObject.SetActive(true);
                    yield return new WaitForSeconds(4);
                    SceneManager.LoadScene("Title");
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
 

            }

        }



    
        

}


