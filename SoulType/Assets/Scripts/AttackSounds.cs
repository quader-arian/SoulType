using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSounds : MonoBehaviour
{

    public AudioSource source;
    public AudioClip damagesound;



    private float healthTemp = 500; //previous value of player health


    private void Update()

    {
        if (healthTemp > PlayerStatsScript.hp)
        {
            healthTemp = PlayerStatsScript.hp;

                source.PlayOneShot(damagesound);




        }





    }


}
