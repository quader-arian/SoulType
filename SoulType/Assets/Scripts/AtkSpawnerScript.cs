using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AtkSpawnerScript : MonoBehaviour
{
    private float timeBtwSpawns;
    private float defaultTimeBtwSpawns;
    public float startTimeBtwSpawns;
    public float timeDecrease;
    public float minTime;

    public bool shuffleNeeded;
    public float wordSpeed;
    public string[] wordPicks;
    public GameObject words;

    private float fireTime;
    private float slowTime;
    private float poweredTime;
    public TMP_Text status;

    private int i;

    private void Start()
    {
        timeBtwSpawns = startTimeBtwSpawns;
        defaultTimeBtwSpawns = timeBtwSpawns;
        fireTime = 5f;
        slowTime = 8f;
        poweredTime = 3f;

        shuffle();
    }

    private void Update()
    {
        if(PlayerStatsScript.isBurn && PlayerStatsScript.isSlowed){
            status.text = "Stunned " + (int)slowTime + "+ Burning " + (int)fireTime;
        }else if(PlayerStatsScript.isSlowed){
            status.text = "Stunned " + (int)slowTime;
        }else if(PlayerStatsScript.isBurn){
            status.text = "Burning " + (int)fireTime;
        }else{
            status.text = "";
        }

        if (timeBtwSpawns <= 0){
            Instantiate(words, transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            AtkWordScript thisWord = words.GetComponent<AtkWordScript>();
            thisWord.word = wordPicks[i++];
            thisWord.speed = wordSpeed;

            if(i >= wordPicks.Length){
                i = 0;
                shuffle();
            }
            timeBtwSpawns = startTimeBtwSpawns;
            if (startTimeBtwSpawns > minTime){
                startTimeBtwSpawns -= timeDecrease;
            }
        }
        else
        {
            //timeBtwSpawns -= Time.deltaTime;
            if(PlayerStatsScript.isSlowed && slowTime >= 0){
                slowTime -= Time.deltaTime;
                timeBtwSpawns -= 0;
            }else if(PlayerStatsScript.isSlowed){
                PlayerStatsScript.isSlowed = false;
                slowTime = 8f; 
                timeBtwSpawns -= Time.deltaTime;
            }else{
                timeBtwSpawns -= Time.deltaTime;
            }
        }

        if(PlayerStatsScript.isBurn && fireTime >= 0){
            fireTime -= Time.deltaTime;
            PlayerStatsScript.hp -= 10*Time.deltaTime;
        }else if(PlayerStatsScript.isBurn){
            PlayerStatsScript.isBurn = false;
            fireTime = 5f; 
        }
    }

    private void shuffle(){
        if(shuffleNeeded){
            for(int j = 0; j < wordPicks.Length; j++){
                int rnd = Random.Range(0, wordPicks.Length);
                string temp = wordPicks[rnd];
                wordPicks[rnd] = wordPicks[j];
                wordPicks[j] = temp;
            }
        }
    }
}
