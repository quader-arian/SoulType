using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class SpellTerminalIce : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;


    public bool isNotAt = true;
    public bool unlocked = false;

    [SerializeField] private Transform ePrompt;
    [SerializeField] private Transform message1;
    [SerializeField] private Transform screenwhite;

 
    UnityEvent onInteract;


    private void Update()
    {
        if (!isNotAt && !unlocked)

        {

            if (Input.GetKeyDown(KeyCode.E))
            {

                screenwhite.gameObject.SetActive(false);
                
                source.PlayOneShot(clip);
                unlocked = true;

                //change code underneath
                PlayerStatsScript.iceUnlock = true;


                {
                    StartCoroutine(Delay());
                }

                IEnumerator Delay()
                {
                    message1.gameObject.SetActive(true);
                    yield return new WaitForSeconds(3);
                    message1.gameObject.SetActive(false);
                }

            }

        }
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player" && !unlocked)

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


