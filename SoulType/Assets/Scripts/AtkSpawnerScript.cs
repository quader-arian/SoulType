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

    private int i;

    private void Start()
    {
        timeBtwSpawns = startTimeBtwSpawns;
        defaultTimeBtwSpawns = timeBtwSpawns;

        shuffle();
    }

    private void Update()
    {
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
            timeBtwSpawns -= Time.deltaTime;
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
