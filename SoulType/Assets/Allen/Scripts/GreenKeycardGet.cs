using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GreenKeycardGet : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;

    public bool isNotAt = true;

    [SerializeField] private Transform ePrompt;
    [SerializeField] private Transform message1;
    [SerializeField] private Transform cardinventorymess;

    [SerializeField] private Transform keyCard;

  

    public bool cardGet = false;
    public bool GetCard()
    {
        return cardGet;



    }




UnityEvent onInteract;


    private void Update()
    {
        if (!isNotAt && !cardGet)

        {

            if (Input.GetKeyDown(KeyCode.E))
            {

            
                keyCard.gameObject.SetActive(false);

                message1.gameObject.SetActive(true);
                cardinventorymess.gameObject.SetActive(true);
                source.PlayOneShot(clip);
                cardGet = true;

           

            }

        }
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player" && !cardGet)

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


