using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Button3 : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;

    public bool isNotAt = true;

    [SerializeField] private Transform ePrompt;


    [SerializeField] private Transform buttonRed;
    [SerializeField] private Transform buttonGreen;
    [SerializeField] private Transform lightRed;
    [SerializeField] private Transform lightGreen;

    public bool pressed = false;

    public bool GetButton()
    {
        return pressed;



    }


    UnityEvent onInteract;


    private void Update()
    {
        if (!isNotAt && !pressed)

        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                buttonRed.gameObject.SetActive(false);
                buttonGreen.gameObject.SetActive(true);
                lightRed.gameObject.SetActive(false);
                lightGreen.gameObject.SetActive(true);

                source.PlayOneShot(clip);
                pressed = true;

           

            }

        }
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player" && !pressed)

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


