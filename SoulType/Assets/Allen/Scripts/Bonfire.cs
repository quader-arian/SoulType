
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Bonfire : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    public AudioClip healsound;

    int bonfireactivated = 0;

    public bool isNotAt = true;
    private BonfirePositions bf;



    [SerializeField] private Transform bonfiremessage;
    [SerializeField] private Transform bonfireoff;
    [SerializeField] private Transform bonfireon;
    [SerializeField] private Transform bonfirelitmessage;
    [SerializeField] private Transform healedmessage;
    [SerializeField] private Transform healedmessage2;
    [SerializeField] private Transform healedmessage3;




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
                if (bonfireactivated == 0)

                {
                    bonfireactivated++;
                    bonfireoff.gameObject.SetActive(false);
                    bonfireon.gameObject.SetActive(true);
                    source.PlayOneShot(clip);


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



                else if (bonfireactivated == 1)

                {


                    PlayerStatsScript.hp = 500;
                    PlayerStatsScript.hp = PlayerStatsScript.maxHp;
                    bonfireactivated++;
                    healedmessage.gameObject.SetActive(false);


                    source.PlayOneShot(healsound);


                    {
                        StartCoroutine(Delay());
                    }

                    IEnumerator Delay()
                    {
                        healedmessage.gameObject.SetActive(true);
                        yield return new WaitForSeconds(2);
                        healedmessage.gameObject.SetActive(false);
                    }

                }


                else if (bonfireactivated == 2)
                {


                    PlayerStatsScript.hp = 500;
                    PlayerStatsScript.hp = PlayerStatsScript.maxHp;
                    bonfireactivated++;
                    healedmessage.gameObject.SetActive(false);
                    source.PlayOneShot(healsound);
                    {
                        StartCoroutine(Delay());
                    }

                    IEnumerator Delay()
                    {
                        healedmessage2.gameObject.SetActive(true);
                        yield return new WaitForSeconds(2);
                        healedmessage2.gameObject.SetActive(false);
                    }



                }


                else if (bonfireactivated == 3)
                {

                    healedmessage2.gameObject.SetActive(false);
                  
                    {
                        StartCoroutine(Delay());
                    }

                    IEnumerator Delay()
                    {
                        healedmessage3.gameObject.SetActive(true);
                        yield return new WaitForSeconds(2);
                        healedmessage3.gameObject.SetActive(false);
                    }



                }



            }


        }




    }









    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")


        {
          
                bonfiremessage.gameObject.SetActive(true);
                isNotAt = false;

            
       





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

