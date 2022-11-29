
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Bonfire : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    private bool bonfireactivated = false;
    public bool isNotAt = true;
    private BonfirePositions bf;



    [SerializeField] private Transform bonfiremessage;
    [SerializeField] private Transform bonfireoff;
    [SerializeField] private Transform bonfireon;
    [SerializeField] private Transform bonfirelitmessage;



    UnityEvent onInteract;

    void Start()
    {
        bf = GameObject.FindGameObjectWithTag("BF").GetComponent<BonfirePositions>();
    }

    private void Update()
    {
        if (!isNotAt)

        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (bonfireactivated)
                {


                    //open the level up menu


                    //replenish all HP and MP

                    //Disable all input from player



                }
                else
                {

                    bonfireoff.gameObject.SetActive(false);
                    bonfireon.gameObject.SetActive(true);
                    source.PlayOneShot(clip);
                    bonfireactivated = true;

                    bf.lastCheckPointPos = transform.position;




                    {
                        StartCoroutine(Delay());
                    }

                    IEnumerator Delay()
                    {
                        bonfirelitmessage.gameObject.SetActive(true);
                        yield return new WaitForSeconds(3);
                        bonfirelitmessage.gameObject.SetActive(false);
                    }

                }











            }



        }


    }



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")

        {
            if (!bonfireactivated)
            {

                bonfiremessage.gameObject.SetActive(true);
                isNotAt = false;

            }
       





        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")

        {

            bonfiremessage.gameObject.SetActive(false);
            isNotAt = true;



        }

    }






}

