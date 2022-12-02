using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitsSound : MonoBehaviour
{

    public AudioSource source;
    public AudioClip hitsound;

    private float healthTemp; //previous value of enemy health


    private void Update()

    {
        if (healthTemp != EnemyStatsScript.hp)
        {
            healthTemp = EnemyStatsScript.hp;

                source.PlayOneShot(hitsound);




        }





    }


}
