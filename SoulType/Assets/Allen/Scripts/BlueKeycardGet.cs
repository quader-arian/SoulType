using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class BlueKeycardGet : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    public AudioClip clip2;


    public bool isNotAt = true;

    [SerializeField] private Transform ePrompt;
    [SerializeField] private Transform message1;
    [SerializeField] private Transform cardinventorymess;
    [SerializeField] private Transform doorClose;
    [SerializeField] private Transform keyCard;

    [SerializeField] private Transform dead1;
    [SerializeField] private Transform dead2;
    [SerializeField] private Transform dead3;
    [SerializeField] private Transform dead4;
    [SerializeField] private Transform dead5;
    [SerializeField] private Transform dead6;


    [SerializeField] private Transform sol1;
    [SerializeField] private Transform sol2;
    [SerializeField] private Transform sol3;
    [SerializeField] private Transform sol4;
    [SerializeField] private Transform sol5;
    [SerializeField] private Transform sol6;



    [SerializeField] private Transform blackscreen;


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
                doorClose.gameObject.SetActive(false);


                dead1.gameObject.SetActive(false);
                dead2.gameObject.SetActive(false);
                dead3.gameObject.SetActive(false);
                dead4.gameObject.SetActive(false);
                dead5.gameObject.SetActive(false);
                dead6.gameObject.SetActive(false);


                sol1.gameObject.SetActive(true);
                sol2.gameObject.SetActive(true);
                sol3.gameObject.SetActive(true);
                sol4.gameObject.SetActive(true);
                sol5.gameObject.SetActive(true);
                sol6.gameObject.SetActive(true);


                source.PlayOneShot(clip);
                source.PlayOneShot(clip2);
                cardGet = true;


                {
                    StartCoroutine(Delay());
                }

                IEnumerator Delay()
                {
                    blackscreen.gameObject.SetActive(true);
                    yield return new WaitForSeconds(0.75f);
                    blackscreen.gameObject.SetActive(false);
                }



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


